using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;


public class Shieldman : Unit
{
    public GameObject normalHit;
    [SerializeField] private float teamRange;
    [SerializeField] private float shieldRange;
    public GameObject shieldEffect;


    //Các điều kiện
    [SerializeField] private bool createShield;
    [SerializeField] private bool canAttack;



    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        rangeAttack = 0.2f;
        shieldRange = 2f;
        baseSpeed = 0.1f;
        speed = baseSpeed;
        maxHP = Random.Range(575,610);
        HP = maxHP;
        baseMagicDmg = 0f;
        magicDmg = baseMagicDmg;
        basePhyDmg = 45f;
        phyDmg = basePhyDmg;
        baseDefense = Random.Range(25,40);
        defense = baseDefense;
        baseResistance = 0;
        resistance = baseResistance;

        maxBaseHP = Random.Range(475, 520);
        maxHP = maxBaseHP * (1 + armyEncircleBonus + armyLvlBonus);
        HP = maxHP;

        currMagicDmg = Random.Range(0, 0);
        baseMagicDmg = currMagicDmg * (1 + armyEncircleBonus + armyLvlBonus);
        magicDmg = baseMagicDmg;

        currPhyDmg = Random.Range(45, 50);
        basePhyDmg = currPhyDmg * (1 + armyEncircleBonus + armyLvlBonus);
        phyDmg = basePhyDmg;

        currDefense = Random.Range(25, 40);
        baseDefense = currDefense * (1 + armyEncircleBonus + armyLvlBonus);
        defense = baseDefense;

        currResistance = Random.Range(0, 0);
        baseResistance = currResistance * (1 + armyEncircleBonus + armyLvlBonus);
        resistance = baseResistance;
        ExhaustToStats();

        hpbar.fillAmount = HP / maxHP;

    }





    // Update is called once per frame
    void FixedUpdate()
    {
        ShieldLogic();
        
        switch (currentState)
        {
            case State.idle:
               
                if (HP <= 0)
                {
                    currentState = State.dead;
                    break;
                }
                Idle();
                break;
            case State.attack:
                rb.velocity = rb.velocity * 0.05f;
                if (HP <= 0)
                {
                    currentState = State.dead;
                }

                if (!canAttack)
                {
                    canAttack = true;
                    if (rb.velocity.x != 0)
                    {
                        animator.Play("Shieldman Run");
                    }
                    else
                    {
                        animator.Play("Shieldman Idle");

                    }
                    float x = target.transform.position.x - transform.position.x;
                    if (x < 0)
                        spriteRenderer.flipX = true;
                    else
                        spriteRenderer.flipX = false;

                    if (playerCollider != null)
                    {
                        if (!createShield)
                        {
                            createShield = true;
                            animator.Play("Shieldman Defend");
                            CreateShieldToAUnit(playerCollider.GetComponent<Unit>());
                            //StartCoroutine(ShieldCountdown());
                        }
                           
                    }
                    else if (enemyCollider != null)
                    {
                        target = enemyCollider.gameObject;
                        animator.Play("Shieldman Attack", 0, 0f);
                        Vector3 pos = new Vector3(target.transform.position.x, target.transform.position.y, -0.1f);
                        Instantiate(normalHit, pos, Quaternion.identity);
                        _Attack(target,magicDmg, phyDmg,target.GetComponent<Unit>().maxHP * 0.01f);
                    }

                }

                break;

            case State.dead:
                DeadStatus("Shieldman Dead");

                break;
            case State.run:
                if (HP <= 0)
                {
                    currentState = State.dead;
                }
                
                Run();

                break;
            default:
                break;
        }
    }

    public void ShieldLogic()
    {
        if (currentState != State.attack && currentState != State.dead)
        {
            if (!syncAttack)
            {
                speed = baseSpeed;
                StopCoroutine(syncCoroutine);
                startSync = false;
            }
            else
            {
                if (!startSync)
                {
                    startSync = true;
                    var minSpeed = gameManager.playerTeam
                    .Select(c => c.GetComponent<Unit>())
                    .OrderBy(d => d.baseSpeed)
                    .FirstOrDefault();

                    if (minSpeed != null)
                    {
                        speed = minSpeed.baseSpeed;
                        StartCoroutine(syncSpeedCountdown());
                    }
                    else
                        speed = baseSpeed;
                }

            }
            enemyCollider = Physics2D.OverlapCircle(transform.position, rangeAttack, 1 << enemyLayer);

            

            if (!createShield)
            {
                FindShieldTarget(shieldRange);
            }

            if ((enemyCollider != null || playerCollider != null) && currentState != State.dead && !retreat)
            {
                if (enemyCollider != null)
                    target = enemyCollider.gameObject;
                
                if (playerCollider != null)
                    target = playerCollider.gameObject;
                currentState = State.attack;
            }
            else
            {
                if (retreat)
                {
                    target = commandPost;
                }
                else if (dontBreakLineup)
                {
                    target = straightTarget;
                }
                else
                {
                    enemyList = gameObject.layer == LayerMask.NameToLayer("Enemy") ? gameManager.playerTeam : gameManager.enemyTeam;
                    if (target == null || !enemyList.Contains(target) && !retreat && !dontBreakLineup)
                    {
                        target = enemyList.Count > 0 ? Process_FocusRange_FocusMelee() : commandPost;
                    }
                }
                Vector2 direction = (target.transform.position - transform.position).normalized;
                if (currentState != State.dead || currentState != State.attack)
                {
                    rb.velocity = !hold ? direction * speed * disruptSlowAmount : Vector2.zero;
                    velocityCheck = rb.velocity.magnitude;
                }
            }
        }
    }
    public void CreateShieldToAUnit(Unit unit)
    {
        unit.ShieldBuff();
    }

    public void Idle()
    {
        animator.Play("Shieldman Idle");
        if (rb.velocity != Vector2.zero && !hold)
        {
            currentState = State.run;
        }

    }

    public void Run()
    {
        animator.Play("Shieldman Run");
        if (rb.velocity == Vector2.zero)
        {
            currentState = State.idle;
        }


        float direction = rb.velocity.x;

        if (direction < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (direction > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    
   

    public void Dead()
    {
        Destroy(gameObject);
    }
    

    public void AfterAttackOrGetHit()
    {
        currentState = State.idle;
        canAttack = false;
        target = null;
        attackSpellCollider = null;
        playerCollider = null;
        enemyCollider = null;
    }


}

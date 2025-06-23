using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;


public class Archmage : Unit
{
    public GameObject flameHit;
    [SerializeField] private float supportRange;
    [SerializeField] private float attackSpellRange;

    public GameObject fireSpark;
    public GameObject heal;



    //Các điều kiện
    [SerializeField] private bool canAttack;
    [SerializeField] private bool canHeal;
    [SerializeField] private bool canCastAttackSpell;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        rangeAttack = 0.2f;
        supportRange = 4f;
        attackSpellRange = 4f;
        baseSpeed = 0.2f;
        speed = baseSpeed;

        maxBaseHP = Random.Range(80, 96);
        maxHP = maxBaseHP * (1 + armyEncircleBonus + armyLvlBonus);
        HP = maxHP;

        currMagicDmg = Random.Range(80, 90);
        baseMagicDmg = currMagicDmg * (1 + armyEncircleBonus + armyLvlBonus);
        magicDmg = baseMagicDmg;

        currPhyDmg = Random.Range(0, 0);
        basePhyDmg = currPhyDmg * (1 + armyEncircleBonus + armyLvlBonus);
        phyDmg = basePhyDmg;

        currDefense = Random.Range(0, 3);
        baseDefense = currDefense * (1 + armyEncircleBonus + armyLvlBonus);
        defense = baseDefense;

        currResistance = Random.Range(8, 16);
        baseResistance = currResistance * (1 + armyEncircleBonus + armyLvlBonus);
        resistance = baseResistance;
        ExhaustToStats();

        hpbar.fillAmount = HP / maxHP;


    }




    // Update is called once per frame
    void FixedUpdate()
    {
        enemyCollider = Physics2D.OverlapCircle(transform.position, rangeAttack, 1 << enemyLayer);

        if (!canCastAttackSpell)
        {
            attackSpellCollider = Physics2D.OverlapCircle(transform.position, attackSpellRange, 1 << enemyLayer);

            if (attackSpellCollider != null )
            {
                if(attackSpellCollider.GetComponent<Unit>().isBurn)
                    attackSpellCollider = null;
                else
                {
                    if (fireSparkTarget == "none")
                    {

                    }
                    else if (attackSpellCollider.GetComponent<Unit>().unitClass != fireSparkTarget)
                    {
                        attackSpellCollider = null;
                    }
                }
            }
            
        }
        else
        {
            attackSpellCollider = null;

        }

        if (!canHeal)
        {
            playerCollider = Physics2D.OverlapCircle(transform.position, supportRange, 1 << friendlyLayer);
            if (playerCollider != null)
            {
                Unit unit = playerCollider.GetComponent<Unit>();

                if (unit.HP / unit.maxHP > 0.75f)
                {
                    playerCollider = null;
                }
                else
                {
                    if (healTarget == "none")
                    {

                    }
                    else if (playerCollider.GetComponent<Unit>().unitClass != healTarget)
                    {
                       playerCollider = null;
                    }
                }    
            }

        }
        else
        {
            playerCollider = null;
        }

        if ((enemyCollider != null || attackSpellCollider != null || playerCollider != null) && currentState != State.dead && currentState != State.attack && !retreat)
        {
            if (enemyCollider != null)
                target = enemyCollider.gameObject;

            if (attackSpellCollider != null)
                target = attackSpellCollider.gameObject;

            if (playerCollider != null)
                target = playerCollider.gameObject;

            currentState = State.attack;
        }


        enemyList = gameObject.layer == LayerMask.NameToLayer("Enemy") ? gameManager.playerTeam : gameManager.enemyTeam;
        if(currentState != State.attack)
        {
            if ((target == null || !enemyList.Contains(target)) && !retreat && !dontBreakLineup)
            {
                Process_FocusRange_FocusMelee();
            }
            else
            {

                if (dontBreakLineup)
                {
                    target = straightTarget;

                }
                if (retreat)
                {
                    target = commandPost;
                }

                if (!syncAttack)
                {
                    speed = baseSpeed;
                    if(syncCoroutine != null)
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

                Vector2 direction = (target.transform.position - transform.position).normalized;
                if (currentState != State.dead || currentState != State.attack)
                {
                    rb.velocity = !hold ? direction * speed * disruptSlowAmount : Vector2.zero;
                }
            }
        }
        

        switch (currentState)
        {
            case State.idle:
                
                if (HP <= 0)
                {
                    currentState = State.dead;
                }
               
                Idle();
                break;
            case State.attack:
                rb.velocity = Vector2.zero;

                if (HP <= 0)
                {
                    currentState = State.dead;
                }
                if (target != null)
                {
                    float x = target.transform.position.x - transform.position.x;
                    if (x < 0)
                        spriteRenderer.flipX = true;
                    else
                        spriteRenderer.flipX = false;
                }



                if (!canAttack)
                {
                    canAttack = true;
                    if (rb.velocity.x != 0)
                    {
                        animator.Play("Archmage Run");
                    }
                    else
                    {
                        animator.Play("Archmage Idle");

                    }

                    if (attackSpellCollider != null)
                    {
                        if (!canCastAttackSpell)
                        {
                            canCastAttackSpell = true;
                            target = attackSpellCollider.gameObject;
                            animator.Play("Archmage Spell 1", 0, 0);
                            StartCoroutine(AttackSpellCountdown());
                        }

                    }
                    else if (playerCollider != null)
                    {
                        if (!canHeal)
                        {
                            canHeal = true;
                            animator.Play("Archmage Spell 2", 0, 0f);
                            GameObject heal_ = Instantiate(heal, playerCollider.gameObject.transform.position, Quaternion.identity);
                            heal_.transform.SetParent(playerCollider.gameObject.transform);
                            _Heal(playerCollider.gameObject, magicDmg * 2.5f);
                            StartCoroutine(HealCountdown());

                        }

                    }
                    else if (enemyCollider != null)
                    {
                        target = enemyCollider.gameObject;
                        animator.Play("Archmage Attack", 0, 0f);
                        float dmg = target.GetComponent<Unit>().unitClass == "shieldman" ? 2 : 1;
                        Vector3 pos = new Vector3(target.transform.position.x, target.transform.position.y, -0.1f);
                        Instantiate(flameHit, pos, Quaternion.identity);
                        _Attack(target, magicDmg * dmg, 0, 0);
                    }
                }
                break;
            case State.dead:
                DeadStatus("Archmage Die");

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
    IEnumerator HealCountdown()
    {
        yield return new WaitForSeconds(5f);
        canHeal = false;
    }
    IEnumerator AttackSpellCountdown()
    {
        yield return new WaitForSeconds(22f);
        canCastAttackSpell = false;
    }

    public void Idle()
    {
        animator.Play("Archmage Idle");
        if (rb.velocity != Vector2.zero && !hold)
        {
            currentState = State.run;
        }

    }

    public void CreateFireSpark()
    {
        if (target != null)
        {
            GameObject _fireSpark = Instantiate(fireSpark, target.transform.position, Quaternion.identity);
            _fireSpark.GetComponent<FireSpark>().magicDmg = magicDmg;
            if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                _fireSpark.GetComponent<FireSpark>().targetLayer = "Player";
            }

            if (gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                _fireSpark.GetComponent<FireSpark>().targetLayer = "Enemy";
            }

        }
    }

    public void Run()
    {
        animator.Play("Archmage Run");
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
    public void GetHit()
    {

    }

    public void Dead()
    {
        Destroy(gameObject);
    }
    public void ChargeAttack()
    {

    }
    public void Attack()
    {

    }

    public void AfterAttackOrGetHit()
    {
        currentState = State.idle;
        canAttack = false;
    }


}

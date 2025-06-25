using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;


public class Archer : Unit
{

    public GameObject arrow;
    public float angel = 60f;

    //Các điều kiện
    [SerializeField] private bool canAttack;



    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        rangeAttack = 3.4f;

        baseSpeed = 0.18f;
        speed = baseSpeed;

        maxBaseHP = Random.Range(60, 77);
        maxHP = maxBaseHP * (1 + armyEncircleBonus + armyLvlBonus);
        HP = maxHP;

        baseMagicDmg = 0;
        magicDmg = baseMagicDmg;

        currPhyDmg = Random.Range(11, 16);
        basePhyDmg = currPhyDmg * ( 1 + armyEncircleBonus + armyLvlBonus);
        phyDmg = basePhyDmg;

        currDefense = Random.Range(2, 5);
        baseDefense = currDefense * (1 + armyEncircleBonus + armyLvlBonus);
        defense = baseDefense;

        currResistance = Random.Range(3, 8);
        baseResistance = currResistance * (1 + armyEncircleBonus + armyLvlBonus);
        resistance = baseResistance;

        ExhaustToStats();
        hpbar.fillAmount = HP / maxHP;

    }





    // Update is called once per frame
    void FixedUpdate()
    {
        ArcherLogic();

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

                if (!canAttack)
                {
                    canAttack = true;

                    animator.Play("Archer Attack", 0, 0f);
                    if (target != null)
                    {
                        float x = target.transform.position.x - transform.position.x;
                        if (x < 0)
                            spriteRenderer.flipX = true;
                        else
                            spriteRenderer.flipX = false;
                    }
                }
                break;
            case State.dead:
                DeadStatus("Archer dead");
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
    

    public void Idle()
    {
        animator.Play("Archer Idle");
        if (rb.velocity != Vector2.zero && !hold)
        {
            currentState = State.run;
        }

    }

    public void Run()
    {
        animator.Play("Archer run");
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
    public void ChargeAttack()
    {

    }
    public void FireArrows()
    {
        if (target != null)
        {
            float dmg = phyDmg;
            string targetLayer = "";
            if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                targetLayer = "Player";
            }

            if (gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                targetLayer = "Enemy";
            }
            Vector2 targetPos = target.transform.position;
            StartCoroutine(ArrowFall(targetPos,dmg,targetLayer));
        }
    }
    public void ArcherLogic()
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
            if (enemyCollider != null && currentState != State.dead && !retreat)
            {
                target = enemyCollider.gameObject;
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
    public void AfterAttackOrGetHit()
    {
        currentState = State.idle;
        canAttack = false;
        target = null;
        attackSpellCollider = null;
        playerCollider = null;
        enemyCollider = null;
    }

    IEnumerator ArrowFall(Vector2 targetPos,float dmg, string targetLayer)
    {
        yield return new WaitForSeconds(0.75f);
        float rad;
        float flipDegre = 135;
        float degre = 45f;
        for (int i = 0; i < 5; i++)
        {
            Vector2 appearPos;
            if (targetPos.x - transform.position.x < 0)
            {
                appearPos = new Vector2(Random.Range(target.transform.position.x + 1.2f, target.transform.position.x + 0.4f), target.transform.position.y + 1f);
                rad = flipDegre * Mathf.Deg2Rad;
                flipDegre -= 7.5f;
            }
            else
            {
                appearPos = new Vector2(Random.Range(target.transform.position.x - 1.2f, target.transform.position.x - 0.4f), target.transform.position.y + 2f);
                rad = degre * Mathf.Deg2Rad;
                degre += 7.5f;

            }
            GameObject _arrow = Instantiate(arrow, appearPos, Quaternion.identity);
            _arrow.transform.right = new Vector2(Mathf.Cos(rad), -Mathf.Sin(rad));
            _arrow.GetComponent<Arrow>().speed = 4f;
            _arrow.GetComponent<Arrow>().direction = _arrow.transform.right;
            _arrow.GetComponent<Arrow>().phyDmg = dmg;
            _arrow.GetComponent<Arrow>().targetLayer = targetLayer;
            _arrow.GetComponent<Arrow>().source = "archer";






        }
    }


}

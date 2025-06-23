using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;


public class Halbert : Unit
{

    public GameObject normalHit;

    //Các điều kiện
    [SerializeField] private bool canAttack;



    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        baseSpeed = 0.32f;
        rangeAttack = 0.2f;
        speed = baseSpeed;


        maxBaseHP = Random.Range(385, 400);
        maxHP = maxBaseHP * (1 + armyEncircleBonus + armyLvlBonus);
        HP = maxHP;

        currMagicDmg = Random.Range(0, 0);
        baseMagicDmg = currMagicDmg * (1 + armyEncircleBonus + armyLvlBonus);
        magicDmg = baseMagicDmg;

        currPhyDmg = Random.Range(24, 32);
        basePhyDmg = currPhyDmg * (1 + armyEncircleBonus + armyLvlBonus);
        phyDmg = basePhyDmg;

        currDefense = Random.Range(5, 10);
        baseDefense = currDefense * (1 + armyEncircleBonus + armyLvlBonus);
        defense = baseDefense;

        currResistance = Random.Range(5, 8);
        baseResistance = currResistance * (1 + armyEncircleBonus + armyLvlBonus);
        resistance = baseResistance;
        ExhaustToStats();

        hpbar.fillAmount = HP / maxHP;
    }





    // Update is called once per frame
    void FixedUpdate()
    {
        Collider2D enemyCollider = Physics2D.OverlapCircle(transform.position, rangeAttack, 1 << enemyLayer);


        if (enemyCollider != null && currentState != State.dead && !retreat)
        {

            target = enemyCollider.gameObject;
            currentState = State.attack;
        }
        enemyList = gameObject.layer == LayerMask.NameToLayer("Enemy") ? gameManager.playerTeam : gameManager.enemyTeam;
        if (currentState != State.attack)
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
                rb.velocity = rb.velocity * 0.1f;
                if (HP <= 0)
                {
                    currentState = State.dead;
                }

                if (!canAttack)
                {
                    canAttack = true;

                    animator.Play("Halbert Attack", 0, 0f);
                    if (target != null)
                    {
                        float x = target.transform.position.x - transform.position.x;
                        if (x < 0)
                            spriteRenderer.flipX = true;
                        else
                            spriteRenderer.flipX = false;

                        float advantage;
                        Unit targetScript = target.GetComponent<Unit>();
                        if (targetScript.unitClass == "knight" ||
                            targetScript.unitClass == "shieldman")
                        {
                            advantage = 1.5f;
                        }
                        else
                        {
                            advantage = 1;
                        }
                        Vector3 pos = new Vector3(target.transform.position.x, target.transform.position.y, -0.1f);
                        Instantiate(normalHit, pos, Quaternion.identity);
                        _Attack(target, 0, phyDmg * advantage, targetScript.maxHP * 0.015f);
                    }

                }

                break;

            case State.dead:
                DeadStatus("Halbert Dead");

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
        animator.Play("Halbert Idle");
        if (rb.velocity != Vector2.zero && !hold)
        {
            currentState = State.run;
        }

    }

    public void Run()
    {
        animator.Play("Halbert Run");
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

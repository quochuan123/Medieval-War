using UnityEngine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;


public class Knight : Unit
{
    public GameObject normalHit;

    //Các điều kiện
    [SerializeField] private bool canAttack;
    [SerializeField] private bool speedIncrease;
    [SerializeField] private float bonusSpeed;
    private float maxSpeed = 0.75f;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        baseSpeed = 0.4f;
        rangeAttack = 0.4f;
        speed = baseSpeed;

        maxBaseHP = Random.Range(200, 220);
        maxHP = maxBaseHP * (1 + armyEncircleBonus + armyLvlBonus);
        HP = maxHP;

        currMagicDmg = Random.Range(0, 0);
        baseMagicDmg = currMagicDmg * (1 + armyEncircleBonus + armyLvlBonus);
        magicDmg = baseMagicDmg;

        currPhyDmg = Random.Range(45, 56);
        basePhyDmg = currPhyDmg * (1 + armyEncircleBonus + armyLvlBonus);
        phyDmg = basePhyDmg;

        currDefense = Random.Range(6, 9);
        baseDefense = currDefense * (1 + armyEncircleBonus + armyLvlBonus);
        defense = baseDefense;

        currResistance = Random.Range(3, 5);
        baseResistance = currResistance * (1 + armyEncircleBonus + armyLvlBonus);
        resistance = baseResistance;
        ExhaustToStats();

        hpbar.fillAmount = HP / maxHP;
    }

    IEnumerator IncreaseSpeed()
    {
        yield return new WaitForSeconds(1f);
        bonusSpeed += 0.15f;
        speedIncrease = false;

    }



    // Update is called once per frame
    void FixedUpdate()
    {
        KnightLogic();
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

                    animator.Play("Knight Attack", 0, 0f);
                    if (target != null)
                    {
                        float x = target.transform.position.x - transform.position.x;
                        if (x < 0)
                            spriteRenderer.flipX = true;
                        else
                            spriteRenderer.flipX = false;
                        float advantage;
                        Unit targetScript = target.GetComponent<Unit>();
                        if (targetScript.unitClass == "halbert" ||
                            targetScript.unitClass == "crossbow" ||
                            targetScript.unitClass == "archer" ||
                                targetScript.unitClass == "mage" ||
                                targetScript.unitClass == "archmage")
                        {
                            advantage = 2;
                        }
                        else
                        {
                            advantage = 1;
                        }

                        Vector3 pos = new Vector3(target.transform.position.x, target.transform.position.y, -1f);
                        GameObject newEffect = Instantiate(normalHit, pos, Quaternion.identity);
                        _Attack(target, 0, phyDmg * (1 + speed + bonusSpeed) * advantage, targetScript.maxHP * 0.01f);
                        bonusSpeed = 0;
                    }

                }

                break;

            case State.chargeAttack:
                animator.Play("Knight Charge Attack");
                if (HP <= 0)
                {
                    currentState = State.dead;
                }

                if (bonusSpeed < maxSpeed)
                {
                    currentState = State.run;
                }
                Run();

                break;
            case State.dead:
                DeadStatus("Knight Dead");

                break;
            case State.run:
                animator.Play("Knight Run");
                if (HP <= 0)
                {
                    currentState = State.dead;
                }

                if (bonusSpeed >= maxSpeed)
                {
                    currentState = State.chargeAttack;
                }



                Run();

                break;
            default:
                break;
        }
    }

    public void Idle()
    {
        animator.Play("Knight Idle");
        if (rb.velocity != Vector2.zero && !hold)
        {
            currentState = State.run;
        }

    }

    public void KnightLogic()
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
                    bonusSpeed = 0;
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
                    if (target == null ||!enemyList.Contains(target)&& !retreat && !dontBreakLineup)
                    {
                        target = enemyList.Count > 0 ? Process_FocusRange_FocusMelee() : commandPost;
                    }
                }
                Vector2 direction = (target.transform.position - transform.position).normalized;
                if (currentState != State.dead || currentState != State.attack)
                {
                    rb.velocity = !hold ? direction * (speed + bonusSpeed) * disruptSlowAmount : Vector2.zero;
                    velocityCheck = rb.velocity.magnitude;
                    if (bonusSpeed < maxSpeed && !syncAttack && !hold)
                    {
                        if (!speedIncrease && currentState == State.run && !syncAttack)
                        {
                            speedIncrease = true;
                            StartCoroutine(IncreaseSpeed());
                        }
                    }
                }

            }
        }
    }

    public void Run()
    {
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
        target = null;
        attackSpellCollider = null;
        playerCollider = null;
        enemyCollider = null;
    }


}

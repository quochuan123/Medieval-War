using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;


public class Crossbow : Unit
{

    public GameObject arrow;

    //Các điều kiện
    [SerializeField] private bool canAttack;

    [SerializeField] private float critChance;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        critChance = 30;
        baseSpeed = 0.24f;
        rangeAttack = 2.1f;
        speed = baseSpeed;
        

        maxBaseHP = Random.Range(125, 140);
        maxHP = maxBaseHP * (1 + armyEncircleBonus + armyLvlBonus);
        HP = maxHP;

        currMagicDmg = Random.Range(0, 0);
        baseMagicDmg = currMagicDmg * (1 + armyEncircleBonus + armyLvlBonus);
        magicDmg = baseMagicDmg;

        currPhyDmg = Random.Range(20, 25);
        basePhyDmg = currPhyDmg * (1 + armyEncircleBonus + armyLvlBonus);
        phyDmg = basePhyDmg;

        currDefense = Random.Range(5, 8);
        baseDefense = currDefense * (1 + armyEncircleBonus + armyLvlBonus);
        defense = baseDefense;

        currResistance = Random.Range(5, 11);
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

                    animator.Play("Crossbow attack", 0, 0f);
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
                DeadStatus("Crossbow die");

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
        animator.Play("Crossbow Idle");
        if (rb.velocity != Vector2.zero && !hold)
        {
            currentState = State.run;
        }

    }

    public void Run()
    {
        animator.Play("Crossbow run");
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
   
    public void FireArrows()
    {
        if (target != null)
        {
            GameObject _arrow = Instantiate(arrow, transform.position, Quaternion.identity);
            _arrow.transform.right = target.transform.position - _arrow.transform.position;

            _arrow.GetComponent<Arrow>().phyDmg = ActiveSpecialSkillChance(critChance) ? phyDmg * 2 : phyDmg;
            _arrow.GetComponent<Arrow>().speed = 3f;
            _arrow.GetComponent<Arrow>().source = "crossbow";
            _arrow.GetComponent<Arrow>().direction = target.transform.position - _arrow.transform.position ;
            if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                _arrow.GetComponent<Arrow>().targetLayer = "Player";
            }

            if (gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                _arrow.GetComponent<Arrow>().targetLayer = "Enemy";
            }
        }
    }

    public void AfterAttackOrGetHit()
    {
        currentState = State.idle;
        canAttack = false;
    }


}

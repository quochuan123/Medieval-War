﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;


public class Mage : Unit
{

    [SerializeField] private float supportRange;
    [SerializeField] private float attackSpellRange;

    public GameObject gravityMagic;
    public GameObject magic;



    //Các điều kiện
    [SerializeField] private bool canAttack;
    [SerializeField] private bool canEnhance;
    [SerializeField] private bool canCastDisruptSpell;





    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        rangeAttack = 1.8f;
        supportRange = 3.2f;
        attackSpellRange = 2.8f;

        baseSpeed = 0.24f;
        speed = baseSpeed;

        maxBaseHP = Random.Range(110, 125);
        maxHP = maxBaseHP * (1 + armyEncircleBonus + armyLvlBonus);
        HP = maxHP;

        currMagicDmg = Random.Range(60, 75);
        baseMagicDmg = currMagicDmg * (1 + armyEncircleBonus + armyLvlBonus);
        magicDmg = baseMagicDmg;

        currPhyDmg = Random.Range(0, 0);
        basePhyDmg = currPhyDmg * (1 + armyEncircleBonus + armyLvlBonus);
        phyDmg = basePhyDmg;

        currDefense = Random.Range(0, 3);
        baseDefense = currDefense * (1 + armyEncircleBonus + armyLvlBonus);
        defense = baseDefense;

        currResistance = Random.Range(25, 40);
        baseResistance = currResistance * (1 + armyEncircleBonus + armyLvlBonus);
        resistance = baseResistance;
        ExhaustToStats();

        hpbar.fillAmount = HP / maxHP;

    }





    // Update is called once per frame
    void FixedUpdate()
    {
        MageLogic();
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
                rb.velocity = Vector2.zero;

                if (HP <= 0)
                {
                    currentState = State.dead;
                    break;
                }

                float x = target.transform.position.x - transform.position.x;
                spriteRenderer.flipX = x < 0 ? true : false;


                if (!canAttack)
                {
                    canAttack = true;
                    if (rb.velocity.x != 0)
                    {
                        animator.Play("Mage Run");
                    }
                    else
                    {
                        animator.Play("Mage Idle");

                    }

                    if (attackSpellCollider != null)
                    {
                        if (!canCastDisruptSpell)
                        {
                            canCastDisruptSpell = true;
                            target = attackSpellCollider.gameObject;
                            animator.Play("Mage Spell 1", 0, 0);
                            StartCoroutine(AttackSpellCountdown());
                        }

                    }
                    else if (playerCollider != null)
                    {
                        if (!canEnhance)
                        {
                            canEnhance = true;

                            {
                                animator.Play("Mage Spell 2", 0, 0f);
                                playerCollider.GetComponent<Unit>().Enhance();
                            }
                        }
                    }
                    else if (enemyCollider != null)
                    {
                        target = enemyCollider.gameObject;
                        animator.Play("Mage Attack", 0, 0f);
                    }

                }

                break;

            case State.dead:
                DeadStatus("Mage Dead");

                break;
            case State.run:
                if (HP <= 0)
                {
                    currentState = State.dead;
                    break;
                }
                Run();

                break;
            default:
                break;
        }
    }

    public void MageLogic()
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

            if (!canCastDisruptSpell)
            {
                attackSpellCollider = enemyList.Count > 0 ? Physics2D.OverlapCircle(transform.position, attackSpellRange, 1 << enemyLayer) : null;

                if (attackSpellCollider != null)
                {
                    if (attackSpellCollider.GetComponent<Unit>().isDisrupt)
                        attackSpellCollider = null;
                    else
                    {
                        if (gravityTarget == "none")
                        {

                        }
                        else if (attackSpellCollider.GetComponent<Unit>().unitClass != gravityTarget)
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

            if (!canEnhance)
            {
                FindEnhanceTarget(supportRange);
            }

            if ((enemyCollider != null || playerCollider != null || attackSpellCollider != null) && currentState != State.dead && !retreat)
            {
                if (enemyCollider != null)
                    target = enemyCollider.gameObject;
                if (attackSpellCollider != null)
                    target = attackSpellCollider.gameObject;
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

    IEnumerator AttackSpellCountdown()
    {
        yield return new WaitForSeconds(30f);
        canCastDisruptSpell = false;
    }

    public void Idle()
    {
        if (rb.velocity != Vector2.zero && !hold)
        {
            currentState = State.run;
        }

    }

    public void FireMagic()
    {
        if (target != null)
        {
            GameObject _magic = Instantiate(magic, transform.position, Quaternion.identity);
            _magic.transform.right = -(target.transform.position - _magic.transform.position);
            _magic.GetComponent<Magic>().magicDmg = magicDmg;
            _magic.GetComponent<Magic>().direction = target.transform.position - _magic.transform.position;
            if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                _magic.GetComponent<Magic>().targetLayer = "Player";
            }

            if (gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                _magic.GetComponent<Magic>().targetLayer = "Enemy";
            }
        }
    }

    public void CreateDisruptField()
    {
        if (target != null)
        {
            GameObject _gravity = Instantiate(gravityMagic, target.transform.position, Quaternion.identity);
            if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                _gravity.GetComponent<DisruptMagic>().targetLayer = "Player";
            }

            if (gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                _gravity.GetComponent<DisruptMagic>().targetLayer = "Enemy";
            }

        }
    }

    public void Run()
    {
        animator.Play("Mage Run");
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

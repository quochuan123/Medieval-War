using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Linq;

public class Unit : MonoBehaviour
{
    public bool focusMelee;
    public bool focusRange;
    public string fireSparkTarget = "none";
    public string healTarget = "none";
    public string gravityTarget = "none";
    public string endureTarget = "none";
    public string enhanceTarget = "none";



    public bool isRange;

    public Coroutine syncCoroutine;
    public GameObject shieldEffectAnim;
    public GameObject strengthEffectAnim;
    public string inArmy = "";
    public int matchingNumber;
    private SingletonScript sts;
    [SerializeField] protected bool startSync;
    public GameObject commandPost;
    [SerializeField] protected float velocityCheck;
    public bool hold;
    public bool attack;
    public bool retreat;
    public bool syncAttack;
    public bool dontBreakLineup;
    public float exhaustValue;
    public float enhanceValue;
    public bool isDead;


    public GameObject hpbarObj;
    public Image hpbar;
    protected bool find1Delay;
    protected bool find2Delay;
    protected bool find3Delay;

    protected bool find1Lock;
    protected bool find2Lock;
    protected bool find3Lock;



    public GameObject target;
    [SerializeField] protected GameObject straightTarget;

    [SerializeField] protected List<GameObject> enemyList;
    [SerializeField] protected List<GameObject> friendList;


    [SerializeField] private GameObject dummy;
    public GameObject enhanceMagic;
    public string unitClass;
    public float maxBaseHP;
    public float maxHP;
    public float HP;
    public float currPhyDmg;
    public float basePhyDmg;
    public float phyDmg;
    public float currMagicDmg;
    public float baseMagicDmg;
    public float magicDmg;

    public float currDefense;
    public float baseDefense;
    public float defense;
    public float currResistance;
    public float baseResistance;
    public float resistance;
    public float baseSpeed;
    public float speed;

    public float armyLvlBonus;
    public float armyEncircleBonus;

    [SerializeField] protected int enemyLayer;
    [SerializeField] protected int friendlyLayer;

    public float shieldBuffAmount;
    [SerializeField] protected float rangeAttack;
    protected Animator animator;

    //Burn effect
    public bool isBurn;
    [SerializeField] private float burnDmg;
    [SerializeField] private float burnDuration;
    [SerializeField] private float burnInterval;

    //Enhance effect
    public bool isEnhance;

    //Disrupt effect
    public bool isDisrupt;
    [SerializeField] private float disruptDuration;
    [SerializeField] protected float disruptSlowAmount;
    [SerializeField] private float disruptInterval;

    //Shield buff
    public bool isShielded;
    [SerializeField] private float shieldDuration;
    [SerializeField] protected float shieldAmount;

    //First shield
    [SerializeField] private bool firstShield;
    public enum State
    {
        idle,
        run,
        chargeAttack,
        dead,
        attack,
        getHit
    };
    public enum Status
    {

    };

    public State currentState;
    public Status currentStatus;
    protected GameManager gameManager;
    protected Rigidbody2D rb;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    [SerializeField] protected Collider2D enemyCollider;
    [SerializeField] protected Collider2D playerCollider;
    [SerializeField] protected Collider2D attackSpellCollider;
    // Start is called before the first frame update
    public virtual void Start()
    {
        shieldBuffAmount = 0f;
        disruptSlowAmount = 1f;
        burnInterval = 0.75f;

        sts = FindObjectOfType<SingletonScript>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();



        currentState = State.idle;
        if (gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            enemyLayer = LayerMask.NameToLayer("Enemy");
            friendlyLayer = LayerMask.NameToLayer("Player");
        }
        else if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemyLayer = LayerMask.NameToLayer("Player");
            friendlyLayer = LayerMask.NameToLayer("Enemy");
        }

        target = null;
        if (unitClass == "halbert")
        {
            firstShield = true;
        }

        //commandPost = GameObject.Find("Command Post");

        if (gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            exhaustValue = sts.playerData.exhaustedLvl;
        }
        if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            exhaustValue = sts.enemyData.exhaustedLvl;
        }
        switch (inArmy)
        {
            case "main":
                if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    straightTarget = gameManager.p_mainforce[matchingNumber];
                }

                if (gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    straightTarget = gameManager.e_mainforce[matchingNumber];
                }
                break;
            case "left":
                if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    straightTarget = gameManager.p_left[matchingNumber];
                }

                if (gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    straightTarget = gameManager.e_right[matchingNumber];
                }
                break;
            case "right":
                if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    straightTarget = gameManager.p_left[matchingNumber];
                }

                if (gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    straightTarget = gameManager.e_left[matchingNumber];
                }
                break;
            case "rear":
                if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    straightTarget = gameManager.p_rear[matchingNumber];
                }

                if (gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    straightTarget = gameManager.e_rear[matchingNumber];
                }
                break;
            case "vanguard":
                if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    straightTarget = gameManager.p_vanguard[matchingNumber];
                }

                if (gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    straightTarget = gameManager.e_vanguard[matchingNumber];
                }
                break;
            default:
                break;
        }

        syncCoroutine = StartCoroutine(syncSpeedCountdown());
    }

    public void Enhance()
    {
        if (!isEnhance)
        {
            isEnhance = true;
            strengthEffectAnim.SetActive(true);
            float phyDmgEnhance = basePhyDmg * 0.5f;
            phyDmg = phyDmg + phyDmgEnhance;

            float magicDmgEnhance = baseMagicDmg * 0.5f;
            magicDmg = magicDmg + magicDmgEnhance;
        }
    }



    public void Burned(float _burnDmg, float _burnDuration)
    {
        burnDmg = _burnDmg;
        burnDuration = _burnDuration;
        if (!isBurn)
        {
            isBurn = true;
            StartCoroutine(IsBurning());
        }
    }
    IEnumerator IsBurning()
    {
        while (burnDuration > 0)
        {
            if (HP >= 1)
            {
                HP -= burnDmg;
                if (HP < 0)
                {
                    HP = 0;
                }
                hpbar.fillAmount = HP / maxHP;
            }
            yield return new WaitForSeconds(burnInterval);
            burnDuration -= burnInterval;
        }
        isBurn = false;
    }

    public void Disrupt()
    {
        disruptDuration = 5f;
        disruptInterval = 1f;

        if (!isDisrupt)
        {
            isDisrupt = true;
            disruptSlowAmount = 0.3f;

            StartCoroutine(IsDisrupting());
        }
    }

    IEnumerator IsDisrupting()
    {
        while (disruptDuration > 0)
        {
            yield return new WaitForSeconds(disruptInterval);
            disruptDuration -= disruptInterval;
        }
        disruptSlowAmount = 1f;
        isDisrupt = false;

    }

    public bool ActiveSpecialSkillChance(float ratio)
    {
        float x = Random.Range(0, 101);
        if (ratio > x)
            return true;
        else
            return false;
    }

    public void DeadStatus(string animString)
    {
        if (!isDead)
        {
            isDead = true;
            if (gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                gameManager.playerTeam.Remove(gameObject);
            }

            if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                gameManager.enemyTeam.Remove(gameObject);
            }
            gameObject.layer = LayerMask.NameToLayer("Dead");
            animator.Play(animString);
            GetComponent<Collider2D>().enabled = false;
            rb.bodyType = RigidbodyType2D.Static;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0.94f);
            hpbarObj.SetActive(false);
            shieldEffectAnim.SetActive(false);
            strengthEffectAnim.SetActive(false);

            this.enabled = false;
        }
    }
    public void UpdateStatsWhenUseCommand()
    {

    }
    protected void ExhaustToStats()
    {
        maxHP -= (maxBaseHP * exhaustValue);
        HP = maxHP;

        baseMagicDmg -= (currMagicDmg * exhaustValue);
        magicDmg = baseMagicDmg;

        basePhyDmg -= (currPhyDmg * exhaustValue);
        phyDmg = basePhyDmg;

        baseDefense -= (currDefense * exhaustValue);
        defense = baseDefense;

        baseResistance -= (currResistance * exhaustValue);
        resistance = baseResistance;
    }
    public bool ShieldManUniqueSkillCal(int chance)
    {
        int number = Random.Range(0, 101);
        return number < chance ? true : false;
    }


    public void _LostHP(float magicDmgTaken, float physicDmgTaken, bool throughArmor, float echoeDmg)
    {
        float baseDmgTaken = magicDmgTaken + physicDmgTaken;

        if (unitClass == "shieldman")
        {
            physicDmgTaken = ShieldManUniqueSkillCal(65) ? physicDmgTaken * 0.25f : physicDmgTaken;
        }

        if (unitClass == "halbert")
        {
            physicDmgTaken = ShieldManUniqueSkillCal(40) ? physicDmgTaken * 0.5f : physicDmgTaken;
        }
        if (unitClass == "knight")
        {
            physicDmgTaken = ShieldManUniqueSkillCal(25) ? physicDmgTaken * 0.3f : physicDmgTaken;
        }
        float dmgTaken = physicDmgTaken * (1 - (defense / (11 + defense))) + magicDmgTaken * (1 - (resistance / (11 + resistance)));
        if (isDisrupt)
        {
            dmgTaken = dmgTaken * 1.25f;
        }

        if (isShielded)
        {
            dmgTaken = dmgTaken * 0.4f;
        }
        if (throughArmor)
        {
            dmgTaken = isDisrupt ? baseDmgTaken * 1.25f : baseDmgTaken;
        }
        if (firstShield)
        {
            firstShield = false;
            dmgTaken = 0f;
        }
        HP -= (dmgTaken + echoeDmg);
        if (HP < 0)
        {
            HP = 0;
        }
        hpbar.fillAmount = HP / maxHP;

    }

    public void _Heal(GameObject _target, float healAmount)
    {
        _target.GetComponent<Unit>()._GetHeal(healAmount);
    }

    public void _GetHeal(float healAmount)
    {
        HP += healAmount;
        if (HP > maxHP)
        {
            HP = maxHP;
        }
        hpbar.fillAmount = HP / maxHP;

    }

    public void _Attack(GameObject _target, float _magDmg, float _phyDmg, float _echoeDmg)
    {
        _target.GetComponent<Unit>()._LostHP(_magDmg, _phyDmg, false, _echoeDmg);
    }

    public void ShieldBuff()
    {
        shieldEffectAnim.SetActive(true);


        if (!isShielded)
        {
            isShielded = true;
            shieldBuffAmount = 5f;
            DefenseCalculate();
        }
    }

    public GameObject Process_FocusRange_FocusMelee()
    {
        if (focusMelee)
        {
            var _target = enemyList
                .FirstOrDefault(a => !a.GetComponent<Unit>().isRange);
            return _target != null ? _target : enemyList[Random.Range(0, enemyList.Count)];

        }
        else if (focusRange)
        {
            var _target = enemyList
                .FirstOrDefault(a => a.GetComponent<Unit>().isRange);
            return _target != null ? _target : enemyList[Random.Range(0, enemyList.Count)];
        }
        else
        {
            return enemyList[Random.Range(0, enemyList.Count)];
        }
    }



    public void DefenseCalculate()
    {
        defense = baseDefense + shieldBuffAmount;
        resistance = baseResistance + shieldBuffAmount;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.layer == friendlyLayer)
        {
            if (!gameObject.CompareTag("range") && collision.collider.CompareTag("range"))
            {
                Collider2D myCollider = GetComponent<Collider2D>();
                if (myCollider != null)
                {
                    // Bỏ qua va chạm giữa hai collider
                    Physics2D.IgnoreCollision(myCollider, collision.collider, true);
                }
            }

            else if (collision.gameObject.GetComponent<Unit>().speed < speed)
            {
                Collider2D myCollider = GetComponent<Collider2D>();
                if (myCollider != null)
                {
                    // Bỏ qua va chạm giữa hai collider
                    Physics2D.IgnoreCollision(myCollider, collision.collider, true);
                    StartCoroutine(IgnoreCountdow(myCollider, collision.collider));
                }
            }

        }
    }

    IEnumerator IgnoreCountdow(Collider2D thisCollider, Collider2D otherCollider)
    {
        yield return new WaitForSeconds(2f);
        if (thisCollider != null && otherCollider != null)
        {
            Physics2D.IgnoreCollision(thisCollider, otherCollider, false);
        }
    }

    public IEnumerator syncSpeedCountdown()
    {
        yield return new WaitForSeconds(0.25f);
        startSync = false;
    }

    public void FindEnhanceTarget(float range)
    {
        friendList = gameObject.layer == LayerMask.NameToLayer("Player") ? gameManager.playerTeam : gameManager.enemyTeam;
        if (friendList.Count != 0 && enemyList.Count != 0)
        {
            GameObject unit = friendList[Random.Range(0, friendList.Count)];
            if (Vector2.Distance(transform.position, unit.transform.position) > range || unit.GetComponent<Unit>().isEnhance)
            {
                playerCollider = null;
            }
            else
            {
                if (enhanceTarget == "none")
                {
                    playerCollider = unit.GetComponent<Collider2D>();
                }
                else if (unit.GetComponent<Unit>().unitClass != enhanceTarget)
                {
                    playerCollider = null;
                }
                else
                {
                    playerCollider = unit.GetComponent<Collider2D>();
                }
            }

        }
        else
        {
            playerCollider = null;
        }
    }

    public bool CheckCurrentHp()
    {
        return HP / maxHP < 0.75f ? true : false;
    }

    public void FindHealTarget(float range)
    {
        friendList = gameObject.layer == LayerMask.NameToLayer("Player") ? gameManager.playerTeam : gameManager.enemyTeam;
        if (friendList.Count != 0 && enemyList.Count != 0)
        {
            GameObject unit = friendList[Random.Range(0, friendList.Count)];
            if (Vector2.Distance(transform.position, unit.transform.position) > range || !unit.GetComponent<Unit>().CheckCurrentHp())
            {
                playerCollider = null;
            }
            else
            {
                if (healTarget == "none")
                {
                    playerCollider = unit.GetComponent<Collider2D>();
                }
                else if (unit.GetComponent<Unit>().unitClass != healTarget)
                {
                    playerCollider = null;
                }
                else
                {
                    playerCollider = unit.GetComponent<Collider2D>();
                }
            }

        }
        else
        {
            playerCollider = null;
        }
    }

    public void FindShieldTarget(float range)
    {
        friendList = gameObject.layer == LayerMask.NameToLayer("Player") ? gameManager.playerTeam : gameManager.enemyTeam;
        if (friendList.Count != 0 && enemyList.Count != 0)
        {
            GameObject unit = friendList[Random.Range(0, friendList.Count)];
            if (Vector2.Distance(transform.position, unit.transform.position) > range || unit.GetComponent<Unit>().isShielded)
            {
                playerCollider = null;
            }
            else
            {
                if (endureTarget == "none")
                {
                    playerCollider = unit.GetComponent<Collider2D>();
                }
                else if (unit.GetComponent<Unit>().unitClass != endureTarget)
                {
                    playerCollider = null;
                }
                else
                {
                    playerCollider = unit.GetComponent<Collider2D>();
                }
            }

        }
        else
        {
            playerCollider = null;
        }
    }




}








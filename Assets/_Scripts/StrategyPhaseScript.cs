using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StrategyPhaseScript : MonoBehaviour
{
    private int clickAmount;
    public GameObject p_post;
    public GameObject e_post;
    public Text numberUnitSet;
    public List<Image> armyImage;
    public List<string> nameList;
    [SerializeField] private int maxReinforcement;
    [SerializeField] private int currentNumber;
    
    public Dictionary<int, int> matching = new Dictionary<int, int>()
    {
        { 0, 21 },{ 1, 20 },{ 2, 23 },{ 3, 22 },{ 4, 25},
        { 5, 4 },{ 6, 27 },{ 7, 26 },{ 8, 29 },{ 9, 28},
        { 10, 11 },{ 11, 10 },{ 12, 13 },{ 13, 12 },{ 14, 15 },
        { 15, 14 },{ 16, 17 },{ 17, 16},{ 18, 19},{ 19, 18 },
        { 20, 1 },{ 21, 0 },{ 22, 3 },{ 23, 2 },{ 24, 5 },
        { 25, 4 },{ 26, 7 },{ 27, 6 },{ 28, 9 },{ 29, 8 }
    };

    private CanvasManager cm;
    private SingletonScript sts;
    private SomeEnemyLineup enemyLineup;
    private GameManager gameManager;
    private GameObject playerTeam;
    private GameObject enemyTeam;

    public Sprite knightSprite;
    public Sprite archerSprite;
    public Sprite crossbowSprite;
    public Sprite halbertSprite;
    public Sprite shieldmanSprite;
    public Sprite mageSprite;
    public Sprite archmageSprite;
    public Sprite notSelectSprite;

    public GameObject halbertUnit;
    public GameObject archerUnit;
    public GameObject mageUnit;
    public GameObject archmageUnit;
    public GameObject shieldmanUnit;
    public GameObject crossbowUnit;
    public GameObject knightUnit;

    public GameObject enemyHalbertUnit;
    public GameObject enemyArcherUnit;
    public GameObject enemyMageUnit;
    public GameObject enemyArchmageUnit;
    public GameObject enemyShieldmanUnit;
    public GameObject enemyCrossbowUnit;
    public GameObject enemyKnightUnit;

    public GameObject none;

    public string battleMap;
    public ArmyData enemyData;
    public ArmyData playerData;
    public Node nextNode;
    public Node prevNode;
    public float enemyLvlBonus;
    public float playerLvlBonus;
    public float enemyEncircleBonus;
    public float playerEncircleBonus;
    [Space(30)]
    public GameObject X1Btn;
    public GameObject X5Btn;
    public GameObject X10Btn;









    // Start is called before the first frame update
    void Start()
    {
        armyImage = new List<Image>();
        nameList = new List<string>();
        cm = FindObjectOfType<CanvasManager>();
        sts = FindObjectOfType<SingletonScript>();
        enemyLineup = FindObjectOfType<SomeEnemyLineup>();
        gameManager = FindObjectOfType<GameManager>();
        playerTeam = GameObject.Find("Player Force");
        enemyTeam = GameObject.Find("Enemy Force");

        if (sts.onlyBattle)
        {
            gameManager.nextScene = sts.nextMap;
            battleMap = sts.nextMap;
            enemyLvlBonus = sts.enemyLvl * 0.015f;
            playerLvlBonus = sts._refArmyInfor.lvl * 0.015f;
            enemyEncircleBonus = 0f;
            playerEncircleBonus = 0f;
        }
        else
        {
            gameManager.nextScene = sts.nextMap;
            battleMap = sts.nextMap;
            enemyData = sts.enemyData;
            playerData = sts.playerData;
            nextNode = sts.nextNode;
            prevNode = sts.prevNode;
            enemyLvlBonus = enemyData.lvl * 0.015f;
            playerLvlBonus = playerData.lvl * 0.015f;

            EncircleBonusCalculate();
        }
        ShowAdditional();



        cm.mainForcesUnit = Enumerable.Repeat("none", 30).ToList();
        cm.leftWingUnit = Enumerable.Repeat("none", 30).ToList();
        cm.rightWingUnit = Enumerable.Repeat("none", 30).ToList();
        cm.rearGuardUnit = Enumerable.Repeat("none", 30).ToList();
        cm.vanguardUnit = Enumerable.Repeat("none", 30).ToList();


        cm.knightAmountText.text = "X" + sts.knightAmount.ToString();
        cm.archerAmountText.text = "X" + sts.archerAmount.ToString();
        cm.crossbowAmountText.text = "X" + sts.crossbowAmount.ToString();
        cm.halbertAmountText.text = "X" + sts.halbertAmount.ToString();
        cm.mageAmountText.text = "X" + sts.mageAmount.ToString();
        cm.archmageAmountText.text = "X" + sts.archmageAmount.ToString();
        cm.shieldmanAmountText.text = "X" + sts.shieldmanAmount.ToString();

        armyImage = cm.mainForces;
        nameList = cm.mainForcesUnit;
        //maxReinforcement = 50;
        currentNumber = 0;

        SetUpEnemyArmy();


        for (int i = 0; i < 30; i++)
        {
            cm.enemyMainForces[i].sprite = CovertStringToImage(cm.enemyMainForcesUnit[i]);
            cm.enemyLeftWing[i].sprite = CovertStringToImage(cm.enemyLeftWingUnit[i]);
            cm.enemyRightWing[i].sprite = CovertStringToImage(cm.enemyRightWingUnit[i]);
            cm.enemyVanguard[i].sprite = CovertStringToImage(cm.enemyVanguardUnit[i]);
            cm.enemyRearGuard[i].sprite = CovertStringToImage(cm.enemyRearGuardUnit[i]);

        }

        X1();
        SoundManager sm = FindObjectOfType<SoundManager>();
        if (sm != null)
        {
            sm.StrategyInterfaceMusic();
        }
    }

    public Sprite CovertStringToImage(string _class)
    {
        switch (_class)
        {
            case "knight":
                return knightSprite;
            case "archer":
                return archerSprite;
            case "halbert":
                return halbertSprite;
            case "shieldman":
                return shieldmanSprite;
            case "mage":
                return mageSprite;
            case "archmage":
                return archmageSprite;
            case "crossbow":
                return crossbowSprite;
            default:
                return notSelectSprite;

        }
    }

    public void OnArmyButtonClicked()
    {
        string tag = "none";
        GameObject clickedObj = EventSystem.current.currentSelectedGameObject;
        if (clickedObj != null)
        {
            tag = clickedObj.tag;
        }
        switch (tag)
        {
            case "vanguard":
                armyImage = cm.vanguard;
                nameList = cm.vanguardUnit;
                break;
            case "rearguard":
                armyImage = cm.rearGuard;
                nameList = cm.rearGuardUnit;
                break;
            case "center":
                armyImage = cm.mainForces;
                nameList = cm.mainForcesUnit;
                break;
            case "leftwing":
                armyImage = cm.leftWing;
                nameList = cm.leftWingUnit;
                break;
            case "rightwing":
                armyImage = cm.rightWing;
                nameList = cm.rightWingUnit;
                break;
            default:
                break;
        }
    }

    public void X1()
    {
        clickAmount = 1;
        X1Btn.SetActive(false);
        X5Btn.SetActive(true);
        X10Btn.SetActive(false);    
    }
    public void X5()
    {
        clickAmount = 5;
        X1Btn.SetActive(false);
        X5Btn.SetActive(false);
        X10Btn.SetActive(true);
    }
    public void X10()
    {
        clickAmount = 10;
        X1Btn.SetActive(true);
        X5Btn.SetActive(false);
        X10Btn.SetActive(false);
    }

    public void OnAddUnitClicked_WithClickAmount()
    {
        for(int i = 0; i < clickAmount; i++)
        {
            OnAddUnitClicked();
        }
    }


    public void OnAddUnitClicked()
    {
        string tag = "none";
        GameObject clickedObj = EventSystem.current.currentSelectedGameObject;
        if (clickedObj != null)
        {
            tag = clickedObj.tag;
        }
        if (tag != "remove")
        {
            for (int i = 0; i < nameList.Count; i++)
            {
                if (nameList[i] != "none")
                {
                    continue;
                }
                else
                {
                    switch (tag)
                    {
                        case "knight":
                            if (currentNumber >= sts.unitAmountCap)
                            {
                                cm.tip.text = "You can't add any units because you've reached the unit cap in this battle.";
                            }
                            else if (sts.knightAmount <= 0)
                            {
                                cm.tip.text = "You don't have any knights.";

                            }
                            else
                            {
                                currentNumber++;
                                sts.knightAmount--;
                                cm.knightAmountText.text = "X" + sts.knightAmount.ToString();

                                nameList[i] = "knight";
                                armyImage[i].sprite = knightSprite;
                                numberUnitSet.text = currentNumber.ToString() + "/" + sts.unitAmountCap.ToString();
                            }
                            break;
                        case "archer":
                            if (currentNumber >= sts.unitAmountCap)
                            {
                                cm.tip.text = "You can't add any units because you've reached the unit cap in this battle.";

                            }
                            else if (sts.archerAmount <= 0)
                            {
                                cm.tip.text = "You don't have any archers.";
                            }
                            else
                            {
                                currentNumber++;
                                sts.archerAmount--;
                                cm.archerAmountText.text = "X" + sts.archerAmount.ToString();

                                nameList[i] = "archer";
                                armyImage[i].sprite = archerSprite;
                                numberUnitSet.text = currentNumber.ToString() + "/" + sts.unitAmountCap.ToString();

                            }
                            break;
                        case "halbert":
                            if (currentNumber >= sts.unitAmountCap)
                            {
                                cm.tip.text = "You can't add any units because you've reached the unit cap in this battle.";

                            }
                            else if (sts.halbertAmount <= 0)
                            {
                                cm.tip.text = "You don't have any halberds";

                            }
                            else
                            {
                                currentNumber++;
                                sts.halbertAmount--;
                                cm.halbertAmountText.text = "X" + sts.halbertAmount.ToString();

                                nameList[i] = "halbert";
                                armyImage[i].sprite = halbertSprite;
                                numberUnitSet.text = currentNumber.ToString() + "/" + sts.unitAmountCap.ToString();

                            }
                            break;
                        case "mage":
                            if (currentNumber >= sts.unitAmountCap)
                            {
                                cm.tip.text = "You can't add any units because you've reached the unit cap in this battle.";

                            }
                            else if (sts.mageAmount <= 0)
                            {
                                cm.tip.text = "You don't have any mages.";

                            }
                            else
                            {
                                currentNumber++;
                                sts.mageAmount--;
                                cm.mageAmountText.text = "X" + sts.mageAmount.ToString();

                                nameList[i] = "mage";
                                armyImage[i].sprite = mageSprite;
                                numberUnitSet.text = currentNumber.ToString() + "/" + sts.unitAmountCap.ToString();

                            }
                            break;
                        case "shieldman":
                            if (currentNumber >= sts.unitAmountCap)
                            {
                                cm.tip.text = "You can't add any units because you've reached the unit cap in this battle.";

                            }
                            else if (sts.shieldmanAmount <= 0)
                            {
                                cm.tip.text = "You don't have any shieldman";

                            }
                            else
                            {
                                currentNumber++;
                                sts.shieldmanAmount--;
                                cm.shieldmanAmountText.text = "X" + sts.shieldmanAmount.ToString();

                                nameList[i] = "shieldman";
                                armyImage[i].sprite = shieldmanSprite;
                                numberUnitSet.text = currentNumber.ToString() + "/" + sts.unitAmountCap.ToString();

                            }
                            break;
                        case "crossbow":
                            if (currentNumber >= sts.unitAmountCap)
                            {
                                cm.tip.text = "You can't add any units because you've reached the unit cap in this battle.";

                            }
                            else if (sts.crossbowAmount <= 0)
                            {
                                cm.tip.text = "You don't have any crossbows";

                            }
                            else
                            {
                                currentNumber++;
                                sts.crossbowAmount--;
                                cm.crossbowAmountText.text = "X" + sts.crossbowAmount.ToString();

                                nameList[i] = "crossbow";
                                armyImage[i].sprite = crossbowSprite;
                                numberUnitSet.text = currentNumber.ToString() + "/" + sts.unitAmountCap.ToString();

                            }
                            break;
                        case "archmage":
                            if (currentNumber >= sts.unitAmountCap)
                            {
                                cm.tip.text = "You can't add any units because you've reached the unit cap in this battle.";

                            }
                            else if (sts.archmageAmount <= 0)
                            {
                                cm.tip.text = "You don't have any archmages";

                            }
                            else
                            {
                                currentNumber++;
                                sts.archmageAmount--;
                                cm.archmageAmountText.text = "X" + sts.archmageAmount.ToString();

                                nameList[i] = "archmage";
                                armyImage[i].sprite = archmageSprite;
                                numberUnitSet.text = currentNumber.ToString() + "/" + sts.unitAmountCap.ToString();

                            }
                            break;
                        default:
                            break;
                    }
                    break;
                }
            }

        }
        else
        {
            for (int i = nameList.Count - 1; i >= 0; i--)
            {
                if (nameList[i] == "none")
                {
                    continue;
                }
                else
                {
                    switch (nameList[i])
                    {
                        case "knight":
                            sts.knightAmount++;
                            cm.knightAmountText.text = "X" + sts.knightAmount.ToString();
                            currentNumber--;
                            break;
                        case "archer":
                            sts.archerAmount++;
                            cm.archerAmountText.text = "X" + sts.archerAmount.ToString();
                            currentNumber--;
                            break;
                        case "halbert":
                            sts.halbertAmount++;
                            cm.halbertAmountText.text = "X" + sts.halbertAmount.ToString();

                            currentNumber--;
                            break;
                        case "archmage":
                            sts.archmageAmount++;
                            cm.archmageAmountText.text = "X" + sts.archmageAmount.ToString();

                            currentNumber--;
                            break;
                        case "shieldman":
                            sts.shieldmanAmount++;
                            cm.shieldmanAmountText.text = "X" + sts.shieldmanAmount.ToString();

                            currentNumber--;
                            break;
                        case "crossbow":
                            sts.crossbowAmount++;
                            cm.crossbowAmountText.text = "X" + sts.crossbowAmount.ToString();

                            currentNumber--;
                            break;
                        case "mage":
                            sts.mageAmount++;
                            cm.mageAmountText.text = "X" + sts.mageAmount.ToString();

                            currentNumber--;
                            break;
                        default:
                            break;
                    }
                    nameList[i] = "none";
                    armyImage[i].sprite = notSelectSprite;
                    numberUnitSet.text = currentNumber.ToString() + "/" + sts.unitAmountCap.ToString();
                    break;
                }
            }

        }


    }

    public void OpenEnemyArmy()
    {
        cm.enemyArmy.SetActive(true);
        cm.playerArmy.SetActive(false);

    }

    public void OpenPlayerArmy()
    {
        cm.enemyArmy.SetActive(false);
        cm.playerArmy.SetActive(true);
    }

    public void StartButton()
    {
        int mainforceNmber = 0;
        int leftwingNumber = 0;
        int rightwingNumber = 0;
        int vanguardNumber = 0;
        int rearguardNumber = 0;

        for (int i = 0; i < 30; i++)
        {
            if (cm.mainForcesUnit[i] != "none")
            {
                mainforceNmber++;
            }
            if (cm.leftWingUnit[i] != "none")
            {
                leftwingNumber++;
            }
            if (cm.rightWingUnit[i] != "none")
            {
                rightwingNumber++;
            }
            if (cm.vanguardUnit[i] != "none")
            {
                vanguardNumber++;
            }
            if (cm.rearGuardUnit[i] != "none")
            {
                rearguardNumber++;
            }
        }

        if (mainforceNmber == 0 && leftwingNumber == 0 && rightwingNumber == 0 && rearguardNumber == 0 && vanguardNumber == 0)
        {
            cm.tip.text = "You don't assign any units!";

            return;
        }

        for (int i = 0; i < 30; i++)
        {

            GameObject newPlayerObj1 = Instantiate(CovertStringToGameObject(cm.vanguardUnit[i], false), cm.vanguardTransforms[i].position, Quaternion.identity);
            gameManager.playerTeam.Add(newPlayerObj1);
            newPlayerObj1.transform.SetParent(playerTeam.transform);
            //newPlayerObj1.GetComponent<Unit>().exhaustValue = sts.playerData.exhaustedLvl;
            InsertDataToUnit(newPlayerObj1, i, false, "vanguard");


            GameObject newPlayerObj2 = Instantiate(CovertStringToGameObject(cm.rearGuardUnit[i], false), cm.rearGuardTransforms[i].position, Quaternion.identity);
            gameManager.playerTeam.Add(newPlayerObj2);
            newPlayerObj2.transform.SetParent(playerTeam.transform);
            InsertDataToUnit(newPlayerObj2, i, false, "rear");
            //newPlayerObj2.GetComponent<Unit>().exhaustValue = sts.playerData.exhaustedLvl;


            GameObject newPlayerObj3 = Instantiate(CovertStringToGameObject(cm.leftWingUnit[i], false), cm.leftWingTransforms[i].position, Quaternion.identity);
            gameManager.playerTeam.Add(newPlayerObj3);
            newPlayerObj3.transform.SetParent(playerTeam.transform);
            //newPlayerObj3.GetComponent<Unit>().exhaustValue = sts.playerData.exhaustedLvl;
            InsertDataToUnit(newPlayerObj3, i, false, "left");


            GameObject newPlayerObj4 = Instantiate(CovertStringToGameObject(cm.rightWingUnit[i], false), cm.rightWingTransforms[i].position, Quaternion.identity);
            gameManager.playerTeam.Add(newPlayerObj4);
            newPlayerObj4.transform.SetParent(playerTeam.transform);
            //newPlayerObj4.GetComponent<Unit>().exhaustValue = sts.playerData.exhaustedLvl;
            InsertDataToUnit(newPlayerObj4, i, false, "right");

            GameObject newPlayerObj5 = Instantiate(CovertStringToGameObject(cm.mainForcesUnit[i], false), cm.mainForcesTransforms[i].position, Quaternion.identity);
            gameManager.playerTeam.Add(newPlayerObj5);
            newPlayerObj5.transform.SetParent(playerTeam.transform);
            //newPlayerObj5.GetComponent<Unit>().exhaustValue = sts.playerData.exhaustedLvl;
            InsertDataToUnit(newPlayerObj5, i, false, "main");



            GameObject newEnemyObj1 = Instantiate(CovertStringToGameObject(cm.enemyVanguardUnit[i], true), cm.enemyVanguardTransforms[i].position, Quaternion.identity);
            gameManager.enemyTeam.Add(newEnemyObj1);
            newEnemyObj1.transform.SetParent(enemyTeam.transform);
            //newEnemyObj1.GetComponent<Unit>().exhaustValue = sts.enemyData.exhaustedLvl;if (newPlayerObj1.layer != LayerMask.NameToLayer("None"))
            InsertDataToUnit(newEnemyObj1, i, true, "vanguard");


            GameObject newEnemyObj2 = Instantiate(CovertStringToGameObject(cm.enemyLeftWingUnit[i], true), cm.enemyLeftWingTransforms[i].position, Quaternion.identity);
            gameManager.enemyTeam.Add(newEnemyObj2);
            newEnemyObj2.transform.SetParent(enemyTeam.transform);
            //newEnemyObj2.GetComponent<Unit>().exhaustValue = sts.enemyData.exhaustedLvl;
            InsertDataToUnit(newEnemyObj2, i, true, "left");


            GameObject newEnemyObj3 = Instantiate(CovertStringToGameObject(cm.enemyRightWingUnit[i], true), cm.enemyRightWingTransforms[i].position, Quaternion.identity);
            gameManager.enemyTeam.Add(newEnemyObj3);
            newEnemyObj3.transform.SetParent(enemyTeam.transform);
            //newEnemyObj3.GetComponent<Unit>().exhaustValue = sts.enemyData.exhaustedLvl;
            InsertDataToUnit(newEnemyObj3, i, true, "right");


            GameObject newEnemyObj4 = Instantiate(CovertStringToGameObject(cm.enemyMainForcesUnit[i], true), cm.enemyMainForcesTransforms[i].position, Quaternion.identity);
            gameManager.enemyTeam.Add(newEnemyObj4);
            newEnemyObj4.transform.SetParent(enemyTeam.transform);
            //newEnemyObj4.GetComponent<Unit>().exhaustValue = sts.enemyData.exhaustedLvl;
            InsertDataToUnit(newEnemyObj4, i,true,"main");

            GameObject newEnemyObj5 = Instantiate(CovertStringToGameObject(cm.enemyRearGuardUnit[i], true), cm.enemyReaGuardTransforms[i].position, Quaternion.identity);
            gameManager.enemyTeam.Add(newEnemyObj5);
            newEnemyObj5.transform.SetParent(enemyTeam.transform);
            //newEnemyObj5.GetComponent<Unit>().exhaustValue = sts.enemyData.exhaustedLvl;
            InsertDataToUnit(newEnemyObj5, i,true,"rear");



            Time.timeScale = 1f;
        }
        gameManager.startBattle = true;
        int e_count = 0;
        int p_count = 0;

        foreach (var unit in gameManager.enemyTeam)
        {
            if (unit.layer != LayerMask.NameToLayer("None"))
            {
                e_count++;
            }
        }

        foreach (var unit in gameManager.playerTeam)
        {
            if (unit.layer != LayerMask.NameToLayer("None"))
            {
                p_count++;
            }
        }
        gameManager.p_maxUnitCount = p_count;
        gameManager.e_maxUnitCount = e_count;
        gameManager.maxEnemy = e_count;
        Time.timeScale = 0f;
        SoundManager sm = FindObjectOfType<SoundManager>();
        if (sm != null)
        {
            sm.BattleMusic();
        }
        cm.strategyPhase.SetActive(false);
    }
    public void InsertDataToUnit(GameObject _unit, int i,bool isEnemy,string pos)
    {
        if (_unit.layer != LayerMask.NameToLayer("None"))
        {
            Unit newUnit = _unit.GetComponent<Unit>();
            if (_unit.layer == LayerMask.NameToLayer("Player"))
            {
                newUnit.armyLvlBonus = playerLvlBonus;
                newUnit.armyEncircleBonus = playerEncircleBonus;
            }

            if (_unit.layer == LayerMask.NameToLayer("Enemy"))
            {
                newUnit.armyLvlBonus = enemyLvlBonus;
                newUnit.armyEncircleBonus = enemyEncircleBonus;
            }
            newUnit.commandPost = isEnemy ?  e_post: p_post;
            newUnit.inArmy = pos;
            newUnit.matchingNumber = matching
                .Where(a => a.Key == i)
                .Select(b => b.Value)
                .FirstOrDefault();
        }
    }

    public GameObject CovertStringToGameObject(string _class, bool isEnemy)
    {
        if (!isEnemy)
        {
            switch (_class)
            {
                case "knight":
                    return knightUnit;
                case "archer":
                    return archerUnit;
                case "halbert":
                    return halbertUnit;
                case "shieldman":
                    return shieldmanUnit;
                case "mage":
                    return mageUnit;
                case "archmage":
                    return archmageUnit;
                case "crossbow":
                    return crossbowUnit;
                default:
                    return none;

            }
        }
        else
        {
            switch (_class)
            {
                case "knight":
                    return enemyKnightUnit;
                case "archer":
                    return enemyArcherUnit;
                case "halbert":
                    return enemyHalbertUnit;
                case "shieldman":
                    return enemyShieldmanUnit;
                case "mage":
                    return enemyMageUnit;
                case "archmage":
                    return enemyArchmageUnit;
                case "crossbow":
                    return enemyCrossbowUnit;
                default:
                    return none;
            }
        }


    }
    void EncircleBonusCalculate()
    {
        switch (sts.enemyEncircleBonus)
        {
            case 1:
                enemyEncircleBonus = 0f;
                break;
            case 2:
                enemyEncircleBonus = 0.15f;
                break;
            case 3:
                enemyEncircleBonus = 0.3f;
                break;
            case 4:
                enemyEncircleBonus = 0.5f;
                break;
            default:
                enemyEncircleBonus = 0f;

                break;
        }

        switch (sts.playerEncircleBonus)
        {
            case 1:
                playerEncircleBonus = 0f;
                break;
            case 2:
                playerEncircleBonus = 0.15f;

                break;
            case 3:
                playerEncircleBonus = 0.3f;

                break;
            case 4:
                playerEncircleBonus = 0.5f;
                break;
            default:
                playerEncircleBonus = 0f;
                break;
        }
    }

    void ShowAdditional()
    {
        cm.enemyEncircleBonus.text = Mathf.RoundToInt(enemyEncircleBonus * 100).ToString() + "%";
        cm.enemyArmyLvl.text = sts.enemyLvl.ToString();
        cm.enemyLvlBonus.text = Mathf.RoundToInt(enemyLvlBonus * 100).ToString() + "%";

        cm.playerArmyLvl.text = sts._refArmyInfor.lvl.ToString();
        cm.playerEncircleBonus.text = Mathf.RoundToInt(playerEncircleBonus * 100).ToString() + "%";
        cm.playerLvlBonus.text = Mathf.RoundToInt(playerLvlBonus * 100).ToString() + "%";

        numberUnitSet.text = 0 + "/" + sts.unitAmountCap.ToString();
    }

    public void SetUpEnemyArmy()
    {
        switch (battleMap)
        {
            //Mist mountain Campaign.Recommend lvl = 1
            case "MistMountain_0":
                switch (RandomNumber())
                {
                    case 1:
                        cm.enemyMainForcesUnit = enemyLineup.halbert_8;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 2:
                        cm.enemyMainForcesUnit = enemyLineup.halbert_5_Crossbow_3;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 3:
                        cm.enemyMainForcesUnit = enemyLineup.halbert_5_Crossbow_2_Archmage_1;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    default:
                        cm.enemyMainForcesUnit = enemyLineup.halbert_5_Crossbow_2_Archmage_1;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                }
                break;
            //////////////////////////////////////////////////////
            case "MistMountain_Boss":
                switch (RandomNumber())
                {
                    case 1:
                        cm.enemyMainForcesUnit = enemyLineup.halbert_15_Crossbow_7;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 2:
                        cm.enemyMainForcesUnit = enemyLineup.all_None;
                        cm.enemyLeftWingUnit = enemyLineup.halbert_8_mage_2_Archmage_1;
                        cm.enemyRightWingUnit = enemyLineup.halbert_8_mage_2_Archmage_1;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 3:
                        cm.enemyMainForcesUnit = enemyLineup.all_None;
                        cm.enemyLeftWingUnit = enemyLineup.halbert_10_Mage_1;
                        cm.enemyRightWingUnit = enemyLineup.halbert_10_Mage_1;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    default:
                        cm.enemyMainForcesUnit = enemyLineup.halbert_15_Crossbow_7;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                }
                break;
            ///////////////////////////////////////////////////////////////
            //Dawn Valley Campaign. Lvl recommend = 5;
            case "DawnValley_0":
                switch (RandomNumber())
                {
                    case 1:
                        cm.enemyMainForcesUnit = enemyLineup.knight_13;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 2:
                        cm.enemyMainForcesUnit = enemyLineup.knight_7_Crossbow_6;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 3:
                        cm.enemyMainForcesUnit = enemyLineup.archer_13;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    default:
                        cm.enemyMainForcesUnit = enemyLineup.knight_13;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                }
                break;
            ///////////////////////////////////////////////
            case "DawnValley_Hard":
                switch (RandomNumber())
                {
                    case 1:
                        cm.enemyMainForcesUnit = enemyLineup.knight_16;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 2:
                        cm.enemyMainForcesUnit = enemyLineup.halbert_8_crossbow_8;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 3:
                        cm.enemyMainForcesUnit = enemyLineup.halbert_12_Mage_2_Archmage_2;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    default:
                        cm.enemyMainForcesUnit = enemyLineup.halbert_12_Mage_2_Archmage_2;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                }
                break;
            //////////////////////////////////////////
            case "DawnValley_Veryhard":
                switch (RandomNumber())
                {
                    case 1:
                        cm.enemyMainForcesUnit = enemyLineup.all_None;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.knight_10;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.knight_10;
                        break;
                    case 2:
                        cm.enemyMainForcesUnit = enemyLineup.all_None;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.archmage_5_Mage_5;
                        cm.enemyRearGuardUnit = enemyLineup.knight_10;
                        break;
                    case 3:
                        cm.enemyMainForcesUnit = enemyLineup.shieldman_10;
                        cm.enemyLeftWingUnit = enemyLineup.knight_5;
                        cm.enemyRightWingUnit = enemyLineup.knight_5;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    default:
                        cm.enemyMainForcesUnit = enemyLineup.shieldman_10;
                        cm.enemyLeftWingUnit = enemyLineup.knight_5;
                        cm.enemyRightWingUnit = enemyLineup.knight_5;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                }
                break;
            //////////////////////////////////////////////
            case "DawnValley_Boss":
                switch (RandomNumber())
                {
                    case 1:
                        cm.enemyMainForcesUnit = enemyLineup.mage2_Archmage_3;
                        cm.enemyLeftWingUnit = enemyLineup.knight_10;
                        cm.enemyRightWingUnit = enemyLineup.knight_10;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.knight_10;
                        break;
                    case 2:
                        cm.enemyMainForcesUnit = enemyLineup.mage2_Archmage_3;
                        cm.enemyLeftWingUnit = enemyLineup.halbert_10_archer_5;
                        cm.enemyRightWingUnit = enemyLineup.halbert_10_Crossbow_5;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 3:
                        cm.enemyMainForcesUnit = enemyLineup.archer_25;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.shieldman_15;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    default:
                        cm.enemyMainForcesUnit = enemyLineup.mage2_Archmage_3;
                        cm.enemyLeftWingUnit = enemyLineup.halbert_10_archer_5;
                        cm.enemyRightWingUnit = enemyLineup.halbert_10_Crossbow_5;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                }
                break;
            ///////////////////////////////////////////////////////
            case "DawnValley_Epic":

                cm.enemyMainForcesUnit = enemyLineup.archer_30;
                cm.enemyLeftWingUnit = enemyLineup.halbert_10_Crossbow_10_Knight_10;
                cm.enemyRightWingUnit = enemyLineup.halbert_10_Crossbow_10_Knight_10;
                cm.enemyVanguardUnit = enemyLineup.halbert_15_Crossbow_15;
                cm.enemyRearGuardUnit = enemyLineup.knight_30;
                break;
            ////////////////////////////////////////////////
            //StrongRiver Campaign. Recommend lvl = 10
            case "StrongsRiver_0":
                switch (RandomNumber())
                {
                    case 1:
                        cm.enemyMainForcesUnit = enemyLineup.all_None;
                        cm.enemyLeftWingUnit = enemyLineup.knight_6;
                        cm.enemyRightWingUnit = enemyLineup.knight_6;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.knight_6;
                        break;
                    case 2:
                        cm.enemyMainForcesUnit = enemyLineup.halbert_5_Mage_6_Archmage_7;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 3:
                        cm.enemyMainForcesUnit = enemyLineup.halbert_10;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.archmage_8;
                        break;
                    default:
                        cm.enemyMainForcesUnit = enemyLineup.halbert_10;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.archmage_8;
                        break;
                }
                break;
            ///////////////////////////////////////////////
            case "StrongsRiver_Hard":
                switch (RandomNumber())
                {
                    case 1:
                        cm.enemyMainForcesUnit = enemyLineup.all_None;
                        cm.enemyLeftWingUnit = enemyLineup.halbert_8_Archmage_3;
                        cm.enemyRightWingUnit = enemyLineup.halbert_8_Mage_3;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.knight_6;
                        break;
                    case 2:
                        cm.enemyMainForcesUnit = enemyLineup.all_None;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.knight_10;
                        cm.enemyVanguardUnit = enemyLineup.halbert_4_mage_4_Archmage_4;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 3:
                        cm.enemyMainForcesUnit = enemyLineup.all_None;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.halbert_15_Crossbow_7;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    default:
                        cm.enemyMainForcesUnit = enemyLineup.all_None;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.halbert_15_Crossbow_7;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                }
                break;
            ////////////////////////////////////////////////
            case "StrongsRiver_Veryhard":
                switch (RandomNumber())
                {
                    case 1:
                        cm.enemyMainForcesUnit = enemyLineup.all_None;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.halbert_10_Crossbow_10_Archer_10;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 2:
                        cm.enemyMainForcesUnit = enemyLineup.all_None;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.halbert_10_Mage_10_Archer_10;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 3:
                        cm.enemyMainForcesUnit = enemyLineup.all_None;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.halbert_10_Archmage_10_Archer_10;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    default:
                        cm.enemyMainForcesUnit = enemyLineup.all_None;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.halbert_10_Archmage_10_Archer_10;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                }
                break;
            /////////////////////////////////////////////////
            case "StrongsRiver_Boss":
                switch (RandomNumber())
                {
                    case 1:
                        cm.enemyMainForcesUnit = enemyLineup.shieldman_10_Archmage_5_Mage_5;
                        cm.enemyLeftWingUnit = enemyLineup.halbert_5_crossbow_5_knight_5;
                        cm.enemyRightWingUnit = enemyLineup.halbert_5_crossbow_5_knight_5;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 2:
                        cm.enemyMainForcesUnit = enemyLineup.all_None;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.shieldman_20;
                        cm.enemyRearGuardUnit = enemyLineup.archer_30;
                        break;
                    case 3:
                        cm.enemyMainForcesUnit = enemyLineup.all_None;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.halbert_30;
                        cm.enemyRearGuardUnit = enemyLineup.knight_20;
                        break;
                    default:
                        cm.enemyMainForcesUnit = enemyLineup.all_None;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.halbert_30;
                        cm.enemyRearGuardUnit = enemyLineup.knight_20;
                        break;
                }
                break;
            //////////////////////////////////////////////////
            case "StrongsRiver_Epic":

                cm.enemyMainForcesUnit = enemyLineup.halbert_10_Mage_5_Archmage_5_Crossbow_10;
                cm.enemyLeftWingUnit = enemyLineup.halbert_10_Mage_5_Archmage_5_Crossbow_10;
                cm.enemyRightWingUnit = enemyLineup.halbert_10_Mage_5_Archmage_5_Crossbow_10;
                cm.enemyVanguardUnit = enemyLineup.halbert_10_Mage_5_Archmage_5_Crossbow_10;
                cm.enemyRearGuardUnit = enemyLineup.halbert_10_Mage_5_Archmage_5_Crossbow_10;
                break;
            /////////////////////////////////////////////////
            //Deadkingdom Campaign. Recommend lvl = 15
            case "Deadkingdom_0":
                switch (RandomNumber())
                {
                    case 1:
                        cm.enemyMainForcesUnit = enemyLineup.halbert_15_Crossbow_7;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 2:
                        cm.enemyMainForcesUnit = enemyLineup.halbert_10_Armage_5_Archer_7;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 3:
                        cm.enemyMainForcesUnit = enemyLineup.shieldman_10_Armage_5_Knight_7;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    default:
                        cm.enemyMainForcesUnit = enemyLineup.shieldman_10_Armage_5_Knight_7;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                }
                break;
            ///////////////////////////////////////////////
            case "Deadkingdom_Hard":
                switch (RandomNumber())
                {
                    case 1:
                        cm.enemyMainForcesUnit = enemyLineup.knight_11;
                        cm.enemyLeftWingUnit = enemyLineup.knight_8;
                        cm.enemyRightWingUnit = enemyLineup.knight_8;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 2:
                        cm.enemyMainForcesUnit = enemyLineup.knight_7_Crossbow_6;
                        cm.enemyLeftWingUnit = enemyLineup.knight_14;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 3:
                        cm.enemyMainForcesUnit = enemyLineup.all_None;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.shieldman_10;
                        cm.enemyRearGuardUnit = enemyLineup.halbert_5_Mage_6_Archmage_7;
                        break;
                    default:
                        cm.enemyMainForcesUnit = enemyLineup.knight_11;
                        cm.enemyLeftWingUnit = enemyLineup.knight_8;
                        cm.enemyRightWingUnit = enemyLineup.knight_8;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                }
                break;
            ///////////////////////////////////////////////
            case "Deadkingdom_Veryhard":
                switch (RandomNumber())
                {
                    case 1:
                        cm.enemyMainForcesUnit = enemyLineup.halbert_6_mage_3_Archmage_3;
                        cm.enemyLeftWingUnit = enemyLineup.knight_6_mage_3_Archmage_3;
                        cm.enemyRightWingUnit = enemyLineup.crossbow_6_mage_3_Archmage_3;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 2:
                        cm.enemyMainForcesUnit = enemyLineup.all_None;
                        cm.enemyLeftWingUnit = enemyLineup.mage_12;
                        cm.enemyRightWingUnit = enemyLineup.archmage_12;
                        cm.enemyVanguardUnit = enemyLineup.crossbow_12;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 3:
                        cm.enemyMainForcesUnit = enemyLineup.shieldman_6;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.archer_30;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    default:
                        cm.enemyMainForcesUnit = enemyLineup.all_None;
                        cm.enemyLeftWingUnit = enemyLineup.mage_12;
                        cm.enemyRightWingUnit = enemyLineup.archmage_12;
                        cm.enemyVanguardUnit = enemyLineup.crossbow_12;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                }
                break;
            ////////////////////////////////////////////////
            case "Deadkingdom_Boss":
                switch (RandomNumber())
                {
                    case 1:
                        cm.enemyMainForcesUnit = enemyLineup.halbert_15_Crossbow_7;
                        cm.enemyLeftWingUnit = enemyLineup.knight_15_mage_7;
                        cm.enemyRightWingUnit = enemyLineup.shieldman_7_Archer_15;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 2:
                        cm.enemyMainForcesUnit = enemyLineup.crossbow_22;
                        cm.enemyLeftWingUnit = enemyLineup.knight_22;
                        cm.enemyRightWingUnit = enemyLineup.halbert_22;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 3:
                        cm.enemyMainForcesUnit = enemyLineup.archer_30;
                        cm.enemyLeftWingUnit = enemyLineup.knight_18;
                        cm.enemyRightWingUnit = enemyLineup.knight_18;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    default:
                        cm.enemyMainForcesUnit = enemyLineup.archer_30;
                        cm.enemyLeftWingUnit = enemyLineup.knight_18;
                        cm.enemyRightWingUnit = enemyLineup.knight_18;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                }
                break;
            ////////////////////////////////////////////////         

            case "Deadkingdom_Epic":
                cm.enemyMainForcesUnit = enemyLineup.halbert_15_Crossbow_15;
                cm.enemyLeftWingUnit = enemyLineup.archer_15_knight_15;
                cm.enemyRightWingUnit = enemyLineup.archer_15_knight_15;
                cm.enemyVanguardUnit = enemyLineup.shieldman_15_Armage_15;
                cm.enemyRearGuardUnit = enemyLineup.knight_30;
                break;
            /////////////////////////////////////////////////
            case "Skyfall_0":
                switch (RandomNumber())
                {
                    case 1:
                        cm.enemyMainForcesUnit = enemyLineup.halbert_7_Archmage_4_Mage_4;
                        cm.enemyLeftWingUnit = enemyLineup.halbert_7_Archer_8;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 2:
                        cm.enemyMainForcesUnit = enemyLineup.all_None;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.halbert_10;
                        cm.enemyRearGuardUnit = enemyLineup.knight_20;
                        break;
                    case 3:
                        cm.enemyMainForcesUnit = enemyLineup.all_None;
                        cm.enemyLeftWingUnit = enemyLineup.crossbow_8_knight_7;
                        cm.enemyRightWingUnit = enemyLineup.halbert_8_knight_7;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    default:
                        cm.enemyMainForcesUnit = enemyLineup.all_None;
                        cm.enemyLeftWingUnit = enemyLineup.crossbow_8_knight_7;
                        cm.enemyRightWingUnit = enemyLineup.halbert_8_knight_7;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                }
                break;
            /////////////////////////////////////////////////
            case "Skyfall_Hard":
                switch (RandomNumber())
                {
                    case 1:
                        cm.enemyMainForcesUnit = enemyLineup.mage_4_Archmage_4_Knight_5;
                        cm.enemyLeftWingUnit = enemyLineup.halbert_5_Knight_5;
                        cm.enemyRightWingUnit = enemyLineup.crossbow_5_Knight_5;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 2:
                        cm.enemyMainForcesUnit = enemyLineup.shieldman_4_Archer_14;
                        cm.enemyLeftWingUnit = enemyLineup.halbert_8_knight_7;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 3:
                        cm.enemyMainForcesUnit = enemyLineup.all_None;
                        cm.enemyLeftWingUnit = enemyLineup.shieldman_4_Archmage_3_Mage_3;
                        cm.enemyRightWingUnit = enemyLineup.shieldman_4_Crossbow_6;
                        cm.enemyVanguardUnit = enemyLineup.shieldman_4_Archmage_3_Archer_6;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    default:
                        cm.enemyMainForcesUnit = enemyLineup.all_None;
                        cm.enemyLeftWingUnit = enemyLineup.shieldman_4_Archmage_3_Mage_3;
                        cm.enemyRightWingUnit = enemyLineup.shieldman_4_Crossbow_6;
                        cm.enemyVanguardUnit = enemyLineup.shieldman_4_Archmage_3_Archer_6;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                }
                break;
            /////////////////////////////////////////////////
            case "Skyfall_Veryhard":
                switch (RandomNumber())
                {
                    case 1:
                        cm.enemyMainForcesUnit = enemyLineup.shieldman_5_Crossbow_5__Knight_13;
                        cm.enemyLeftWingUnit = enemyLineup.halbert_10_Mage_2_Archmage_2_Crossbow_6;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 2:
                        cm.enemyMainForcesUnit = enemyLineup.halbert_23;
                        cm.enemyLeftWingUnit = enemyLineup.all_None;
                        cm.enemyRightWingUnit = enemyLineup.all_None;
                        cm.enemyVanguardUnit = enemyLineup.Archmage_20;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 3:
                        cm.enemyMainForcesUnit = enemyLineup.halbert_10_Mage_7;
                        cm.enemyLeftWingUnit = enemyLineup.halbert_7_Mage_3;
                        cm.enemyRightWingUnit = enemyLineup.halbert_7_Mage_3;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    default:
                        cm.enemyMainForcesUnit = enemyLineup.halbert_10_Mage_7;
                        cm.enemyLeftWingUnit = enemyLineup.halbert_7_Mage_3;
                        cm.enemyRightWingUnit = enemyLineup.halbert_7_Mage_3;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                }
                break;
            /////////////////////////////////////////////////
            case "Skyfall_Boss":
                switch (RandomNumber())
                {
                    case 1:
                        cm.enemyMainForcesUnit = enemyLineup.halbert_10_Crossbow_10;
                        cm.enemyLeftWingUnit = enemyLineup.knight_10_Mage_5_Archer_5;
                        cm.enemyRightWingUnit = enemyLineup.knight_10_Mage_5_Archer_5;
                        cm.enemyVanguardUnit = enemyLineup.shieldman_10_Archmage_10;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 2:
                        cm.enemyMainForcesUnit = enemyLineup.archer_20;
                        cm.enemyLeftWingUnit = enemyLineup.halbert_15_Knight_15;
                        cm.enemyRightWingUnit = enemyLineup.halbert_15_Knight_15;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    case 3:
                        cm.enemyMainForcesUnit = enemyLineup.shieldman_10_Archer_15_Armage_5;
                        cm.enemyLeftWingUnit = enemyLineup.knight_20;
                        cm.enemyRightWingUnit = enemyLineup.halbert_10_Crossbow_5_Mage_5_Archmage_5_Archer_5;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                    default:
                        cm.enemyMainForcesUnit = enemyLineup.shieldman_10_Archer_15_Armage_5;
                        cm.enemyLeftWingUnit = enemyLineup.knight_20;
                        cm.enemyRightWingUnit = enemyLineup.halbert_10_Crossbow_5_Mage_5_Archmage_5_Archer_5;
                        cm.enemyVanguardUnit = enemyLineup.all_None;
                        cm.enemyRearGuardUnit = enemyLineup.all_None;
                        break;
                }
                break;
            /////////////////////////////////////////////////
            case "Skyfall_Epic":


                cm.enemyMainForcesUnit = enemyLineup.archer_15_Knight_15;
                cm.enemyLeftWingUnit = enemyLineup.shieldman_10_Archmage_5_Mage_5_Archer_10;
                cm.enemyRightWingUnit = enemyLineup.shieldman_10_Archmage_5_Mage_5_Archer_10;
                cm.enemyVanguardUnit = enemyLineup.knight_30;
                cm.enemyRearGuardUnit = enemyLineup.shieldman_10_Archmage_5_Mage_5_Archer_10;
                break;
            /////////////////////////////////////////////////
            //Villager undetattack
            case "villager_underattack":
                cm.enemyMainForcesUnit = enemyLineup.archer_5;
                cm.enemyLeftWingUnit = enemyLineup.all_None;
                cm.enemyRightWingUnit = enemyLineup.all_None;
                cm.enemyVanguardUnit = enemyLineup.all_None;
                cm.enemyRearGuardUnit = enemyLineup.all_None;
                break;
            //Ice Cave Campaign
            case "IceCave":
                cm.enemyMainForcesUnit = enemyLineup.crossbow_5;
                cm.enemyLeftWingUnit = enemyLineup.all_None;
                cm.enemyRightWingUnit = enemyLineup.all_None;
                cm.enemyVanguardUnit = enemyLineup.all_None;
                cm.enemyRearGuardUnit = enemyLineup.all_None;
                break;
            case "IceCave_Last":
                cm.enemyMainForcesUnit = enemyLineup.crossbow_5;
                cm.enemyLeftWingUnit = enemyLineup.all_None;
                cm.enemyRightWingUnit = enemyLineup.all_None;
                cm.enemyVanguardUnit = enemyLineup.all_None;
                cm.enemyRearGuardUnit = enemyLineup.all_None;
                break;
            default:
                cm.enemyMainForcesUnit = enemyLineup.halbert_10_Crossbow_10;
                cm.enemyLeftWingUnit = enemyLineup.all_None;
                cm.enemyRightWingUnit = enemyLineup.all_None;
                cm.enemyVanguardUnit = enemyLineup.all_None;
                cm.enemyRearGuardUnit = enemyLineup.all_None;
                break;
        }

    }

    public int RandomNumber()
    {
        int randomNum = Random.Range(0, 101);
        if (randomNum <= 33)
        {
            return 1;
        }
        else if (randomNum <= 66)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }





}






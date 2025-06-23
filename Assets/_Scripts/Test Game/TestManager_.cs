using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class TestManager_ : MonoBehaviour
{
    private TestCanvasManager tcm;
    private GameManager manager;
    public GameObject p_post;
    public GameObject e_post;
    public List<string> nameList;
    public List<Image> armyImage;
    public bool isPause;

    [SerializeField] private int MaxUnit;
    [SerializeField] private int currentNumber;
    public int clickAmount = 1;

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

    public Dictionary<int, int> matching = new Dictionary<int, int>()
    {
        { 0, 21 },{ 1, 20 },{ 2, 23 },{ 3, 22 },{ 4, 25},
        { 5, 4 },{ 6, 27 },{ 7, 26 },{ 8, 29 },{ 9, 28},
        { 10, 11 },{ 11, 10 },{ 12, 13 },{ 13, 12 },{ 14, 15 },
        { 15, 14 },{ 16, 17 },{ 17, 16},{ 18, 19},{ 19, 18 },
        { 20, 1 },{ 21, 0 },{ 22, 3 },{ 23, 2 },{ 24, 5 },
        { 25, 4 },{ 26, 7 },{ 27, 6 },{ 28, 9 },{ 29, 8 }
    };

    private bool updateScore;
    private int e_maxUnitCount;
    private int p_maxUnitCount;


    private void Update()
    {
        if (!updateScore)
        {
            updateScore = true;
            StartCoroutine(UpdateScore());
        }
    }

    IEnumerator UpdateScore()
    {
        tcm.playerUnits.text = manager.playerTeam.Count.ToString() +"/" +p_maxUnitCount.ToString();
        tcm.enemyUnits.text = manager.enemyTeam.Count.ToString() + "/" + e_maxUnitCount.ToString();

        yield return new WaitForSeconds(0.5f);
        updateScore = false;
    }




    void Start()
    {
        tcm = FindObjectOfType<TestCanvasManager>();
        manager = FindObjectOfType<GameManager>();
        playerTeam = GameObject.Find("Player Force");
        enemyTeam = GameObject.Find("Enemy Force");
        X1();
        _Reset();
        SoundManager sm = FindObjectOfType<SoundManager>();
        if (sm != null)
        {
            sm.StrategyInterfaceMusic();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Test Scene");
    }

    public void OnPause()
    {
        Time.timeScale = 0f;
        tcm.pauseButton.SetActive(false);
        tcm.unpauseButton.SetActive(true);
    }

        public void OnUnpause()
    {
        Time.timeScale = 1f;
        tcm.pauseButton.SetActive(true);
        tcm.unpauseButton.SetActive(false);
    }

    public void OpenStrategyScreen()
    {
        tcm.strategyPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseStrategyScreen()
    {
        tcm.strategyPanel.SetActive(false);
        if (tcm.pauseButton.activeSelf)
            Time.timeScale = 1f;
    }

    public void SetUpStart()
    {
        _Reset();
    }

    public void _Reset()
    {
        tcm.p_mainString = Enumerable.Repeat("none", 30).ToList();
        tcm.p_vanguardString = Enumerable.Repeat("none", 30).ToList();
        tcm.p_rearString = Enumerable.Repeat("none", 30).ToList();
        tcm.p_leftString = Enumerable.Repeat("none", 30).ToList();
        tcm.p_rightString = Enumerable.Repeat("none", 30).ToList();

        tcm.e_mainString = Enumerable.Repeat("none", 30).ToList();
        tcm.e_vanguardString = Enumerable.Repeat("none", 30).ToList();
        tcm.e_rearString = Enumerable.Repeat("none", 30).ToList();
        tcm.e_leftString = Enumerable.Repeat("none", 30).ToList();
        tcm.e_rightString = Enumerable.Repeat("none", 30).ToList();
    }

    public Sprite CovertStringToSprite(string _class)
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
    public void X1()
    {
        clickAmount = 1;
        tcm.x1.SetActive(false);
        tcm.x5.SetActive(true);
        tcm.x10.SetActive(false);

    }

    public void X5()
    {
        clickAmount = 5;
        tcm.x1.SetActive(false);
        tcm.x5.SetActive(false);
        tcm.x10.SetActive(true);

    }
    public void X10()
    {
        clickAmount = 10;
        tcm.x1.SetActive(true);
        tcm.x5.SetActive(false);
        tcm.x10.SetActive(false);
    }
    public void _OnAddUnitClicked()
    {
        for(int i = 0; i< clickAmount; i++)
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
                            nameList[i] = "knight";
                            armyImage[i].sprite = knightSprite;
                            break;
                        case "archer":
                            nameList[i] = "archer";
                            armyImage[i].sprite = archerSprite;
                            break;
                        case "halbert":
                            nameList[i] = "halbert";
                            armyImage[i].sprite = halbertSprite;
                            break;
                        case "mage":
                            nameList[i] = "mage";
                            armyImage[i].sprite = mageSprite;
                            break;
                        case "shieldman":


                            nameList[i] = "shieldman";
                            armyImage[i].sprite = shieldmanSprite;

                            break;
                        case "crossbow":
                            nameList[i] = "crossbow";
                            armyImage[i].sprite = crossbowSprite;
                            break;
                        case "archmage":
                            nameList[i] = "archmage";
                            armyImage[i].sprite = archmageSprite;
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

                nameList[i] = "none";
                armyImage[i].sprite = notSelectSprite;
                break;
            }
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
    public void OpenPlayerScreen()
    {
        tcm.playerScreen.SetActive(true);
        tcm.enemyScreen.SetActive(false);
    }

    public void OpenEnemyScreen()
    {
        tcm.playerScreen.SetActive(false);
        tcm.enemyScreen.SetActive(true);
    }
    public void OnPlayerArmyButtonClicked()
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
                armyImage = tcm.p_vanguardImg;
                nameList = tcm.p_vanguardString;
                break;
            case "rearguard":
                armyImage = tcm.p_rearImg;
                nameList = tcm.p_rearString;
                break;
            case "center":
                armyImage = tcm.p_mainImg;
                nameList = tcm.p_mainString;
                break;
            case "leftwing":
                armyImage = tcm.p_leftImg;
                nameList = tcm.p_leftString;
                break;
            case "rightwing":
                armyImage = tcm.p_rightImg;
                nameList = tcm.p_rightString;
                break;
            default:
                break;
        }
    }

    public void StartButton()
    {
        int p_main = 0;
        int p_left = 0;
        int p_right = 0;
        int p_rear = 0;
        int p_vanguard = 0;

        int e_main = 0;
        int e_left = 0;
        int e_right = 0;
        int e_rear = 0;
        int e_vanguard = 0;

        for (int i = 0; i < 30; i++)
        {
            if (tcm.p_mainString[i] != "none")
            {
                p_main++;
            }
            if (tcm.p_leftString[i] != "none")
            {
                p_left++;
            }
            if (tcm.p_rightString[i] != "none")
            {
                p_right++;
            }
            if (tcm.p_rearString[i] != "none")
            {
                p_rear++;
            }
            if (tcm.p_vanguardString[i] != "none")
            {
                p_vanguard++;
            }

            if (tcm.e_mainString[i] != "none")
            {
                e_main++;
            }
            if (tcm.e_leftString[i] != "none")
            {
                e_left++;
            }
            if (tcm.e_rightString[i] != "none")
            {
                e_right++;
            }
            if (tcm.e_rearString[i] != "none")
            {
                e_rear++;
            }
            if (tcm.e_vanguardString[i] != "none")
            {
                e_rear++;
            }
        }

        if (p_main == 0 && p_left == 0 && p_right == 0 && p_rear == 0 && p_vanguard == 0)
        {
            Debug.Log("You don't assign any units!");
            return;
        }

        if (e_main == 0 && e_left == 0 && e_right == 0 && e_rear == 0 && e_vanguard == 0)
        {
            Debug.Log("Enemy doesn't assign any units");
            return;
        }

        for (int i = 0; i < 30; i++)
        {

            GameObject newPlayerObj1 = Instantiate(CovertStringToGameObject(tcm.p_vanguardString[i], false), tcm.p_vanguardObj[i].position, Quaternion.identity);
            manager.playerTeam.Add(newPlayerObj1);
            newPlayerObj1.transform.SetParent(playerTeam.transform);
            //newPlayerObj1.GetComponent<Unit>().exhaustValue = sts.playerData.exhaustedLvl;
            InsertDataToUnit(newPlayerObj1, i, false, "vanguard");


            GameObject newPlayerObj2 = Instantiate(CovertStringToGameObject(tcm.p_rearString[i], false), tcm.p_rearObj[i].position, Quaternion.identity);
            manager.playerTeam.Add(newPlayerObj2);
            newPlayerObj2.transform.SetParent(playerTeam.transform);
            InsertDataToUnit(newPlayerObj2, i, false, "rear");
            //newPlayerObj2.GetComponent<Unit>().exhaustValue = sts.playerData.exhaustedLvl;


            GameObject newPlayerObj3 = Instantiate(CovertStringToGameObject(tcm.p_leftString[i], false), tcm.p_leftObj[i].position, Quaternion.identity);
            manager.playerTeam.Add(newPlayerObj3);
            newPlayerObj3.transform.SetParent(playerTeam.transform);
            //newPlayerObj3.GetComponent<Unit>().exhaustValue = sts.playerData.exhaustedLvl;
            InsertDataToUnit(newPlayerObj3, i, false, "left");


            GameObject newPlayerObj4 = Instantiate(CovertStringToGameObject(tcm.p_rightString[i], false), tcm.p_rightObj[i].position, Quaternion.identity);
            manager.playerTeam.Add(newPlayerObj4);
            newPlayerObj4.transform.SetParent(playerTeam.transform);
            //newPlayerObj4.GetComponent<Unit>().exhaustValue = sts.playerData.exhaustedLvl;
            InsertDataToUnit(newPlayerObj4, i, false, "right");

            GameObject newPlayerObj5 = Instantiate(CovertStringToGameObject(tcm.p_mainString[i], false), tcm.p_mainObj[i].position, Quaternion.identity);
            manager.playerTeam.Add(newPlayerObj5);
            newPlayerObj5.transform.SetParent(playerTeam.transform);
            //newPlayerObj5.GetComponent<Unit>().exhaustValue = sts.playerData.exhaustedLvl;
            InsertDataToUnit(newPlayerObj5, i, false, "main");



            GameObject newEnemyObj1 = Instantiate(CovertStringToGameObject(tcm.e_vanguardString[i], true), tcm.e_vanguardObj[i].position, Quaternion.identity);
            manager.enemyTeam.Add(newEnemyObj1);
            newEnemyObj1.transform.SetParent(enemyTeam.transform);
            //newEnemyObj1.GetComponent<Unit>().exhaustValue = sts.enemyData.exhaustedLvl;if (newPlayerObj1.layer != LayerMask.NameToLayer("None"))
            InsertDataToUnit(newEnemyObj1, i, true, "vanguard");


            GameObject newEnemyObj2 = Instantiate(CovertStringToGameObject(tcm.e_leftString[i], true), tcm.e_leftObj[i].position, Quaternion.identity);
            manager.enemyTeam.Add(newEnemyObj2);
            newEnemyObj2.transform.SetParent(enemyTeam.transform);
            //newEnemyObj2.GetComponent<Unit>().exhaustValue = sts.enemyData.exhaustedLvl;
            InsertDataToUnit(newEnemyObj2, i, true, "left");


            GameObject newEnemyObj3 = Instantiate(CovertStringToGameObject(tcm.e_rightString[i], true), tcm.e_rightObj[i].position, Quaternion.identity);
            manager.enemyTeam.Add(newEnemyObj3);
            newEnemyObj3.transform.SetParent(enemyTeam.transform);
            //newEnemyObj3.GetComponent<Unit>().exhaustValue = sts.enemyData.exhaustedLvl;
            InsertDataToUnit(newEnemyObj3, i, true, "right");


            GameObject newEnemyObj4 = Instantiate(CovertStringToGameObject(tcm.e_mainString[i], true), tcm.e_mainObj[i].position, Quaternion.identity);
            manager.enemyTeam.Add(newEnemyObj4);
            newEnemyObj4.transform.SetParent(enemyTeam.transform);
            //newEnemyObj4.GetComponent<Unit>().exhaustValue = sts.enemyData.exhaustedLvl;
            InsertDataToUnit(newEnemyObj4, i, true, "main");

            GameObject newEnemyObj5 = Instantiate(CovertStringToGameObject(tcm.e_rearString[i], true), tcm.e_rearObj[i].position, Quaternion.identity);
            manager.enemyTeam.Add(newEnemyObj5);
            newEnemyObj5.transform.SetParent(enemyTeam.transform);
            //newEnemyObj5.GetComponent<Unit>().exhaustValue = sts.enemyData.exhaustedLvl;
            InsertDataToUnit(newEnemyObj5, i, true, "rear");

            Time.timeScale = 0f;
            p_maxUnitCount = manager.playerTeam.Count;
            e_maxUnitCount = manager.enemyTeam.Count;

            SoundManager sm = FindObjectOfType<SoundManager>();
            if (sm != null)
            {
                sm.BattleMusic();
            }
            tcm.strategyGameObject.SetActive(false);
        }
    }


    public void InsertDataToUnit(GameObject _unit, int i, bool isEnemy, string pos)
    {
        if (_unit.layer != LayerMask.NameToLayer("None"))
        {
            Unit newUnit = _unit.GetComponent<Unit>();
            
            newUnit.commandPost = isEnemy ? e_post : p_post;
            newUnit.inArmy = pos;
            newUnit.matchingNumber = matching
                .Where(a => a.Key == i)
                .Select(b => b.Value)
                .FirstOrDefault();
        }
    }

    public void OnEnemyArmyButtonClicked()
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
                armyImage = tcm.e_vanguardImg;
                nameList = tcm.e_vanguardString;
                break;
            case "rearguard":
                armyImage = tcm.e_rearImg;
                nameList = tcm.e_rearString;
                break;
            case "center":
                armyImage = tcm.e_mainImg;
                nameList = tcm.e_mainString;
                break;
            case "leftwing":
                armyImage = tcm.e_leftImg;
                nameList = tcm.e_leftString;
                break;
            case "rightwing":
                armyImage = tcm.e_rightImg;
                nameList = tcm.e_rightString;
                break;
            default:
                break;
        }
    }

    public void OpenCommand()
    {
        tcm.commandPanel.SetActive(true);
        tcm.offCommandBtn.SetActive(true);
        tcm.onCommandBtn.SetActive(false);
    }

    public void CloseCommand()
    {
        tcm.commandPanel.SetActive(false);
        tcm.offCommandBtn.SetActive(false);
        tcm.onCommandBtn.SetActive(true);
    }

    public void OpenMenu()
    {
        tcm.MenuPanel.SetActive(true);
        tcm.disablePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Back()
    {
        tcm.MenuPanel.SetActive(false);
        tcm.disablePanel.SetActive(false);
        if(tcm.pauseButton.activeSelf)
            Time.timeScale = 1f;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }


}







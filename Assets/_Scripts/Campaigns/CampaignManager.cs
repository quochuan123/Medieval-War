using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CampaignManager : MonoBehaviour
{
    public GameObject startBattleButton;
    public GameObject armyChoose;
    public GameObject commandObj;
    public GameObject controller;
    public GameObject nodeView;
    public GameObject army;
    public GameObject enemyArmy;
    public List<GameObject> nodeObjects;
    public List<GameObject> armyObjects;
    public GameObject back;

    public Node prevNode;

    public MapState grave_mapstate;
    public MapState mistMountain_mapstate;
    public MapState strongRiver_mapstate;
    public MapState dawnValley_mapstate;
    public MapState deadKingdom_mapstate;
    public MapState skyfall_mapstate;
    public MapState iceCave_mapstate;


    private SingletonScript sts;
    private AddArmyWindow aam;

    public GameObject enemyStatus;
    public Text enemyLvl;
    public Text encircle;
    public Text dangerLvl;
    public Text gold;
    public Text xp;
    public Text recruits;
    public Image exhaustBar;
    public GameObject outOfStock;
    [Space(30)]
    //Unit amount
    public GameObject unitAmount;
    public Text knightAmount;
    public Text crossbowAmount;
    public Text shieldmanAmount;
    public Text archerAmount;
    public Text mageAmount;
    public Text halberdAmount;
    public Text archmageAmount;

    public GameObject openAmountButton;
    public GameObject closeAmountButton;


    private void Start()
    {
        sts = FindObjectOfType<SingletonScript>();
        aam = FindObjectOfType<AddArmyWindow>();
        
        nodeObjects = new List<GameObject>();

        IntializeMap();
        Renderer();
        aam.StartRegister(sts, this);

        SoundManager sm = FindObjectOfType<SoundManager>();
        if(sm != null)
        {
            sm.CampaignMusic();
        }
    }

    public void OpenAmountButton()
    {
        unitAmount.SetActive(true);
        openAmountButton.SetActive(false);
        closeAmountButton.SetActive(true);
        UpdateUnitAmountPanel();
    }
    public void CloseAmountButton()
    {
        unitAmount.SetActive(false);
        openAmountButton.SetActive(true);
        closeAmountButton.SetActive(false);

    }

    public void UpdateUnitAmountPanel()
    {
        knightAmount.text = sts.knightAmount.ToString();
        knightAmount.color = sts.knightAmount >= sts.knightCap ? Color.red : Color.black;
        
        archerAmount.text = sts.archerAmount.ToString();
        archerAmount.color = sts.archerAmount >= sts.archerCap ? Color.red : Color.black;

        halberdAmount.text = sts.halbertAmount.ToString();
        halberdAmount.color = sts.halbertAmount >= sts.halbertCap ? Color.red : Color.black;

        shieldmanAmount.text = sts.shieldmanAmount.ToString();
        shieldmanAmount.color = sts.shieldmanAmount >= sts.shieldmanCap ? Color.red : Color.black;

        crossbowAmount.text = sts.crossbowAmount.ToString();
        crossbowAmount.color = sts.crossbowAmount >= sts.crossbowCap ? Color.red : Color.black;

        mageAmount.text = sts.mageAmount.ToString();
        mageAmount.color = sts.mageAmount >= sts.mageCap ? Color.red : Color.black;

        archmageAmount.text = sts.archmageAmount.ToString();
        archmageAmount.color = sts.archmageAmount >= sts.archmageCap ? Color.red : Color.black;


    }

    public void Return()
    {
        sts.isStartStrategy = false;
        SceneManager.LoadScene("Village Map");
    }
    public void IntializeMap()
    {
        if (!sts.isStartStrategy)
        {
            switch (sts.nextCampaign)
            {
                case "mist_mountain":
                    sts.mapState = Instantiate(mistMountain_mapstate);
                    break;
                case "strong_river":
                    sts.mapState = Instantiate(strongRiver_mapstate);
                    break;
                case "dawn_valley":
                    sts.mapState = Instantiate(dawnValley_mapstate);
                    break;
                case "dead_kingdom":
                    sts.mapState = Instantiate(deadKingdom_mapstate);
                    break;
                case "skyfall":
                    sts.mapState = Instantiate(skyfall_mapstate);
                    break;
                case "icecave":
                    sts.mapState = Instantiate(iceCave_mapstate);
                    break;

                default:
                    sts.mapState = Instantiate(mistMountain_mapstate);
                    break;
            }
        }
    }

    public void Renderer()
    {
        foreach (GameObject go in nodeObjects)
        {
            Destroy(go);
        }
        nodeObjects.Clear();

        foreach (GameObject go in armyObjects)
        {
            Destroy(go);
        }
        armyObjects.Clear();
        for (int i = 0; i < sts.mapState.allNodes.Count; i++)
        {
            GameObject node = Instantiate(nodeView, sts.mapState.allNodes[i].worldPosition, Quaternion.identity);
            node.transform.position = new Vector3(node.transform.position.x, node.transform.position.y, 0.2f);
            sts.mapState.allNodes[i].ObjectID = i;
            node.GetComponent<NodeView>().node = sts.mapState.allNodes[i];
            nodeObjects.Add(node);
        }



        for (int i = 0; i < sts.mapState.allArmies.Count; i++)
        {
            if (!sts.mapState.allArmies[i].isPlayer)
            {
                if (sts.mapState.allArmies[i].isDead)
                {
                    continue;
                }
                GameObject newArmy = Instantiate(enemyArmy);
                newArmy.GetComponent<ArmyController>().armyData = sts.mapState.allArmies[i];
                newArmy.transform.position = sts.mapState.allNodes[newArmy.GetComponent<ArmyController>().armyData.currentNodeID].worldPosition;
                newArmy.transform.position = new Vector3(newArmy.transform.position.x, newArmy.transform.position.y, -2);
                armyObjects.Add(newArmy);
            }
            else
            {
                if (sts.mapState.allArmies[i].isDead)
                {
                    continue;
                }
                GameObject newArmy = Instantiate(army);
                newArmy.GetComponent<ArmyController>().armyData = sts.mapState.allArmies[i];
                newArmy.transform.position = sts.mapState.allNodes[newArmy.GetComponent<ArmyController>().armyData.currentNodeID].worldPosition;
                newArmy.transform.position = new Vector3(newArmy.transform.position.x, newArmy.transform.position.y, -2);

                armyObjects.Add(newArmy);
            }

        }
    }

    public void OpenArmyChoose()
    {
        armyChoose.SetActive(true);
    }

    public void OpenEnemyStatus()
    {
        enemyStatus.SetActive(true);
        enemyLvl.text = sts.enemyData.lvl.ToString();
        encircle.text = sts.enemyEncircleBonus.ToString();
        dangerLvl.text = sts.dangerLvl;
        gold.text = sts.goldNextBattle.ToString();
        xp.text = sts.xpNextBattle.ToString();
        recruits.text = sts.recruitsNextBattle.ToString();
        exhaustBar.fillAmount = 1 - sts.enemyData.exhaustedLvl;
    }

    public void MapInformationUpdate()
    {
        switch (sts.nextMap)
        {
            //Mist Mountain Campaign. Recommend Lvl = 1
            case "MistMountain_0":
                sts.dangerLvl = "Normal";
                sts.goldNextBattle = Random.Range(200,300);
                sts.xpNextBattle = Random.Range(100,200);
                sts.recruitsNextBattle = Random.Range(1,3);
                break;
            case "MistMountain_1":
                sts.dangerLvl = "Normal";
                sts.goldNextBattle = Random.Range(200, 300);
                sts.xpNextBattle = Random.Range(100, 200);
                sts.recruitsNextBattle = Random.Range(1, 3);

                break;
            case "MistMountain_Boss":
                sts.dangerLvl = "Hard";
                sts.goldNextBattle = Random.Range(300, 450);
                sts.xpNextBattle = Random.Range(150, 275);
                sts.recruitsNextBattle = Random.Range(2, 4);
                break;
            //Dawn Valley Campaign. Recommend lvl = 5;
            case "DawnValley_0":
                sts.dangerLvl = "Normal";
                sts.goldNextBattle = Random.Range(700, 1050);
                sts.xpNextBattle = Random.Range(350, 525);
                sts.recruitsNextBattle = Random.Range(5, 7);
                break;
            case "DawnValley_Hard":
                sts.dangerLvl = "Hard";
                sts.goldNextBattle = Random.Range(800, 1200);
                sts.xpNextBattle = Random.Range(450, 675);
                sts.recruitsNextBattle = Random.Range(6, 8);
                break;
            case "DawnValley_Veryhard":
                sts.dangerLvl = "Very hard";
                sts.goldNextBattle = Random.Range(900, 1350);
                sts.xpNextBattle = Random.Range(550, 775);
                sts.recruitsNextBattle = Random.Range(7, 9);
                break;
            case "DawnValley_Epic":
                sts.dangerLvl = "Epic - You have no chance here!";
                sts.goldNextBattle = Random.Range(99999, 199999);
                sts.xpNextBattle = Random.Range(50000, 100000);
                sts.recruitsNextBattle = Random.Range(250, 350);
                break;
            case "DawnValley_Boss":
                sts.dangerLvl = "Boss";
                sts.goldNextBattle = Random.Range(1000, 1500);
                sts.xpNextBattle = Random.Range(650, 1075);
                sts.recruitsNextBattle = Random.Range(8, 10);
                break;
            //Strong River Campaign. Recommend Lvl = 10
            case "StrongsRiver_0":
                sts.dangerLvl = "Normal";
                sts.goldNextBattle = Random.Range(1500, 2250);
                sts.xpNextBattle = Random.Range(1150, 1500);
                sts.recruitsNextBattle = Random.Range(9, 11);
                break;
            case "StrongsRiver_Hard":
                sts.dangerLvl = "Hard";
                sts.goldNextBattle = Random.Range(1600, 2400);
                sts.xpNextBattle = Random.Range(1300, 1850);
                sts.recruitsNextBattle = Random.Range(10, 12);
                break;
            case "StrongsRiver_Veryhard":
                sts.dangerLvl = "Very Hard";
                sts.goldNextBattle = Random.Range(1700,2550);
                sts.xpNextBattle = Random.Range(1400, 2100);
                sts.recruitsNextBattle = Random.Range(11, 13);
                break;
            case "StrongsRiver_Boss":
                sts.dangerLvl = "Boss";
                sts.goldNextBattle = Random.Range(1800, 2700);
                sts.xpNextBattle = Random.Range(1500, 2250);
                sts.recruitsNextBattle = Random.Range(12, 14);
                break;
            case "StrongsRiver_Epic":
                sts.dangerLvl = "Epic - You have no chance here!";
                sts.goldNextBattle = Random.Range(99999, 199999);
                sts.xpNextBattle = Random.Range(50000, 100000);
                sts.recruitsNextBattle = Random.Range(250, 350);
                break;
            //Dead Kingdom Campaign. Recommend Lvl = 15
            case "Deadkingdom_0":
                sts.dangerLvl = "Normal";
                sts.goldNextBattle = Random.Range(2300, 3450);
                sts.xpNextBattle = Random.Range(2000, 3000);
                sts.recruitsNextBattle = Random.Range(13, 15);
                break;
            case "Deadkingdom_Hard":
                sts.dangerLvl = "Hard";
                sts.goldNextBattle = Random.Range(2400, 3600);
                sts.xpNextBattle = Random.Range(2100, 3150);
                sts.recruitsNextBattle = Random.Range(14, 16);
                break;
            case "Deadkingdom_Veryhard":
                sts.dangerLvl = "Very Hard";
                sts.goldNextBattle = Random.Range(2500, 3750);
                sts.xpNextBattle = Random.Range(2200, 3300);
                sts.recruitsNextBattle = Random.Range(15, 17);
                break;
            case "Deadkingdom_Boss":
                sts.dangerLvl = "Boss";
                sts.goldNextBattle = Random.Range(2600, 3900);
                sts.xpNextBattle = Random.Range(2400, 3600);
                sts.recruitsNextBattle = Random.Range(16, 18);
                break;
            case "Deadkingdom_Epic":
                sts.dangerLvl = "Epic - You have no chance here!";
                sts.goldNextBattle = Random.Range(99999, 199999);
                sts.xpNextBattle = Random.Range(50000, 100000);
                sts.recruitsNextBattle = Random.Range(250, 350);
                break;
            //Skyfall Campaign. Recommend Lvl = 20

            case "Skyfall_0":
                sts.dangerLvl = "Normal";
                sts.goldNextBattle = Random.Range(4000, 6000);
                sts.xpNextBattle = Random.Range(3000, 4500);
                sts.recruitsNextBattle = Random.Range(20, 22);
                break;
            case "Skyfall_Hard":
                sts.dangerLvl = "Hard";
                sts.goldNextBattle = Random.Range(4400, 6600);
                sts.xpNextBattle = Random.Range(3200, 4600);
                sts.recruitsNextBattle = Random.Range(22, 24);
                break;
            case "Skyfall_Veryhard":
                sts.dangerLvl = "Very Hard";
                sts.goldNextBattle = Random.Range(4800, 7200);
                sts.xpNextBattle = Random.Range(3400, 5100);
                sts.recruitsNextBattle = Random.Range(24, 26);
                break;
            case "Skyfall_Boss":
                sts.dangerLvl = "Boss";
                sts.goldNextBattle = Random.Range(5500, 7750);
                sts.xpNextBattle = Random.Range(3800, 5100);
                sts.recruitsNextBattle = Random.Range(26, 28);
                break;
            case "Skyfall_Epic":
                sts.dangerLvl = "Epic - You have no chance here!";
                sts.goldNextBattle = Random.Range(99999, 199999);
                sts.xpNextBattle = Random.Range(50000, 100000);
                sts.recruitsNextBattle = Random.Range(250, 350);
                break;
            //Ice Cave Campaign. Recommend lvl 1
            case "IceCave":
                sts.dangerLvl = "Normal";
                sts.goldNextBattle = Random.Range(250 * sts.kingdomLvl, 250 * (sts.kingdomLvl+1));
                sts.xpNextBattle = Random.Range(250 * sts.kingdomLvl, 350 * sts.kingdomLvl);
                sts.recruitsNextBattle = Random.Range(1, 3);
                break;
        case "IceCave_Last":
                sts.dangerLvl = "Normal";
                sts.goldNextBattle = Random.Range(250 * sts.kingdomLvl, 250 * (sts.kingdomLvl + 1));
                sts.xpNextBattle = Random.Range(250 * sts.kingdomLvl, 350 * sts.kingdomLvl);
                sts.recruitsNextBattle = Random.Range(1, 3);
                break;


            default:
                break;

        }
    }
}






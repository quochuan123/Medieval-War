using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text playerUnits;
    public Text enemyUnits;
    public bool updateScore;
    public int p_maxUnitCount;
    public int e_maxUnitCount;


    public List<GameObject> enemyTeam;
    public List<GameObject> playerTeam;
    private SingletonScript sts;
    private CanvasManager canvasManager;
    public int battleXp;

    public string nextScene;

    public bool startBattle;
    public bool battleEnd;
    public int maxEnemy;
    public int minEnemy;
    public bool isTest;

    public List<GameObject> p_vanguard;
    public List<GameObject> p_mainforce;
    public List<GameObject> p_left;
    public List<GameObject> p_right;
    public List<GameObject> p_rear;


    public List<GameObject> e_vanguard;
    public List<GameObject> e_mainforce;
    public List<GameObject> e_left;
    public List<GameObject> e_right;
    public List<GameObject> e_rear;



    // Start is called before the first frame update
    void Start()
    {
        canvasManager = FindObjectOfType<CanvasManager>();
        sts = FindObjectOfType<SingletonScript>();
    }
    IEnumerator UpdateScore()
    {
        playerUnits.text = playerTeam.Count.ToString() + "/" + p_maxUnitCount.ToString();
        enemyUnits.text = enemyTeam.Count.ToString() + "/" + e_maxUnitCount.ToString();

        yield return new WaitForSeconds(0.5f);
        updateScore = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isTest)
        {
            if (startBattle)
            {
                if (!updateScore)
                {
                    updateScore = false;
                    StartCoroutine(UpdateScore());  
                }

                if (enemyTeam.Count == 0 || playerTeam.Count == 0)
                {
                    if (!battleEnd)
                    {
                        battleEnd = true;

                        if (enemyTeam.Count == 0)
                        {


                            if (!sts.onlyBattle)
                            {
                                sts.enemyData.isDead = true;

                                sts.nextNode.controlledByPlayer = true;
                                sts.nextNode.occupyingArmyID = sts.playerData.armyID;
                                sts.playerData.currentNodeID = sts.nextNode.nodeID;
                                sts.gold += sts.goldNextBattle;
                                sts.volunteers += sts.recruitsNextBattle;

                                for (int i = 0; i < sts.mapState.allNodes.Count; i++)
                                {
                                    if (sts.mapState.allNodes[i].nodeID == sts.nextNode.nodeID)
                                    {
                                        sts.mapState.allNodes[i] = sts.nextNode;
                                    }
                                }

                                for (int i = 0; i < sts.mapState.allArmies.Count; i++)
                                {
                                    if (sts.mapState.allArmies[i].armyID == sts.enemyData.armyID)
                                    {
                                        sts.mapState.allArmies[i] = sts.enemyData;
                                    }

                                    if (sts.mapState.allArmies[i].armyID == sts.playerData.armyID)
                                    {
                                        sts.mapState.allArmies[i] = sts.playerData;
                                    }
                                }

                                for (int i = 0; i < sts.controllersList.Count; i++)
                                {
                                    if (sts.controllersList[i].node.nodeID == sts.prevNode.nodeID)
                                    {
                                        sts.controllersList[i].node = sts.nextNode;
                                    }
                                }
                            }
                            for (int i = 0; i < playerTeam.Count; i++)
                            {
                                Unit unit = playerTeam[i].GetComponent<Unit>();
                                switch (unit.unitClass)
                                {
                                    case "knight":
                                        sts.knightAmount++;
                                        break;
                                    case "archmage":
                                        sts.archmageAmount++;
                                        break;

                                    case "archer":
                                        sts.archerAmount++;
                                        break;

                                    case "halbert":
                                        sts.halbertAmount++;
                                        break;

                                    case "shieldman":
                                        sts.shieldmanAmount++;
                                        break;

                                    case "mage":
                                        sts.mageAmount++;
                                        break;

                                    case "crossbow":
                                        sts.crossbowAmount++;
                                        break;
                                    default:
                                        break;
                                }
                            }

                            canvasManager.postgameScreen.SetActive(true);
                            SoundManager sm = FindObjectOfType<SoundManager>();
                            if (sm != null)
                            {
                                sm.PostBattleMusic();
                            }

                        }

                        if (playerTeam.Count == 0)
                        {
                            //sts.playerData.isDead = true;

                            //for (int i = 0; i < sts.mapState.allNodes.Count; i++)
                            //{
                            //  if (sts.mapState.allNodes[i].nodeID == sts.prevNode.nodeID)
                            //{
                            //  sts.mapState.allNodes[i] = sts.prevNode;
                            //}
                            //}

                            //sts.controllersList.RemoveAll(a => a.node.nodeID == sts.prevNode.nodeID);
                            //sts.armies.RemoveAll(b => b.name == sts.playerData.name);
                            if (!sts.onlyBattle)
                            {
                                if (sts.armies.Count == 0)
                                {
                                    Debug.Log("GameOver");
                                }
                                minEnemy = enemyTeam.Count;

                                sts.enemyData.ExhaustedCalculator(1 - ((float)minEnemy / maxEnemy));
                                sts.playerData.ExhaustedCalculator(0.5f);
                            }

                            NextSceneProcess(false);

                        }


                        //NextSceneProcess();
                    }
                }


            }
        }
        
    }

    public void OpenCommand()
    {
        canvasManager.openCommandButton.SetActive(false);
        canvasManager.closeCommandButton.SetActive(true);
        canvasManager.commandPanel.SetActive(true);
    }
    public void CloseCommand()
    {
        canvasManager.openCommandButton.SetActive(true);
        canvasManager.closeCommandButton.SetActive(false);
        canvasManager.commandPanel.SetActive(false);
    }

    public void Back()
    {
        NextSceneProcess(true);
    }

    public void NextSceneProcess(bool isWin)
    {
        /*
        switch (sts.nextMap)
        {
            //Map List
            //Mist Mountain Campaign. Recommend Lvl = 1
            case "MistMountain_0":
                
                break;
            case "MistMountain_1":
                

                break;
            case "MistMountain_Boss":
                
                break;
            //Strong River Campaign. Recommend Lvl = 10
            case "StrongsRiver_Veryhard":
                
                break;
            case "StrongsRiver_Hard":
                
                break;
            case "StrongsRiver_Boss":
                
                break;
            case "StrongsRiver_Extremely":
                
                break;
            case "StrongsRiver_0":
               
                break;
            default:
                break;

        }
        */


        switch (sts.nextMap)
        {
            //Map List
            //Mist Mountain Campaign. Recommend Lvl = 1
            case "MistMountain_0":
                SceneManager.LoadScene("Campaign Scene");
                break;
            case "MistMountain_1":
                SceneManager.LoadScene("Campaign Scene");
                break;
            case "MistMountain_Boss":
                if (isWin)
                {
                    SceneManager.LoadScene("Village Map");
                    sts.isCompleteMistMountain = true;
                    sts.isStartStrategy = false;
                }
                else
                {
                    SceneManager.LoadScene("Campaign Scene");
                }


                break;
            //Dawn Valley Campaigm. Recommend lvl = 5
            case "DawnValley_Veryhard":
                SceneManager.LoadScene("Campaign Scene");

                break;
            case "DawnValley_Hard":
                SceneManager.LoadScene("Campaign Scene");

                break;
            case "DawnValley_Boss":
                if (isWin)
                {
                    SceneManager.LoadScene("Village Map");
                    sts.isCompleteDawnValley = true;
                    sts.isStartStrategy = false;
                }
                else
                {
                    SceneManager.LoadScene("Campaign Scene");
                }
                break;
            case "DawnValley_Epic":
                SceneManager.LoadScene("Campaign Scene");

                break;
            case "DawnValley_0":
                SceneManager.LoadScene("Campaign Scene");

                break;
            //Strong River Campaign. Recommend Lvl = 10
            case "StrongsRiver_Veryhard":
                SceneManager.LoadScene("Campaign Scene");

                break;
            case "StrongsRiver_Hard":
                SceneManager.LoadScene("Campaign Scene");

                break;
            case "StrongsRiver_Boss":
                if (isWin)
                {
                    SceneManager.LoadScene("Village Map");
                    sts.isCompleteStrongRiver = true;
                    sts.isStartStrategy = false;
                }
                else
                {
                    SceneManager.LoadScene("Campaign Scene");
                }
                break;
            case "StrongsRiver_Epic":
                SceneManager.LoadScene("Campaign Scene");

                break;
            case "StrongsRiver_0":
                SceneManager.LoadScene("Campaign Scene");
                break;
            //Dead Kingdom Campaign. Recommend lvl = 15

            case "Deadkingdom_0":
                SceneManager.LoadScene("Campaign Scene");

                break;
            case "Deadkingdom_Hard":
                SceneManager.LoadScene("Campaign Scene");

                break;
            case "Deadkingdom_Veryhard":
                SceneManager.LoadScene("Campaign Scene");

                break;
            case "Deadkingdom_Boss":
                if (isWin)
                {
                    SceneManager.LoadScene("Village Map");
                    sts.isCompleteDeadKingdom = true;
                    sts.isStartStrategy = false;
                }
                else
                {
                    SceneManager.LoadScene("Campaign Scene");
                }
                break;
            case "Deadkingdom_Epic":
                SceneManager.LoadScene("Campaign Scene");

                break;
            //Skyfall Campaign. Recommend lvl = 20
            case "Skyfall_Veryhard":
                SceneManager.LoadScene("Campaign Scene");

                break;
            case "Skyfall_Hard":
                SceneManager.LoadScene("Campaign Scene");

                break;
            case "Skyfall_Boss":
                if (isWin)
                {
                    SceneManager.LoadScene("Village Map");
                    sts.isCompleteSkyfall = true;
                    sts.isStartStrategy = false;
                }
                else
                {
                    SceneManager.LoadScene("Campaign Scene");
                }
                break;
            case "Skyfall_Epic":
                SceneManager.LoadScene("Campaign Scene");

                break;
            case "Skyfall_0":
                SceneManager.LoadScene("Campaign Scene");
                break;
            //Custom
            case "villager_underattack":
                SceneManager.LoadScene("Village Map");
                sts.isStartStrategy = false;
                sts.onlyBattle = false;
                break;
            case "IceCave":
                SceneManager.LoadScene("Campaign Scene");
                break;
            case "IceCave_Last":
                SceneManager.LoadScene("Village Map");
                sts.isStartStrategy = false;
                break;
            default:
                break;

        }


    }
   
    public void OpenHelp()
    {
        canvasManager.helpInterface.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseHelp()
    {
        canvasManager.helpInterface.SetActive(false);
        if(!canvasManager.unpauseBtn.activeSelf)
        {
            Time.timeScale = 1f;
        }    
    }

    public void OpenStrategyUnitScreen()
    {
        canvasManager.strategyUnitScreen.SetActive(true);
    }
    public void CloseStrategyUnitScreen()
    {
        canvasManager.strategyUnitScreen.SetActive(false);

    }

    public void OnPauseBtn()
    {
        Time.timeScale = 0;
        canvasManager.pauseBtn.SetActive(false);
        canvasManager.unpauseBtn.SetActive(true);
    }
    public void OnUnpauseBtn()
    {
        Time.timeScale = 1f;
        canvasManager.pauseBtn.SetActive(true);
        canvasManager.unpauseBtn.SetActive(false);
    }
}

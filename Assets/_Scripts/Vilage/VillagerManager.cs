using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class VillagerManager : MonoBehaviour
{
    public bool takeGold;
    public bool isTakenGold;
    public bool takeVoluteers;
    public bool isTakenVoluteers;
    public bool CanOpenTrainingUnit;
    public bool isTalkWithElf;
    public bool isTalkWithGraveKeeper;
    public bool isTalkWithSoldier;
    public bool isTalkWithHunter;






    private SingletonScript sts;
    private VillagerResourcesManager rm;
    private PrinceController player;
    // Start is called before the first frame update
    void Start()
    {
        sts = FindObjectOfType<SingletonScript>();
        rm = FindObjectOfType<VillagerResourcesManager>();
        player = FindObjectOfType<PrinceController>();


        rm.voluteers.text = sts.volunteers.ToString();
        rm.gold.text = sts.gold.ToString();
        isTakenGold = true;
        isTakenVoluteers = true;

        sts.KingdomMaxExpCalculate();
        sts.CapCalculate();

        rm.kdXPbar.fillAmount = (float)sts.kingdomLvlExp / sts.kingdomLvlMaxExp;
        rm.kdLvl.text = "Level " + sts.kingdomLvl.ToString();


        SoundManager sm = FindObjectOfType<SoundManager>();
        if(sm != null)
        {
            sm.VillagerMusic();
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {



        if (takeGold && isTakenGold && Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log(takeGold && isTakenGold && Input.GetKeyDown(KeyCode.E));
            isTakenGold = false;
            rm.disableHud.SetActive(true);
            rm.normalConversation.SetActive(true);
            rm.npcName.text = "Miner";
            rm.conversationContent.text = "Have a good day my lord. Here are our gold we took from mines.";
            player.isTalk = true;

            sts.gold += 1000 + 500 * sts.kingdomLvl;
            rm.gold.text = sts.gold.ToString();
            Invoke("CloseNormalConversation", 1f);
        }




        if (takeVoluteers && isTakenVoluteers && Input.GetKeyDown(KeyCode.E))
        {
            // Debug.Log(takeVoluteers && isTakenVoluteers && Input.GetKeyDown(KeyCode.E));

            rm.normalConversation.SetActive(true);
            rm.disableHud.SetActive(true);

            rm.npcName.text = "Nun";
            rm.conversationContent.text = "Have a good day my lord. Here are our new mens.";
            player.isTalk = true;

            isTakenVoluteers = false;
            sts.volunteers += sts.kingdomLvl;
            rm.voluteers.text = sts.volunteers.ToString();
            Invoke("CloseNormalConversation", 1f);

        }

        if (CanOpenTrainingUnit && Input.GetKeyDown(KeyCode.E))
        {
            rm.normalConversation.SetActive(true);
            rm.disableHud.SetActive(true);

            player.isTalk = true;
            rm.npcName.text = "Merchant";
            rm.conversationContent.text = "Welcome Welcome! I can help you train your army.";
            Invoke("OpenTrainingArmyInterface", 1f);
        }


        if (isTalkWithElf && Input.GetKeyDown(KeyCode.E))
        {
            rm.elfConversation.SetActive(true);
            rm.disableHud.SetActive(true);

            player.isTalk = true;
            rm.elfConversationContent.text = "I can help you create some armies. Just give me some gold!\r\nGold need: " + Mathf.RoundToInt((10000 * sts.armies.Count * 1.5f)).ToString() + " \r\n(You can create " + sts.armies.Count.ToString() + "/ 6 armies)";
        }

        if (isTalkWithGraveKeeper && Input.GetKeyDown(KeyCode.E))
        {
            rm.graveKeeperConversation.SetActive(true);
            player.isTalk = true;
            rm.disableHud.SetActive(true);
            rm.gkConvers_content.text = "I found an Acient tomb with a lot of treasures. Do you want to join?";

        }

        if (isTalkWithHunter && Input.GetKeyDown(KeyCode.E))
        {
            rm.hunterConversation.SetActive(true);
            rm.disableHud.SetActive(true);

            rm.hunterYNpanel.SetActive(true);
            player.isTalk = true;
            rm.hunterConvers_content.text = "My villager is under attack! If you help us we will join your army.";

        }


        if (isTalkWithSoldier && Input.GetKeyDown(KeyCode.E))
        {
            rm.normalConversation.SetActive(true);
            player.isTalk = true;
            rm.disableHud.SetActive(true);

            rm.npcName.text = "Soldier";
            rm.conversationContent.text = "My lord. What is our next mission?";
            Invoke("OpenCampaignInterface", 1f);
        }




    }

    public void CloseNormalConversation()
    {
        rm.normalConversation.SetActive(false);
        rm.disableHud.SetActive(false);
        player.isTalk = false;
    }


    public void OpenTrainingArmyInterface()
    {
        rm.trainningInterface.SetActive(true);
        rm.normalConversation.SetActive(false);

        SoundManager sm = FindObjectOfType<SoundManager>();
        if(sm != null)
        {
            sm.TrainingInterfaceMusicOn();
        }
        
    }

    public void OpenCampaignInterface()
    {
        rm.campaignInterface.SetActive(true);
        rm.normalConversation.SetActive(false);

        rm.campaignInterface.GetComponent<CampaignInterfaceManager>().SetUpCampaign(sts, rm);
    SoundManager sm = FindObjectOfType<SoundManager>();
        if(sm != null)
        {
            sm.TrainingInterfaceMusicOn();
        }
    }

    public void OnElfYesButton()
    {
        if (sts.armies.Count >= 6)
        {
            rm.elfConversationContent.text = "Your armies are 6! You can't create any armies.";
            StartCoroutine(WaitUntilYesNoPanelAppearAgain(rm.elfYesNoPanel, 1f, null, false));
        }
        else if (sts.gold < Mathf.RoundToInt((10000 * sts.armies.Count * 1.5f)))
        {
            rm.elfConversationContent.text = "You don't have enough gold!";
            StartCoroutine(WaitUntilYesNoPanelAppearAgain(rm.elfYesNoPanel, 1f, null, false));

        }
        else
        {
            ArmyInfor newArmy = new ArmyInfor("Army " + (sts.armies.Count + 1).ToString(), 1);
            sts.armies.Add(newArmy);
            newArmy.MaxXPCalculate();

            sts.gold -= Mathf.RoundToInt((10000 * (sts.armies.Count - 1) * 1.5f));
            rm.gold.text = sts.gold.ToString();
            rm.elfConversationContent.text = "Now you have a new army!";
            StartCoroutine(WaitUntilYesNoPanelAppearAgain(rm.elfYesNoPanel, 1f, null, false));

        }

    }



    public void OnElfNoButton()
    {
        rm.elfConversationContent.text = "See you again!";
        StartCoroutine(WaitUntilYesNoPanelAppearAgain(rm.elfYesNoPanel, 1f, rm.elfConversation, true));
    }

    public void OnGraveKeeperYesButton()
    {
        sts.nextCampaign = "icecave";
        SceneManager.LoadScene("Campaign Scene");
    }

    public void OnGraveKeepterNoButton()
    {
        rm.gkConvers_content.text = "Ok i will go alone then.";
        StartCoroutine(WaitUntilYesNoPanelAppearAgain(rm.gkYNpanel, 1f, rm.graveKeeperConversation, true));
    }

    public void OnHunterYesButton()
    {
        rm.hunterConvers_content.text = "Thank you so much! We need to go right away.";
        rm.selectArmy.SetActive(true);
        rm.selectArmy.GetComponent<ChooseArmyVillagerScenePanel>().OpenSelectPanel(sts);
        rm.hunterYNpanel.SetActive(false);
    }

    public void OnHunterNoButton()
    {
        rm.hunterConvers_content.text = "I have no choice but to fight alone.";
        StartCoroutine(WaitUntilYesNoPanelAppearAgain(rm.hunterYNpanel, 1f, rm.hunterConversation, true));
    }


    IEnumerator WaitUntilYesNoPanelAppearAgain(GameObject hideGameObject, float hideTime, GameObject inactiveObj, bool isCloseConversation)
    {
        hideGameObject.SetActive(false);
        yield return new WaitForSeconds(hideTime);
        hideGameObject.SetActive(true);
        if (isCloseConversation)
        {
            player.isTalk = false;
            inactiveObj.SetActive(false);
            rm.disableHud.SetActive(false);
            yield break;
        }
        rm.elfConversationContent.text = "I can help you create some armies. Just give me some gold!\r\nGold need: " + Mathf.RoundToInt((10000 * sts.armies.Count * 1.5f)).ToString() + " \r\n(You can create " + sts.armies.Count.ToString() + "/ 6 armies)";

    }

    public void OpenMenu()
    {
        rm.menuScreen.SetActive(true);
        player.isTalk = true;
    }
    public void CloseMenu()
    {
        rm.menuScreen.SetActive(false);
        player.isTalk = false;
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void SaveGame()
    {
        SaveLoadManager data = new SaveLoadManager()
        {
            _volunteers = sts.volunteers,
            _gold = sts.gold,
            _armies = sts.armies,
            _kingdomLvl = sts.kingdomLvl,
            _kingdomLvlExp = sts.kingdomLvlExp,
            _kingdomLvlCap = sts.kingdomLvlCap,
            _knightAmount = sts.knightAmount,
            _archerAmount = sts.archerAmount,
            _halbertAmount = sts.halbertAmount,
            _shieldmanAmount = sts.shieldmanAmount,
            _mageAmount = sts.mageAmount,
            _archmageAmount = sts.archmageAmount,
            _crossbowAmount = sts.crossbowAmount,
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + "/player.json", json);
        CloseMenu();
        rm.warring.text = "Save complete";
        rm.warringObj.SetActive(true);
        StartCoroutine(closeWarring());
    }

    public void LoadFile()
    {
        string path = Application.persistentDataPath + "/player.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveLoadManager data = JsonUtility.FromJson<SaveLoadManager>(json);

            // Gán dữ liệu từ file vào các biến hiện tại
            sts.volunteers = data._volunteers;
            sts.gold = data._gold;
            sts.armies = data._armies;
            sts.kingdomLvl = data._kingdomLvl;
            sts.kingdomLvlExp = data._kingdomLvlExp;
            sts.kingdomLvlCap = data._kingdomLvlCap;
            sts.knightAmount = data._knightAmount;
            sts.archerAmount = data._archerAmount;
            sts.halbertAmount = data._halbertAmount;
            sts.shieldmanAmount = data._shieldmanAmount;
            sts.mageAmount = data._mageAmount;
            sts.archmageAmount = data._archmageAmount;
            sts.crossbowAmount = data._crossbowAmount;
            Debug.Log("Game loaded from JSON!");
            SceneManager.LoadScene("Village Map");

        }
        else
        {
            rm.warring.text = "No save file found at: " + path;
            rm.warringObj.SetActive(true);
            StartCoroutine(closeWarring());
        }

    }

    IEnumerator closeWarring()
    {
        yield return new WaitForSeconds(0.5f);
        rm.warringObj.SetActive(false);


    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}

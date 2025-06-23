using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VillagerResourcesManager : MonoBehaviour
{
    //Warring
    public Text warring;
    public GameObject warringObj;


    //HUD
    [Space(30)]

    public Text gold;
    public Text voluteers;
    public Image kdXPbar;
    public Text kdLvl;
    public GameObject disableHud;
    [Space(30)]

    //Trainning interface
    public GameObject trainningInterface;
    public Text currentGold;
    public Text currentVoluteers;

    public Text goldCost;
    public Text voluteersCost;

    public Text trainningText;

    public Text knightAmount;
    public Text archerAmount;
    public Text halbertAmount;
    public Text shieldmanAmount;
    public Text archmageAmount;
    public Text mageAmount;
    public Text crossbowAmount;

    public Text tip;
    [Space(30)]

    //KD interface
    public List<ArmyInforKD> armies;
    public GameObject KdStatusGameObject;

    public Text unitAmount;
    public Text goldKd;
    public Text volunteersKd;
    public Text knightAmountKd;
    public Text archerAmountKd;
    public Text archmageAmountKd;
    public Text halbertAmountKd;
    public Text mageAmountKd;
    public Text shieldmanAmountKd;
    public Text crossbowAmountKd;
    [Space(30)]

    //NPC Conversation
    public GameObject elfConversation;
    public Text elfConversationContent;
    public GameObject elfYesNoPanel;


    public GameObject graveKeeperConversation;
    public Text gkConvers_content;
    public GameObject gkYNpanel;


    public GameObject hunterConversation;
    public Text hunterConvers_content;
    public GameObject hunterYNpanel;

    public GameObject normalConversation;
    public Text npcName;
    public Text conversationContent;
    [Space(30)]

    //Campaigns
    public GameObject campaignInterface;
    public Text detailCPName;
    public Text detailDesName;
    public Text detailLvlRecName;
    public Text detailAmountName;
    [Space(30)]

    //Farm mens
    public GameObject selectArmy;

    [Space(30)]
    //Main Menu
    public GameObject menuScreen;
}

[System.Serializable] public class ArmyInforKD
{
    public ArmyInfor _ref;
    public GameObject armyObj;
    public Text name;
    public Text lvl;
    public Text xp;
}

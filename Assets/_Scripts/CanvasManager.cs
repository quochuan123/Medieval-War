using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    //Help interface
    public GameObject helpInterface;
    [Space(30)]


    //Button
    public GameObject pauseBtn;
    public GameObject unpauseBtn;
    //Test Canvas
    //Space near or far
    public Image farRangeCheck;
    public Image nearRangeCheck;
    [Space(30)]
    //Enemy unit or player unit
    public Image enemyCheck;
    public Image playerCheck;
    [Space(30)]
    //Command Canvas
    public GameObject holdCheck;
    public GameObject retreatCheck;
    public GameObject attackCheck;
    public GameObject notBreakLineupCheck;
    public GameObject movingSyncEnable;
    public GameObject movingSyncDisable;
    public GameObject commandPanel;
    public GameObject openCommandButton;
    public GameObject closeCommandButton;
    [Space(30)]
    //Strategy Phase Canvas
    public Text tip;
    public List<Image> vanguard;
    public List<Image> mainForces;
    public List<Image> leftWing;
    public List<Image> rightWing;
    public List<Image> rearGuard;

    public List<string> vanguardUnit;
    public List<string> mainForcesUnit;
    public List<string> leftWingUnit;
    public List<string> rightWingUnit;
    public List<string> rearGuardUnit;

    public Text knightAmountText;
    public Text archerAmountText;
    public Text halbertAmountText;
    public Text mageAmountText;
    public Text archmageAmountText;
    public Text shieldmanAmountText;
    public Text crossbowAmountText;

    public GameObject enemyArmy;
    public GameObject playerArmy;

    public List<string> enemyVanguardUnit;
    public List<string> enemyMainForcesUnit;
    public List<string> enemyLeftWingUnit;
    public List<string> enemyRightWingUnit;
    public List<string> enemyRearGuardUnit;

    public List<Image> enemyVanguard;
    public List<Image> enemyMainForces;
    public List<Image> enemyLeftWing;
    public List<Image> enemyRightWing;
    public List<Image> enemyRearGuard;

    public List<Transform> enemyVanguardTransforms;
    public List<Transform> enemyReaGuardTransforms;
    public List<Transform> enemyLeftWingTransforms;
    public List<Transform> enemyRightWingTransforms;
    public List<Transform> enemyMainForcesTransforms;

    public List<Transform> vanguardTransforms;
    public List<Transform> rearGuardTransforms;
    public List<Transform> leftWingTransforms;
    public List<Transform> rightWingTransforms;
    public List<Transform> mainForcesTransforms;

    public Text enemyArmyLvl;
    public Text enemyEncircleBonus;
    public Text enemyLvlBonus;
    public Text playerArmyLvl;
    public Text playerEncircleBonus;
    public Text playerLvlBonus;
    [Space(30)]

    //Post game screen
    public GameObject postgameScreen;
    public Image kdXPBar;
    public Image armyXPBar;
    //public Text kdXpAmount;
    //public Text armyXpAmount;
    public Text armyLvlPostScrn;
    public Text kdLvlPostScrn;
    public Text armyNamePostGame;
    public Text goldRecieve;
    public Text recruitReceive;





    public GameObject strategyPhase;
    [Space(30)]
    //Strategy unit
    public GameObject strategyUnitScreen;


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCanvasManager : MonoBehaviour
{
    //Strategy phase
    public GameObject strategyGameObject;
    public GameObject playerScreen;
    public GameObject enemyScreen;
    [Space(10)]

    public GameObject x1;
    public GameObject x5;
    public GameObject x10;
    public GameObject tips;
    [Space(30)]
    //Strategy Screen
    public GameObject strategyPanel;

    [Space(30)]
    //String
    public List<string> p_vanguardString;
    public List<string> p_mainString;
    public List<string> p_rearString;
    public List<string> p_leftString;
    public List<string> p_rightString;
    [Space(10)]

    public List<string> e_vanguardString;
    public List<string> e_mainString;
    public List<string> e_rearString;
    public List<string> e_leftString;
    public List<string> e_rightString;
    //Image
    [Space(30)]
    public List<Image> p_vanguardImg;
    public List<Image> p_mainImg;
    public List<Image> p_rearImg;
    public List<Image> p_leftImg;
    public List<Image> p_rightImg;
    [Space(10)]

    public List<Image> e_vanguardImg;
    public List<Image> e_mainImg;
    public List<Image> e_rearImg;
    public List<Image> e_leftImg;
    public List<Image> e_rightImg;
    [Space(30)]

    //GameObject
    public List<Transform> p_vanguardObj;
    public List<Transform> p_mainObj;
    public List<Transform> p_rearObj;
    public List<Transform> p_leftObj;
    public List<Transform> p_rightObj;

    public List<Transform> e_vanguardObj;
    public List<Transform> e_mainObj;
    public List<Transform> e_rearObj;
    public List<Transform> e_leftObj;
    public List<Transform> e_rightObj;
    [Space(30)]
    //Command
    public GameObject onCommandBtn;
    public GameObject offCommandBtn;

    public GameObject commandPanel;
    public GameObject holdCheck;
    public GameObject attackCheck;
    public GameObject retreatCheck;
    public GameObject notBreakLineupCheck;
    public GameObject movingSyncDisable;
    public GameObject movingSyncEnable;

    //Pause
    public GameObject pauseButton;
    public GameObject unpauseButton;

    //Score
    public Text playerUnits;
    public Text enemyUnits;

    //Menu
    public GameObject MenuPanel;
    public GameObject disablePanel;




}

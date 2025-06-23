using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TesManager : MonoBehaviour
{
    public Sprite checkSprite;
    public Sprite notCheckSprite;
    private GameManager gameManager;
    private GameObject playerTeam;
    private GameObject enemyTeam;
    private CanvasManager tcm;
    public bool editMode;
    public bool isFromFar;
    public bool isPlayerUnit;

    private GameObject createPlace;
    private GameObject createUnit;

    public GameObject knight;
    public GameObject archmage;
    public GameObject mage;
    public GameObject archer;
    public GameObject crossbow;
    public GameObject halbert;
    public GameObject shieldman;

    public GameObject enemyKnight;
    public GameObject enemyArchmage;
    public GameObject enemyMage;
    public GameObject enemyArcher;
    public GameObject enemyCrossbow;
    public GameObject enemyHalbert;
    public GameObject enemyShieldman;

    public GameObject enemyNearPlace;
    public GameObject enemyFarPlace;
    public GameObject playerNearPlace;
    public GameObject playerFarPlace;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        tcm = FindObjectOfType<CanvasManager>();
        playerTeam = GameObject.Find("Player Force");
        enemyTeam = GameObject.Find("Enemy Force");

        tcm.nearRangeCheck.sprite = checkSprite;
        tcm.farRangeCheck.sprite = notCheckSprite;
        isFromFar = false;

        tcm.playerCheck.sprite = checkSprite;
        tcm.enemyCheck.sprite = notCheckSprite;
        isPlayerUnit = true;

        editMode = true;
        Time.timeScale = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetButton()
    {
        gameManager.enemyTeam.Clear();
        gameManager.playerTeam.Clear();
        foreach (Transform child in enemyTeam.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in playerTeam.transform)
        {
            Destroy(child.gameObject);
        }
        editMode = true;
        Time.timeScale = 0f;

    }

    public void StartButton()
    {
        editMode = false;
        Time.timeScale = 1f;
    }

    public void LongRangeCheck()
    {
        tcm.nearRangeCheck.sprite = notCheckSprite;
        tcm.farRangeCheck.sprite = checkSprite;
        isFromFar = true;
    }

    public void NearRangeCheck()
    {
        tcm.nearRangeCheck.sprite = checkSprite;
        tcm.farRangeCheck.sprite = notCheckSprite;
        isFromFar = false;
    }

    public void PlayerUnit()
    {
        tcm.playerCheck.sprite = checkSprite;
        tcm.enemyCheck.sprite = notCheckSprite;
        isPlayerUnit = true;
    }

    public void EnemyUnit()
    {
        tcm.playerCheck.sprite = notCheckSprite;
        tcm.enemyCheck.sprite = checkSprite;
        isPlayerUnit = false;

    }

    public void CreateUnit()
    {
        if (editMode)
        {
            if(isPlayerUnit && isFromFar)
            {
                createPlace = playerFarPlace;
            }

            if (!isPlayerUnit && isFromFar)
            {
                createPlace = enemyFarPlace;
            }

            if (isPlayerUnit && !isFromFar)
            {
                createPlace = playerNearPlace;
            }

            if (!isPlayerUnit && !isFromFar)
            {
                createPlace = enemyNearPlace;
            }

            float x = Random.Range(createPlace.transform.position.x+0.1f, createPlace.transform.position.x - 0.1f);
            float y = Random.Range(createPlace.transform.position.y + 0.5f, createPlace.transform.position.y - 0.5f);
            Vector2 trueCreatePlace = new Vector2(x,y);
            string tag = "";
            GameObject clickedObj = EventSystem.current.currentSelectedGameObject;
            if (clickedObj != null)
            {
                tag =  clickedObj.tag;
            }
            switch (tag)
            {
                case"knight":
                    if (isPlayerUnit)
                    {
                        GameObject _newUnit = Instantiate(knight, trueCreatePlace, Quaternion.identity);
                        gameManager.playerTeam.Add(_newUnit);
                        _newUnit.transform.SetParent(playerTeam.transform);
                    }
                    else
                    {
                        GameObject _newUnit = Instantiate(enemyKnight, trueCreatePlace, Quaternion.identity);
                        gameManager.enemyTeam.Add(_newUnit);
                        _newUnit.transform.SetParent(enemyTeam.transform);
                    }    
                        break;
                case "mage":
                    if (isPlayerUnit)
                    {
                        GameObject _newUnit = Instantiate(mage, trueCreatePlace, Quaternion.identity);
                        gameManager.playerTeam.Add(_newUnit);
                        _newUnit.transform.SetParent(playerTeam.transform);
                    }
                    else
                    {
                        GameObject _newUnit = Instantiate(enemyMage, trueCreatePlace, Quaternion.identity);
                        gameManager.enemyTeam.Add(_newUnit);
                        _newUnit.transform.SetParent(enemyTeam.transform);
                    }
                    break;
                case "archmage":
                    if (isPlayerUnit)
                    {
                        GameObject _newUnit = Instantiate(archmage, trueCreatePlace, Quaternion.identity);
                        gameManager.playerTeam.Add(_newUnit);
                        _newUnit.transform.SetParent(playerTeam.transform);
                    }
                    else
                    {
                        GameObject _newUnit = Instantiate(enemyArchmage, trueCreatePlace, Quaternion.identity);
                        gameManager.enemyTeam.Add(_newUnit);
                        _newUnit.transform.SetParent(enemyTeam.transform);
                    }
                    break;
                case "halbert":
                    if (isPlayerUnit)
                    {
                        GameObject _newUnit = Instantiate(halbert, trueCreatePlace, Quaternion.identity);
                        gameManager.playerTeam.Add(_newUnit);
                        _newUnit.transform.SetParent(playerTeam.transform);
                    }
                    else
                    {
                        GameObject _newUnit = Instantiate(enemyHalbert, trueCreatePlace, Quaternion.identity);
                        gameManager.enemyTeam.Add(_newUnit);
                        _newUnit.transform.SetParent(enemyTeam.transform);
                    }
                    break;
                case "crossbow":
                    if (isPlayerUnit)
                    {
                        GameObject _newUnit = Instantiate(crossbow, trueCreatePlace, Quaternion.identity);
                        gameManager.playerTeam.Add(_newUnit);
                        _newUnit.transform.SetParent(playerTeam.transform);
                    }
                    else
                    {
                        GameObject _newUnit = Instantiate(enemyCrossbow, trueCreatePlace, Quaternion.identity);
                        gameManager.enemyTeam.Add(_newUnit);
                        _newUnit.transform.SetParent(enemyTeam.transform);
                    }
                    break;
                case "archer":
                    if (isPlayerUnit)
                    {
                        GameObject _newUnit = Instantiate(archer, trueCreatePlace, Quaternion.identity);
                        gameManager.playerTeam.Add(_newUnit);
                        _newUnit.transform.SetParent(playerTeam.transform);
                    }
                    else
                    {
                        GameObject _newUnit = Instantiate(enemyArcher, trueCreatePlace, Quaternion.identity);
                        gameManager.enemyTeam.Add(_newUnit);
                        _newUnit.transform.SetParent(enemyTeam.transform);
                    }
                    break;
                case "shieldman":
                    if (isPlayerUnit)
                    {
                        GameObject _newUnit = Instantiate(shieldman, trueCreatePlace, Quaternion.identity);
                        gameManager.playerTeam.Add(_newUnit);
                        _newUnit.transform.SetParent(playerTeam.transform);
                    }
                    else
                    {
                        GameObject _newUnit = Instantiate(enemyShieldman, trueCreatePlace, Quaternion.identity);
                        gameManager.enemyTeam.Add(_newUnit);
                        _newUnit.transform.SetParent(enemyTeam.transform);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}

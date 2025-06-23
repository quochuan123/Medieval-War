using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandCanvas : MonoBehaviour
{
    public bool isHold;
    public bool isAttackFreely;
    public bool isRetreat;
    public bool isMovingInSync;
    public bool isNotBreakLineup;

    private GameManager gameManager;
    private CanvasManager cm;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        cm = FindObjectOfType<CanvasManager>();
        AttackCommand();
        SyncCommandDisable();
    }


    public void AttackCommand()
    {
        isAttackFreely = true;
        isHold = false;
        isRetreat = false;
        isNotBreakLineup = false;

        cm.holdCheck.SetActive(false);
        cm.attackCheck.SetActive(true);
        cm.retreatCheck.SetActive(false);
        cm.notBreakLineupCheck.SetActive(false);


        foreach (GameObject unit in gameManager.playerTeam)
        {
            unit.GetComponent<Unit>().attack = true;
            unit.GetComponent<Unit>().retreat = false;
            unit.GetComponent<Unit>().hold = false;
            unit.GetComponent<Unit>().dontBreakLineup = false;


        }
    }
    public void NotBreakLineUpCommand()
    {
        isAttackFreely = false;
        isNotBreakLineup = true;
        isHold = false;
        isRetreat = false;
        cm.holdCheck.SetActive(false);
        cm.attackCheck.SetActive(false);
        cm.retreatCheck.SetActive(false);
        cm.notBreakLineupCheck.SetActive(true);


        foreach (GameObject unit in gameManager.playerTeam)
        {
            unit.GetComponent<Unit>().attack = false;
            unit.GetComponent<Unit>().retreat = false;
            unit.GetComponent<Unit>().hold = false;
            unit.GetComponent<Unit>().dontBreakLineup = true;
        }
    }

    public void HoldCommand()
    {
        isAttackFreely = false;
        isHold = true;
        isNotBreakLineup = false;
        isRetreat = false;
        cm.holdCheck.SetActive(true);
        cm.attackCheck.SetActive(false);
        cm.retreatCheck.SetActive(false);
        cm.notBreakLineupCheck.SetActive(false);

        foreach (GameObject unit in gameManager.playerTeam)
        {
            unit.GetComponent<Unit>().attack = false;
            unit.GetComponent<Unit>().retreat = false;
            unit.GetComponent<Unit>().hold = true;
            unit.GetComponent<Unit>().dontBreakLineup = false;


        }
    }

    public void RetreatCommand()
    {
        isAttackFreely = false;
        isHold = false;
        isRetreat = true;
        isNotBreakLineup = false;
        cm.holdCheck.SetActive(false);
        cm.attackCheck.SetActive(false);
        cm.retreatCheck.SetActive(true);
        cm.notBreakLineupCheck.SetActive(false);

        foreach(GameObject unit in gameManager.playerTeam)
        {
            unit.GetComponent<Unit>().attack = false;
            unit.GetComponent<Unit>().retreat = true;
            unit.GetComponent<Unit>().hold = false;
            unit.GetComponent<Unit>().dontBreakLineup = false;

        }
    }   
    
    public void SyncCommandEnable()
    {
        isMovingInSync = true;
        cm.movingSyncDisable.SetActive(true);
        cm.movingSyncEnable.SetActive(false);

        if (gameManager.playerTeam.Count > 0)
        {
            foreach (GameObject unit in gameManager.playerTeam)
            {
                unit.GetComponent<Unit>().syncAttack = true;
            }
        }
    }

    public void SyncCommandDisable()
    {
        isMovingInSync = false;
        cm.movingSyncDisable.SetActive(false);
        cm.movingSyncEnable.SetActive(true);

        if (gameManager.playerTeam.Count > 0)
        {
            foreach (GameObject unit in gameManager.playerTeam)
            {
                unit.GetComponent<Unit>().syncAttack = false;
            }
        }
        
    }


}

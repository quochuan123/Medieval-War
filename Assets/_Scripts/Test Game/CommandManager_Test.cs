using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandCanvas_Test : MonoBehaviour
{
    public bool isHold;
    public bool isAttackFreely;
    public bool isRetreat;
    public bool isMovingInSync;
    public bool isNotBreakLineup;

    private GameManager gameManager;
    private TestCanvasManager tcm;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        tcm = FindObjectOfType<TestCanvasManager>();
        //isAttackFreely = true;



        //tcm.holdCheck.SetActive(false);
        //tcm.attackCheck.SetActive(true);
        //tcm.retreatCheck.SetActive(false);
        //tcm.notBreakLineupCheck.SetActive(false);

        // isMovingInSync = false;
        // tcm.movingSyncDisable.SetActive(false);
        // tcm.movingSyncEnable.SetActive(true);
        SyncCommandEnable();
        AttackCommand();
    }


    public void AttackCommand()
    {
        isAttackFreely = true;
        isHold = false;
        isRetreat = false;
        isNotBreakLineup = false;

        tcm.holdCheck.SetActive(false);
        tcm.attackCheck.SetActive(true);
        tcm.retreatCheck.SetActive(false);
        tcm.notBreakLineupCheck.SetActive(false);


        foreach (GameObject unit in gameManager.playerTeam)
        {
            unit.GetComponent<Unit>().attack = true;
            unit.GetComponent<Unit>().retreat = false;
            unit.GetComponent<Unit>().hold = false;
            unit.GetComponent<Unit>().dontBreakLineup = false;
            unit.GetComponent<Unit>().target = null;



        }
    }
    public void NotBreakLineUpCommand()
    {
        isAttackFreely = false;
        isNotBreakLineup = true;
        isHold = false;
        isRetreat = false;
        tcm.holdCheck.SetActive(false);
        tcm.attackCheck.SetActive(false);
        tcm.retreatCheck.SetActive(false);
        tcm.notBreakLineupCheck.SetActive(true);


        foreach (GameObject unit in gameManager.playerTeam)
        {
            unit.GetComponent<Unit>().attack = false;
            unit.GetComponent<Unit>().retreat = false;
            unit.GetComponent<Unit>().hold = false;
            unit.GetComponent<Unit>().dontBreakLineup = true;
            unit.GetComponent<Unit>().target = null;

        }
    }

    public void HoldCommand()
    {
        isAttackFreely = false;
        isHold = true;
        isNotBreakLineup = false;
        isRetreat = false;
        tcm.holdCheck.SetActive(true);
        tcm.attackCheck.SetActive(false);
        tcm.retreatCheck.SetActive(false);
        tcm.notBreakLineupCheck.SetActive(false);

        foreach (GameObject unit in gameManager.playerTeam)
        {
            unit.GetComponent<Unit>().attack = false;
            unit.GetComponent<Unit>().retreat = false;
            unit.GetComponent<Unit>().hold = true;
            unit.GetComponent<Unit>().dontBreakLineup = false;
            unit.GetComponent<Unit>().target = null;


        }
    }

    public void RetreatCommand()
    {
        isAttackFreely = false;
        isHold = false;
        isRetreat = true;
        isNotBreakLineup = false;
        tcm.holdCheck.SetActive(false);
        tcm.attackCheck.SetActive(false);
        tcm.retreatCheck.SetActive(true);
        tcm.notBreakLineupCheck.SetActive(false);

        foreach (GameObject unit in gameManager.playerTeam)
        {
            unit.GetComponent<Unit>().attack = false;
            unit.GetComponent<Unit>().retreat = true;
            unit.GetComponent<Unit>().hold = false;
            unit.GetComponent<Unit>().dontBreakLineup = false;
            unit.GetComponent<Unit>().target = null;

        }
    }

    public void SyncCommandEnable()
    {
        isMovingInSync = true;
        tcm.movingSyncDisable.SetActive(false);
        tcm.movingSyncEnable.SetActive(true);

        if (gameManager.playerTeam.Count > 0)
        {
            foreach (GameObject unit in gameManager.playerTeam)
            {
                unit.GetComponent<Unit>().syncAttack = true;
                unit.GetComponent<Unit>().target = null;

            }
        }
    }

    public void SyncCommandDisable()
    {
        isMovingInSync = false;
        tcm.movingSyncDisable.SetActive(true);
        tcm.movingSyncEnable.SetActive(false);

        if (gameManager.playerTeam.Count > 0)
        {
            foreach (GameObject unit in gameManager.playerTeam)
            {
                unit.GetComponent<Unit>().syncAttack = false;
                unit.GetComponent<Unit>().target = null;

            }
        }

    }


}

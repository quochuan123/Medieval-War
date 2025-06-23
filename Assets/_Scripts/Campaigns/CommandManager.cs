using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public GameObject back;
    public GameObject startBattleButton;
    public Node node;
    public ArmyInfor _refArmy;
    private CampaignManager cm;
    private SingletonScript sts;
    // Start is called before the first frame update
    private void Start()
    {
        cm = FindObjectOfType<CampaignManager>();
        sts = FindObjectOfType<SingletonScript>();

    }
    public void OnMoveButtonClicked()
    {
        back.SetActive(true);

        for(int i = 0; i < cm.nodeObjects.Count; i++)
        {
            cm.nodeObjects[i].GetComponent<NodeView>().arrow.SetActive(false);
        }

        for (int i = 0; i < node.connectedNodeIDs.Count; i++)
        {
            if (!sts.mapState.allNodes[node.connectedNodeIDs[i]].controlledByPlayer || 
                (sts.mapState.allNodes[node.connectedNodeIDs[i]].controlledByPlayer && 
                !sts.controllersList.Any(b => b.node.nodeID == node.connectedNodeIDs[i])))
            {
                cm.nodeObjects[sts.mapState.allNodes[node.connectedNodeIDs[i]].ObjectID].GetComponent<NodeView>().arrow.SetActive(true);
                cm.nodeObjects[sts.mapState.allNodes[node.connectedNodeIDs[i]].ObjectID].GetComponent<NodeView>().aura.SetActive(true);
            }

            cm.prevNode = node;
            cm.commandObj.SetActive(false);
            cm.controller.GetComponent<CanvasGroup>().interactable = false;

        }
    }

    public void Back()
    {
        cm.commandObj.SetActive(true);
        foreach(var army in cm.armyObjects)
        {
            army.gameObject.GetComponent<Animator>().Play("Army Idle");
        }
        for (int i = 0; i < cm.nodeObjects.Count; i++)
        {
            cm.nodeObjects[i].GetComponent<NodeView>().arrow.SetActive(false);
            cm.nodeObjects[i].GetComponent<NodeView>().aura.SetActive(false);
        }
        back.SetActive(false);
        startBattleButton.SetActive(false);
        cm.controller.GetComponent<CanvasGroup>().interactable = true;
        cm.commandObj.SetActive(false);
        cm.enemyStatus.SetActive(false);

    }
}

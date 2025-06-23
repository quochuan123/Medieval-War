using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ControllerButton : MonoBehaviour
{
    public Image exhaustBar;
    public Text armyLvl;
    public Text armyName;
    public Image xpBar;
    public GameObject _animation;
    public Node node;
    public ArmyInfor _refArmy;
    private CampaignManager cm;
    private SingletonScript sts;

    private void Start()
    {
        cm = FindObjectOfType<CampaignManager>();
        sts = FindObjectOfType<SingletonScript>();
    }

    public void OnArmyControlButton()
    {
        cm.commandObj.SetActive(true);
        cm.commandObj.GetComponent<CommandManager>().node = node;
        cm.commandObj.GetComponent<CommandManager>()._refArmy = _refArmy;

        foreach(var _army in cm.armyObjects)
        {
            _army.GetComponent<Animator>().Play("Army Idle");
        }

        foreach(var arrow in cm.nodeObjects)
        {
            arrow.GetComponent<NodeView>().arrow.SetActive(false);
        }


        cm.nodeObjects[node.nodeID].GetComponent<NodeView>().arrow.SetActive(true);
        cm.nodeObjects[node.nodeID].GetComponent<NodeView>().arrow.GetComponent<Animator>().Play("Green_Arrow");

        GameObject army = cm.armyObjects
            .FirstOrDefault(a => a.GetComponent<ArmyController>().armyData.armyID == node.occupyingArmyID);

        if(army != null)
        {
            army.GetComponent<Animator>().Play("Army Run");
        }
    }



}

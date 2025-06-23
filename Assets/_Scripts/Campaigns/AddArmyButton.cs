using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class AddArmyButton : MonoBehaviour
{
    public Text nodeName;
    public Node node;
    public ArmyInfor _refArmy;
    public bool isRegistered;
    private CampaignManager manager;
    private AddArmyWindow aaw;
    private ChooseArmyWindow caw;
    private SingletonScript sts;
    private void Start()
    {
        sts = FindObjectOfType<SingletonScript>();
        manager = FindObjectOfType<CampaignManager>();
        aaw = FindObjectOfType<AddArmyWindow>(true);
        caw = FindObjectOfType<ChooseArmyWindow>(true);
    }
    public void OnRegisterButtonClicked()
    {
        for(int i= 0; i < manager.nodeObjects.Count; i++)
        {
            manager.nodeObjects[i].GetComponent<NodeView>().arrow.SetActive(false);
        }

        manager.nodeObjects[node.ObjectID].GetComponent<NodeView>().arrow.SetActive(true);
        manager.nodeObjects[node.ObjectID].GetComponent<NodeView>().arrow.GetComponent<Animator>().Play("Green_Arrow");
        aaw._refAddArmyButton = gameObject.GetComponent<AddArmyButton>();
        manager.armyChoose.SetActive(true);
        caw.OpenChooseArmyButton(sts,aaw.registerButtonList);
       
    }

    
}

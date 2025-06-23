using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class AddArmyWindow : MonoBehaviour
{
    public GameObject okButton;
    private CampaignManager cm;
    public List<Node> playerNodes;
    public List<GameObject> registerButtonList;
    public AddArmyButton _refAddArmyButton;
    private SingletonScript sts;
    


    public GameObject addArmyWindow;
    void Start()
    {
        sts = FindObjectOfType<SingletonScript>();
        cm = FindObjectOfType<CampaignManager>();
        
    }

    public void StartRegister(SingletonScript _sts, CampaignManager _cm)
    {
        if (!_sts.isStartStrategy)
        {
            _sts.isStartStrategy = true;

            addArmyWindow.SetActive(true);
            playerNodes = _sts.mapState.allNodes
                .Where(n=>n.controlledByPlayer).ToList();

            for(int i = 0; i < 6; i++)
            {
                bool isPlayer =i< playerNodes.Count;
                registerButtonList[i].SetActive(isPlayer);
                if (registerButtonList[i].activeSelf)
                {
                    registerButtonList[i].GetComponent<AddArmyButton>().node = playerNodes[i];
                }
            }
        }
        else
        {
            _cm.controller.SetActive(true);
            addArmyWindow.SetActive(false);
        }    
    }

    public void OnOkButtonClicked()
    {
        sts.controllersList.Clear();
        
        for(int i = 0; i < 6; i++)
        {
            if (registerButtonList[i].activeSelf && registerButtonList[i].GetComponent<AddArmyButton>().isRegistered)
            {
                ArmyData newArmyData = new ArmyData(registerButtonList[i].GetComponent<AddArmyButton>()._refArmy.name,sts.mapState.allArmies.Count, true,
                                    registerButtonList[i].GetComponent<AddArmyButton>().node.nodeID,
                                       registerButtonList[i].GetComponent<AddArmyButton>()._refArmy.lvl,
                                       false);
                sts.mapState.allArmies.Add(newArmyData);
                GameObject newArmy = Instantiate(cm.army);
                newArmy.GetComponent<ArmyController>().armyData = newArmyData;
                newArmy.GetComponent<ArmyController>()._ref = registerButtonList[i].GetComponent<AddArmyButton>()._refArmy;
                newArmy.transform.position = registerButtonList[i].GetComponent<AddArmyButton>().node.worldPosition;
                newArmy.transform.position = new Vector3(newArmy.transform.position.x, newArmy.transform.position.y, -2);
                Node _refNode = registerButtonList[i].GetComponent<AddArmyButton>().node;
                _refNode.occupyingArmyID = newArmyData.armyID;
                cm.armyObjects.Add(newArmy);

                sts.controllersList.Add(new ControllerSave(registerButtonList[i].GetComponent<AddArmyButton>().node, registerButtonList[i].GetComponent<AddArmyButton>()._refArmy
                    , registerButtonList[i].GetComponent<AddArmyButton>().isRegistered, newArmyData
                    ));

            }
            else
                continue;


        }
        cm.controller.SetActive(true);
        addArmyWindow.SetActive(false);
        //cm.controller.GetComponent<ControllerManager>().OpenControlArmyWindow(registerButtonList, this);

    }

    

    
}

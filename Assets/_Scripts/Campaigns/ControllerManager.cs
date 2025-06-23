using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    public List<GameObject> armyControlButtonsList;

    private SingletonScript sts;
    // Start is called before the first frame update
    void Start()
    {
        sts = FindObjectOfType<SingletonScript>();
        OpenControlArmyWindow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenControlArmyWindow()
    {
        for (int i = 0;i < 6; i++)
        {
            bool active = i < sts.controllersList.Count;
            armyControlButtonsList[i].SetActive(active);
            if (active)
            {
                armyControlButtonsList[i].GetComponent<ControllerButton>().node = sts.controllersList[i].node;
                if(sts.controllersList[i].isRegistered)
                {
                    ControllerButton button = armyControlButtonsList[i].GetComponent<ControllerButton>();
                    button._refArmy = sts.controllersList[i]._refArmy;
                    button.armyLvl.text = "Level "+button._refArmy.lvl.ToString();
                    button.armyName.text = button._refArmy.name.ToString();
                    button.exhaustBar.fillAmount = 1- sts.mapState.allArmies[button.node.occupyingArmyID].exhaustedLvl;

                    

                    button.xpBar.fillAmount = (float)button._refArmy.xp / button._refArmy.maxXp;

                    var army = sts.mapState.allArmies
                        .FirstOrDefault(a => a.name == sts.controllersList[i]._refArmy.name);
                    if(army != null)
                    {
                        army.lvl = sts.controllersList[i]._refArmy.lvl;
                    }
                }
                else
                {
                    armyControlButtonsList[i].SetActive(false);
                }    
            }
        }

    }
}

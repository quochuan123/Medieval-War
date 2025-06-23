using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChooseArmyVillagerScenePanel : MonoBehaviour
{
    private SingletonScript sts;
    public List<GameObject> selectArmyButtons;
    private VillagerResourcesManager vrm;
    private PrinceController player;
    // Start is called before the first frame update
    void Start()
    {
        sts = FindObjectOfType<SingletonScript>();
        vrm = FindObjectOfType<VillagerResourcesManager>();
        player = FindObjectOfType<PrinceController>();
    }

    public void OpenSelectPanel(SingletonScript _sts)
    {
        for (int i = 0; i < 6; i++)
        {
            bool activeArmy = i < _sts.armies.Count;
            selectArmyButtons[i].SetActive(activeArmy);
            if (activeArmy)
            {
                ChooseArmyInVillagerScene button = selectArmyButtons[i].GetComponent<ChooseArmyInVillagerScene>();
                button._armyInfor = _sts.armies[i];
                button.armyName.text = button._armyInfor.name;
                button.armyLvl.text = "Level "+button._armyInfor.lvl.ToString();
                button.xpBar.fillAmount = (float)button._armyInfor.xp / button._armyInfor.maxXp;
            }
        }
    }

    public void Back()
    {
        vrm.hunterConversation.SetActive(false);
        player.isTalk = false;
        vrm.selectArmy.SetActive(false);

    }


}

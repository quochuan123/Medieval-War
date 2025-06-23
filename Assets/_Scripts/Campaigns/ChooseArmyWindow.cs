using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChooseArmyWindow : MonoBehaviour
{
    private SingletonScript sts;
    private AddArmyWindow aaw;
    public List<GameObject> chooseArmyButtonsList;
    // Start is called before the first frame update
    void Start()
    {
        sts = FindObjectOfType<SingletonScript>();
        aaw = FindObjectOfType<AddArmyWindow>();


    }

    public void OpenChooseArmyButton(SingletonScript _sts, List<GameObject> _registerButtonList)
    {
        for (int i = 0; i < 6; i++)
        {
            bool available = i < _sts.armies.Count;
            chooseArmyButtonsList[i].SetActive(available);
            if (available)
            {
                ChooseArmyButton chooseButton = chooseArmyButtonsList[i].GetComponent<ChooseArmyButton>();
                chooseButton._refArmyInfo = _sts.armies[i];
                chooseButton.armyName.text = _sts.armies[i].name;
                chooseButton.armyLvl.text = "Level "+_sts.armies[i].lvl.ToString();
                chooseButton.xpBar.fillAmount = (float)_sts.armies[i].xp / _sts.armies[i].maxXp;
                GameObject army = _registerButtonList
                    .Where(a => a.GetComponent<AddArmyButton>()._refArmy == _sts.armies[i]).
                    FirstOrDefault();
                if (army != null)
                {
                    chooseArmyButtonsList[i].SetActive(false);

                }
            }
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KingdomStatus : MonoBehaviour
{
    public Text tipText;
    public List<GameObject> armiesKdList;
    private SingletonScript sts;
    private VillagerResourcesManager rm;
    // Start is called before the first frame update
    void Start()
    {
        sts = FindObjectOfType<SingletonScript>();
        rm = FindObjectOfType<VillagerResourcesManager>();
    }


    public void OpenKingdomStatus()
    {
        rm.KdStatusGameObject.SetActive(true);
        UpdateResources();
        UpdateArmy();
    }

    public void UpdateResources()
    {
        rm.goldKd.text = sts.gold.ToString();
        rm.volunteersKd.text = sts.volunteers.ToString();
        rm.unitAmount.text = sts.unitAmountCap.ToString();

        rm.knightAmountKd.text = sts.knightAmount.ToString();
        rm.knightAmountKd.color = sts.knightAmount >= sts.knightCap ? Color.red : Color.gray;

        rm.archerAmountKd.text = sts.archerAmount.ToString();
        rm.archerAmountKd.color = sts.archerAmount >= sts.archerCap ? Color.red : Color.gray;

        rm.crossbowAmountKd.text = sts.crossbowAmount.ToString();
        rm.crossbowAmountKd.color = sts.crossbowAmount >= sts.crossbowCap ? Color.red : Color.gray;

        rm.archmageAmountKd.text = sts.archmageAmount.ToString();
        rm.archmageAmountKd.color = sts.archmageAmount >= sts.archmageCap ? Color.red : Color.gray;

        rm.mageAmountKd.text = sts.mageAmount.ToString();
        rm.mageAmountKd.color = sts.mageAmount >= sts.mageCap ? Color.red : Color.gray;

        rm.shieldmanAmountKd.text = sts.shieldmanAmount.ToString();
        rm.shieldmanAmountKd.color = sts.shieldmanAmount >= sts.shieldmanCap ? Color.red : Color.gray;

        rm.halbertAmountKd.text = sts.halbertAmount.ToString();
        rm.halbertAmountKd.color = sts.halbertAmount >= sts.halbertCap ? Color.red : Color.gray;
    }

    public void UpdateArmy()
    {
        for (int i = 0; i < armiesKdList.Count; i++)
        {
            bool haveArmy = i < sts.armies.Count;
            ArmyStatus _as = armiesKdList[i].GetComponent<ArmyStatus>();
            _as.showArmy.SetActive(haveArmy);

            if (haveArmy)
            {
                _as.armyName.text = sts.armies[i].name;
                _as.armyLvl.text = "Level " + sts.armies[i].lvl.ToString();
                _as.armyCurrXp.text = sts.armies[i].xp + "/" + sts.armies[i].maxXp;
                _as.xpBar.fillAmount = (float)sts.armies[i].xp / sts.armies[i].maxXp;
            }
        }
    }

    public void OnPointerEnterEvent(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;
        GameObject hoveredGO = pointerData.pointerEnter;

        switch (hoveredGO.tag)
        {
            case "gold":
                tipText.text = "The kingdom's gold resources.";
                break;
            case "volunteer":
                tipText.text = "The kingdom's people resources.";
                break;
            case "unit_amount":
                tipText.text = "This number is how many units you can deploy in a battle.";
                break;
            case "knight":
                tipText.text = "Your knight amount. " + sts.knightAmount.ToString() + "/" + sts.knightCap.ToString();
                break;
            case "archer":
                tipText.text = "Your archer amount. " + sts.archerAmount.ToString() + "/" + sts.archerCap.ToString();

                break;
            case "archmage":
                tipText.text = "Your archmage amount. " + sts.archmageAmount.ToString() + "/" + sts.archmageCap.ToString();

                break;
            case "halbert":
                tipText.text = "Your halberdman amount. " + sts.halbertAmount.ToString() + "/" + sts.halbertCap.ToString();

                break;
            case "shieldman":
                tipText.text = "Your shieldman amount. " + sts.shieldmanAmount.ToString() + "/" + sts.shieldmanCap.ToString();

                break;
            case "crossbow":
                tipText.text = "Your crossbowman amount. " + sts.crossbowAmount.ToString() + "/" + sts.crossbowCap.ToString();

                break;
            case "mage":
                tipText.text = "Your mage amount. " + sts.mageAmount.ToString() + "/" + sts.mageCap.ToString();

                break;
            default:
                break;
        }
    }

    public void OnPointerExitEvent(BaseEventData data)
    {
        tipText.text = "";
    }

    public void ExitKingdomStatus()
    {
        rm.KdStatusGameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CampaignMapButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string campaignName;
    private CampaignInterfaceManager cim;
    private VillagerResourcesManager rm;
    public Text campaignText;
    public GameObject campaignHighlight;
    private SingletonScript sts;

    void Start()
    {
        cim = FindObjectOfType<CampaignInterfaceManager>();
        sts = FindObjectOfType<SingletonScript>();
        rm = FindObjectOfType<VillagerResourcesManager>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        campaignText.color = Color.white;
        campaignHighlight.SetActive(true);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (EventSystem.current.currentSelectedGameObject != gameObject)
        {
            campaignText.color = Color.gray;
            campaignHighlight.SetActive(false);
        }

    }

    public void OnClick()
    {
        foreach (var button in cim.cpButtons)
        {
            button.campaignText.color = Color.gray;
            button.campaignHighlight.SetActive(false);
        }

        campaignHighlight.SetActive(true);
        campaignText.color = Color.white;
        sts.nextCampaign = campaignName;

        switch (campaignName)
        {
            case "mist_mountain":
                rm.detailCPName.text = "The Mist Mountain Campaign";
                rm.detailDesName.text = "First and easist campaign.";
                rm.detailAmountName.text = "1 army";
                rm.detailLvlRecName.text = "Level 1";
                break;
            case "strong_river":
                rm.detailCPName.text = "The Strong River Campaign";
                rm.detailDesName.text = "Third campaign.";
                rm.detailAmountName.text = "3 armies";
                rm.detailLvlRecName.text = "Level 10";
                break;
            case "dawn_valley":
                rm.detailCPName.text = "The Dawn Valley Campaign";
                rm.detailDesName.text = "Second campaign.";
                rm.detailAmountName.text = "2 armies";
                rm.detailLvlRecName.text = "Level 5";
                break;
            case "dead_kingdom":
                rm.detailCPName.text = "The Dead Kingdom Campaign";
                rm.detailDesName.text = "forth campaign.";
                rm.detailAmountName.text = "4 armies";
                rm.detailLvlRecName.text = "Level 15";
                break;
            case "skyfall":
                rm.detailCPName.text = "The Skyfall Campaign";
                rm.detailDesName.text = "fifth campaign.";
                rm.detailAmountName.text = "4 armies";
                rm.detailLvlRecName.text = "Level 20";
                break;
            default:
                rm.detailCPName.text = "The Strong River Campaign";
                rm.detailDesName.text = "Second campaign.";
                rm.detailAmountName.text = "3 armies";
                rm.detailLvlRecName.text = "Level 10";
                break;
        }

    }



    // Update is called once per frame
    void Update()
    {

    }
}

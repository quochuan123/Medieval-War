using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CampaignInterfaceManager : MonoBehaviour
{
    public List<CampaignMapButton> cpButtons;
    private PrinceController player;
    private VillagerManager vm;
    private VillagerResourcesManager rm;
    private SingletonScript sts;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PrinceController>();
        sts = FindObjectOfType<SingletonScript>();
        rm = FindObjectOfType<VillagerResourcesManager>();
        vm = FindObjectOfType<VillagerManager>();
    }

    public void SetUpCampaign(SingletonScript _sts, VillagerResourcesManager _rm)
    {
         CampaignMapButton setbutton = cpButtons
            .FirstOrDefault(a => a.campaignName == "mist_mountain");
        EventSystem.current.SetSelectedGameObject(setbutton.gameObject);

        setbutton.campaignHighlight.SetActive(true);
        setbutton.campaignText.color = Color.white;
        _sts.nextCampaign = setbutton.campaignName;
        _rm.detailCPName.text = "The Mist Mountain Campaign";
        _rm.detailDesName.text = "First and easist campaign.";
        _rm.detailAmountName.text = "1 army";
        _rm.detailLvlRecName.text = "Level 1";
    }


    public void CloseInterface()
    {
        player.isTalk = false;
        rm.disableHud.SetActive(false);
        rm.campaignInterface.SetActive(false);
        SoundManager sm = FindObjectOfType<SoundManager>();
        if (sm != null)
        {
            sm.InterfaceMusicOff();
        }
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene("Campaign Scene");
    }
}

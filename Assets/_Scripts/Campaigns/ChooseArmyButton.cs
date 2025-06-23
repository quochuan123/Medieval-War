using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChooseArmyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text armyName;
    public Text armyLvl;
    public Image xpBar;
    public GameObject effect;
    public ArmyInfor _refArmyInfo;
    private AddArmyWindow aaw;
    private CampaignManager manager;
    void Start()
    {
        manager = FindObjectOfType<CampaignManager>();
        aaw = FindObjectOfType<AddArmyWindow>();
    }

    
    public void OnChooseArmyButtonClicked()
    {
        aaw._refAddArmyButton._refArmy = _refArmyInfo;
        aaw._refAddArmyButton.isRegistered = true;
        aaw._refAddArmyButton.nodeName.text = _refArmyInfo.name;
        manager.armyChoose.SetActive(false);
        for(int i = 0; i < manager.nodeObjects.Count; i++)
        {
            manager.nodeObjects[i].GetComponent<NodeView>().arrow.SetActive(false);
        }

        aaw.okButton.GetComponent<CanvasGroup>().interactable = true;
    }

    public void Back()
    {
        manager.armyChoose.SetActive(false);
        for (int i = 0; i < manager.nodeObjects.Count; i++)
        {
            manager.nodeObjects[i].GetComponent<NodeView>().arrow.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(gameObject.activeSelf)
        effect.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (gameObject.activeSelf)
            effect.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBattleButton : MonoBehaviour
{
    private SingletonScript sts;
    private CampaignManager cm;
    private void Start()
    {
        sts = FindObjectOfType<SingletonScript>();
        cm = FindObjectOfType<CampaignManager>();
    }
    public void OnStartBattleBattleClicked()
    {
        int unitAmount = sts.knightAmount + sts.mageAmount + sts.archerAmount + sts.archmageAmount + sts.shieldmanAmount + sts.crossbowAmount + sts.halbertAmount;
        if(unitAmount > 0)
            SceneManager.LoadScene("BattleScene");
        else
        {
            StartCoroutine(ShowOutOfStock());
        }
    }
    IEnumerator ShowOutOfStock()
    {
        cm.outOfStock.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        cm.outOfStock.SetActive(false);

    }


}

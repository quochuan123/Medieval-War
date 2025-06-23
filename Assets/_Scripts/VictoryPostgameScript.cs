using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VictoryPostgameScript : MonoBehaviour
{
    private GameManager gameManager;
    private CanvasManager cm;
    private SingletonScript sts;
    void Start()
    {
        sts = FindObjectOfType<SingletonScript>();
        gameManager = FindObjectOfType<GameManager>();
        cm = FindObjectOfType<CanvasManager>();
        PostGame();
    }

    public void PostGame()
    {
        cm.goldRecieve.text = sts.goldNextBattle.ToString();
        cm.recruitReceive.text = sts.recruitsNextBattle.ToString();

        cm.armyNamePostGame.text = sts.playerData.name;


        sts.gold += sts.goldNextBattle;
        sts.volunteers += sts.recruitsNextBattle;

        sts.KingdomMaxExpCalculate();
        ArmyInfor army = sts._refArmyInfor;
        if (army != null)
        {
            army.MaxXPCalculate();
        }
        //sts.GainXp(1000);


        StartCoroutine(XPGainProcess(sts.xpNextBattle, army));
    }

    IEnumerator XPGainProcess(int xpGainAmount, ArmyInfor _army)
    {

        int armyXpGainAmount = xpGainAmount;
        int kdXpGainAmount = xpGainAmount;

        int armyLvl = _army.lvl;
        int armyCurrXp = _army.xp;
        int armyMaxXp = _army.maxXp;

        int kdLvl = sts.kingdomLvl;
        int kdCurrXp = sts.kingdomLvlExp;
        int kdMaxXp = sts.kingdomLvlMaxExp;

        cm.kdXPBar.fillAmount = (float)sts.kingdomLvlExp / sts.kingdomLvlMaxExp;
        cm.kdLvlPostScrn.text = "Level "+sts.kingdomLvl.ToString();

        cm.armyXPBar.fillAmount = (float)_army.xp / _army.maxXp;
        cm.armyLvlPostScrn.text ="Level "+ _army.lvl.ToString();

        if(_army.lvl < _army.lvlCap)
        _army.GainXp(xpGainAmount);
        else
        {
            cm.armyXPBar.fillAmount = 1;
        }

        if(sts.kingdomLvl < 20)
        sts.GainXp(xpGainAmount);
        else
        {
            cm.armyXPBar.fillAmount = 1;
        }
        while (armyXpGainAmount > 0 || kdXpGainAmount > 0)
        {
            if (_army.lvl >= 20)
                armyXpGainAmount = 0;
            if (sts.kingdomLvl >= 20)
                kdXpGainAmount = 0;
            if (armyXpGainAmount > 0)
            {
                if (armyXpGainAmount > 1 + Mathf.RoundToInt(armyXpGainAmount * 5 / armyMaxXp))
                {
                    armyCurrXp += (1 + Mathf.RoundToInt(armyXpGainAmount * 5 / armyMaxXp));
                    armyXpGainAmount -= (1 + Mathf.RoundToInt(armyXpGainAmount * 5 / armyMaxXp));
                }
                else
                {
                    armyCurrXp += armyXpGainAmount;
                    armyXpGainAmount = 0;

                }

                cm.armyXPBar.fillAmount = (float)armyCurrXp / armyMaxXp;

                if (armyCurrXp >= armyMaxXp)
                {
                    armyLvl++;
                    cm.armyLvlPostScrn.text = "Level "+armyLvl.ToString();
                    int xpLeftOver = armyCurrXp - armyMaxXp;
                    armyXpGainAmount += xpLeftOver;
                    armyCurrXp = 0;
                    armyMaxXp = ArmyMaxXPCal(armyLvl);
                }
            }

            if (kdXpGainAmount > 0)
            {
                if (kdXpGainAmount > 1 + Mathf.RoundToInt(kdXpGainAmount * 5 / kdMaxXp))
                {
                    kdCurrXp += (1 + Mathf.RoundToInt(kdXpGainAmount * 5 / kdMaxXp));
                    kdXpGainAmount -= (1 + Mathf.RoundToInt(kdXpGainAmount * 5 / kdMaxXp));
                }
                else
                {
                    kdCurrXp += kdXpGainAmount;
                    kdXpGainAmount = 0;

                }

                cm.kdXPBar.fillAmount = (float)kdCurrXp / kdMaxXp;

                if (kdCurrXp >= kdMaxXp)
                {
                    kdLvl++;
                    cm.kdLvlPostScrn.text = "Level " + kdLvl.ToString();
                    int xpLeftOver = kdCurrXp - kdMaxXp;
                    kdXpGainAmount += xpLeftOver;
                    kdCurrXp = 0;
                    kdMaxXp = KdMaxXPCal(kdLvl);
                }
            }



            yield return 0.1f;
        }
    }

    public int ArmyMaxXPCal(int lvl)
    {
        return 200 * (lvl * 2 + 1);

    }
    public int KdMaxXPCal(int lvl)
    {
        return  300 * (lvl * 2 + 1);
    }
}
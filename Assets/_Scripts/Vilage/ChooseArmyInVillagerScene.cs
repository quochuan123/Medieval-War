using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseArmyInVillagerScene : MonoBehaviour
{
    public ArmyInfor _armyInfor;
    private SingletonScript sts;

    public Text armyName;
    public Text armyLvl;
    public Image xpBar;
    // Start is called before the first frame update
    private void Start()
    {
        sts = FindObjectOfType<SingletonScript>();
    }
    public void OnClick()
    {
        sts._refArmyInfor = _armyInfor;
        sts.enemyLvl = sts.kingdomLvl + 1;
        sts.goldNextBattle = Random.Range(100 * sts.kingdomLvl, 200 * sts.kingdomLvl);
        sts.recruitsNextBattle = Random.Range(Mathf.RoundToInt(sts.kingdomLvl *2.5f), Mathf.RoundToInt(sts.kingdomLvl * 3f));
        sts.xpNextBattle = Random.Range(Mathf.RoundToInt(sts.kingdomLvlMaxExp / 3), Mathf.RoundToInt(sts.kingdomLvlMaxExp /2));
        sts.onlyBattle = true;
        sts.nextMap = "villager_underattack";
        SceneManager.LoadScene("BattleScene");
    }
}

   

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SingletonScript : MonoBehaviour
{

    //Village Scene
    public int volunteers = 0;
    public int gold = 0;
    public List<ArmyInfor> armies;

    public int kingdomLvl = 1;
    public int kingdomLvlExp;
    public int kingdomLvlMaxExp;
    public int kingdomLvlCap;

    public int unitAmountCap;
    public int knightAmount;
    public int knightCap;
    public int archerAmount;
    public int archerCap;

    public int halbertAmount;
    public int halbertCap;

    public int shieldmanAmount;
    public int shieldmanCap;

    public int mageAmount;
    public int mageCap;


    public int archmageAmount;
    public int archmageCap;

    public int crossbowAmount;
    public int crossbowCap;


    //Battle
    public string nextMap;
    public Node nextNode;
    public Node prevNode;
    public ArmyData enemyData;
    public int enemyLvl;
    public ArmyData playerData;
    public ArmyInfor _refArmyInfor;
    public int enemyEncircleBonus;
    public int playerEncircleBonus;
    public int goldNextBattle;
    public int recruitsNextBattle;
    public int xpNextBattle;
    public string dangerLvl;

    public bool onlyBattle;
    //Campaign
    public string nextCampaign;
    public MapState mapState;
    public bool isStartStrategy = false;
    public List<ControllerSave> controllersList = new List<ControllerSave>();

    public static SingletonScript Instance;
    private VillagerManager vm;
    //Savedata varibles
    
    void Start()
    {
        vm = FindObjectOfType<VillagerManager>();
        foreach (var army in armies)
        {
            army.MaxXPCalculate();
        }

        KingdomMaxExpCalculate();
    }
    

    


    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Gán instance và giữ lại khi chuyển scene
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    

    public void CapCalculate()
    {
        knightCap = Mathf.RoundToInt(kingdomLvl * 2.25f);
        knightAmount = knightCap >= knightAmount ? knightAmount : knightCap;

        archerCap = Mathf.RoundToInt(kingdomLvl * 1.65f);
        archerAmount = archerCap >= archerAmount ? archerAmount : archerCap;

        archmageCap = Mathf.RoundToInt(kingdomLvl * 3.75f);
        archmageAmount = archmageCap >= archmageAmount ? archmageAmount : archmageCap;

        halbertCap = Mathf.RoundToInt(kingdomLvl * 7.15f);
        halbertAmount = halbertCap >= halbertAmount ? halbertAmount : halbertCap;

        mageCap = Mathf.RoundToInt(kingdomLvl * 3.5f);
        mageAmount = mageCap >= mageAmount ? mageAmount : mageCap;

        shieldmanCap = Mathf.RoundToInt(kingdomLvl * 2.95f);
        shieldmanAmount = shieldmanCap >= shieldmanAmount ? shieldmanAmount : shieldmanCap;

        crossbowCap = Mathf.RoundToInt(kingdomLvl * 6.25f);
        crossbowAmount = crossbowCap >= crossbowAmount ? crossbowAmount : crossbowCap;


        unitAmountCap = 10 + Mathf.RoundToInt(kingdomLvl * 1.15f);
    }

    public void KingdomMaxExpCalculate()
    {
        kingdomLvlMaxExp = 300 * (kingdomLvl * 2 + 1);
    }

    public void GainXp(int xpSource)
    {
        while (xpSource > 0)
        {
            if (kingdomLvl >= 20) break;
            int xpNeeded = kingdomLvlMaxExp - kingdomLvlExp;

            if (xpSource >= xpNeeded)
            {
                xpSource -= xpNeeded;
                kingdomLvlExp = 0;
                kingdomLvl++;
                KingdomMaxExpCalculate();
                CapCalculate();
            }
            else
            {
                kingdomLvlExp += xpSource;
                xpSource = 0;
            }
        }
    }

}

[System.Serializable]
public class ArmyInfor
{
    public string name;
    public int lvl;
    public int xp;
    public int maxXp;
    public int lvlCap;

    public ArmyInfor(string _name, int _lvl)
    {
        name = _name;
        lvl = _lvl;
        lvlCap = 20;
    }

    public void MaxXPCalculate()
    {
        maxXp = 200 * (lvl * 2 + 1);
    }


    public void GainXp(int xpSource)
    {
        while (xpSource > 0)
        {
            if (lvl >= 20) break;
            int xpNeeded = maxXp - xp;

            if (xpSource >= xpNeeded)
            {
                xpSource -= xpNeeded;
                xp = 0;
                lvl++;
                MaxXPCalculate();
            }
            else
            {
                xp += xpSource;
                xpSource = 0;
            }
        }
    }


}
[System.Serializable]
public class ControllerSave
{
    public Node node;
    public ArmyInfor _refArmy;
    public bool isRegistered;
    public ArmyData armyData;

    public ControllerSave(Node _node, ArmyInfor _armyInfor, bool _isRegister, ArmyData _armyData)
    {
        node = _node;
        _refArmy = _armyInfor;
        armyData = _armyData;
        isRegistered = _isRegister;
    }
}
[System.Serializable]
public class SaveLoadManager
{
    public int _volunteers = 0;
    public int _gold = 0;
    public List<ArmyInfor> _armies;

    public int _kingdomLvl;
    public int _kingdomLvlExp;
    public int _kingdomLvlCap;

    public int _knightAmount;

    public int _archerAmount;

    public int _halbertAmount;

    public int _shieldmanAmount;

    public int _mageAmount;

    public int _archmageAmount;

    public int _crossbowAmount;
}

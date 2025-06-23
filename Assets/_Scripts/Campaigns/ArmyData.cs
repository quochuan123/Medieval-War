using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ArmyData
{
    public string name;
    public int armyID;
    public bool isPlayer;             // chủ sở hữu
    public int currentNodeID;
    public int lvl;
    public bool isDead;
    public float exhaustedLvl;

    public ArmyData(string _name,int _armyID, bool _isPlayer, int _currenNodeID, int _lvl, bool _isDead)
    {
        name = _name;
        armyID = _armyID;
        isPlayer = _isPlayer;
        currentNodeID = _currenNodeID;
        isDead = _isDead;
        lvl = _lvl; 
    }

    public void ExhaustedCalculator(float exhaustIncrease)
    {
        exhaustedLvl = exhaustedLvl + (1 - exhaustedLvl)* exhaustIncrease;
    }

    public void ExhaustedDecrease(float exhaustIncrease)
    {
        exhaustedLvl = exhaustedLvl - (1 - exhaustedLvl) * exhaustIncrease;
    }
}

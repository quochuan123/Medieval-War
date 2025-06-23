using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitsStrategy : MonoBehaviour
{
    //Unit btn
    public GameObject knightScreen;
    public GameObject halberdScreen;
    public GameObject crossbowScreen;
    public GameObject archerScreen;
    public GameObject archmageScreen;
    public GameObject shieldmanScreen;
    public GameObject mageScreen;
    [Space(30)]
    //Knight
    public GameObject k_a_focusRange_on;
    public GameObject k_a_focusMelee_on;
    public GameObject k_a_none_on;
    //Shieldman
    [Space(30)]
    public GameObject sm_a_focusRange_on;
    public GameObject sm_a_focusMelee_on;
    public GameObject sm_a_none_on;
    [Space(10)]
    public GameObject endure_kn;
    public GameObject endure_cb;
    public GameObject endure_ar;
    public GameObject endure_am;
    public GameObject endure_mg;
    public GameObject endure_sm;
    public GameObject endure_h;
    public GameObject endure_none;
    //Archer
    [Space(30)]
    public GameObject ar_a_focusRange_on;
    public GameObject ar_a_focusMelee_on;
    public GameObject ar_a_none_on;
    //Archmage
    [Space(30)]
    public GameObject am_a_focusRange_on;
    public GameObject am_a_focusMelee_on;
    public GameObject am_a_none_on;
    [Space(10)]
    public GameObject heal_kn;
    public GameObject heal_cb;
    public GameObject heal_ar;
    public GameObject heal_am;
    public GameObject heal_mg;
    public GameObject heal_sm;
    public GameObject heal_h;
    public GameObject heal_none;
    [Space(10)]
    public GameObject fireSpark_kn;
    public GameObject fireSpark_cb;
    public GameObject fireSpark_ar;
    public GameObject fireSpark_am;
    public GameObject fireSpark_mg;
    public GameObject fireSpark_sm;
    public GameObject fireSpark_h;
    public GameObject fireSpark_none;



    //Halberd
    [Space(30)]
    public GameObject h_a_focusRange_on;
    public GameObject h_a_focusMelee_on;
    public GameObject h_a_none_on;
    //Crossbow
    [Space(30)]
    public GameObject cb_a_focusRange_on;
    public GameObject cb_a_focusMelee_on;
    public GameObject cb_a_none_on;
    //Mage
    [Space(30)]
    public GameObject mg_a_focusRange_on;
    public GameObject mg_a_focusMelee_on;
    public GameObject mg_a_none_on;
    [Space(10)]
    public GameObject enhance_kn;
    public GameObject enhance_cb;
    public GameObject enhance_ar;
    public GameObject enhance_am;
    public GameObject enhance_mg;
    public GameObject enhance_sm;
    public GameObject enhance_h;
    public GameObject enhance_none;
    [Space(10)]
    public GameObject gravity_kn;
    public GameObject gravity_cb;
    public GameObject gravity_ar;
    public GameObject gravity_am;
    public GameObject gravity_mg;
    public GameObject gravity_sm;
    public GameObject gravity_h;
    public GameObject gravity_none;


    private GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        SetUpStrategyScreen();

    }

    public void SetUpStrategyScreen()
    {
        ChanceScreenSetup(knightScreen);

        //Knight
        OnK_A_None_On();
        //Shieldman
        OnSm_A_None_On();
        EndureSetup(endure_none,"none");
        //Halberd
        OnH_A_None_On();
        //Archer
        OnAr_A_None_On();
        //Archmage
        OnAm_A_None_On();
        FireSparkSetup(fireSpark_none, "none");
        HealSetup(heal_none,"none");
        //Crossbow
        OnCb_A_None_On();
        //Mage
        OnMg_A_None_On();
        EnhanceSetup(enhance_none, "none");
        GravitySetup(gravity_none, "none");

    }
    public void HealSetup(GameObject target, string _class)
    {
        heal_kn.SetActive(true);
        heal_cb.SetActive(true);
        heal_ar.SetActive(true);
        heal_am.SetActive(true);
        heal_mg.SetActive(true);
        heal_sm.SetActive(true);
        heal_h.SetActive(true);
        heal_none.SetActive(true);

        target.SetActive(false);

        foreach(var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "archmage")
            {
                unitScript.healTarget = _class;
                unitScript.target = null;
            }
        }
    }
    public void FireSparkSetup(GameObject target, string _class)
    {
        fireSpark_kn.SetActive(true);
        fireSpark_cb.SetActive(true);
        fireSpark_ar.SetActive(true);
        fireSpark_am.SetActive(true);
        fireSpark_mg.SetActive(true);
        fireSpark_sm.SetActive(true);
        fireSpark_h.SetActive(true);
        fireSpark_none.SetActive(true);

        target.SetActive(false);
        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "archmage")
            {
                unitScript.fireSparkTarget = _class;
                unitScript.target = null;
            }
        }
    }

    public void EndureSetup(GameObject target,string _class)
    {
        endure_kn.SetActive(true);
        endure_cb.SetActive(true);
        endure_ar.SetActive(true);
        endure_am.SetActive(true);
        endure_mg.SetActive(true);
        endure_sm.SetActive(true);
        endure_h.SetActive(true);
        endure_none.SetActive(true);

        target.SetActive(false);
        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "shieldman")
            {
                unitScript.endureTarget = _class;
                unitScript.target = null;
            }
        }
    }

    public void GravitySetup(GameObject target, string _class)
    {
        gravity_kn.SetActive(true);
        gravity_cb.SetActive(true);
        gravity_ar.SetActive(true);
        gravity_am.SetActive(true);
        gravity_mg.SetActive(true);
        gravity_sm.SetActive(true);
        gravity_h.SetActive(true);
        gravity_none.SetActive(true);

        target.SetActive (false);
        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "mage")
            {
                unitScript.gravityTarget = _class;
                unitScript.target = null;
            }
        }
    }

    public void EnhanceSetup(GameObject target, string _class)
    {
        enhance_kn.SetActive(true);
        enhance_cb.SetActive(true);
        enhance_ar.SetActive(true);
        enhance_am.SetActive(true);
        enhance_mg.SetActive(true);
        enhance_sm.SetActive(true);
        enhance_h.SetActive(true);
        enhance_none.SetActive(true);

        target.SetActive(false);

        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "mage")
            {
                unitScript.enhanceTarget = _class;
                unitScript.target = null;
            }
        }
    }

    public void ChanceScreenSetup(GameObject target)
    {
        knightScreen.SetActive(false);
        halberdScreen.SetActive(false);
        crossbowScreen.SetActive(false);
        archerScreen.SetActive(false);
        archmageScreen.SetActive(false);
        shieldmanScreen.SetActive(false);
        mageScreen.SetActive(false);

        target.SetActive(true);
    }

    public void OnOpenScreenClick()
    {
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;
        if (clickObj != null)
        {
            switch (clickObj.tag)
            {
                case "knight":
                    ChanceScreenSetup(knightScreen);
                    break;
                case "archer":
                    ChanceScreenSetup(archerScreen);
                    break;
                case "crossbow":
                    ChanceScreenSetup(crossbowScreen);
                    break;
                case "archmage":
                    ChanceScreenSetup(archmageScreen);
                    break;
                case "halbert":
                    ChanceScreenSetup(halberdScreen);
                    break;
                case "mage":
                    ChanceScreenSetup(mageScreen);
                    break;
                case "shieldman":
                    ChanceScreenSetup(shieldmanScreen);
                    break;
                default:
                    break;

            }
        }

    }

    //Knight
    public void OnK_A_Focus_R_On()
    {
        k_a_focusMelee_on.SetActive(true);
        k_a_focusRange_on.SetActive(false);
        k_a_none_on.SetActive(true);
        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "knight")
            {
                unitScript.focusRange = true;
                unitScript.focusMelee = false;
                unitScript.target = null;
            }
        }
    }
    public void OnK_A_Focus_M_On()
    {
        k_a_focusMelee_on.SetActive(false);
        k_a_focusRange_on.SetActive(true);
        k_a_none_on.SetActive(true);
        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "knight")
            {
                unitScript.focusRange = false;
                unitScript.focusMelee = true;
                unitScript.target = null;
            }
        }
    }

    public void OnK_A_None_On()
    {
        k_a_focusMelee_on.SetActive(true);
        k_a_focusRange_on.SetActive(true);
        k_a_none_on.SetActive(false);
        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "knight")
            {
                unitScript.focusRange = false;
                unitScript.focusMelee = false;
                unitScript.target = null;
            }
        }
    }


    //Shieldman
    public void OnSm_A_Focus_R_On()
    {
        sm_a_focusMelee_on.SetActive(true);
        sm_a_focusRange_on.SetActive(false);
        sm_a_none_on.SetActive(true);

        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "shieldman")
            {
                unitScript.focusRange = true;
                unitScript.focusMelee = false;
                unitScript.target = null;
            }
        }
    }
    public void OnSm_A_Focus_M_On()
    {
        sm_a_focusMelee_on.SetActive(false);
        sm_a_focusRange_on.SetActive(true);
        sm_a_none_on.SetActive(true);
        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "shieldman")
            {
                unitScript.focusRange = false;
                unitScript.focusMelee = true;
                unitScript.target = null;
            }
        }
    }

    public void OnSm_A_None_On()
    {
        sm_a_focusMelee_on.SetActive(true);
        sm_a_focusRange_on.SetActive(true);
        sm_a_none_on.SetActive(false);
        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "shieldman")
            {
                unitScript.focusRange = false;
                unitScript.focusMelee = false;
                unitScript.target = null;
            }
        }
    }



    public void Endure_OnTick()
    {
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;
        if (clickObj != null)
        {
            switch (clickObj.tag)
            {
                case "knight":
                    EndureSetup(endure_kn,"knight");
                    break;
                case "halbert":
                    EndureSetup(endure_h, "halbert");
                    break;
                case "crossbow":
                    EndureSetup(endure_cb, "crossbow");
                    break;
                case "archer":
                    EndureSetup(endure_ar, "archer");
                    break;
                case "archmage":
                    EndureSetup(endure_am,"archmage");
                    break;
                case "shieldman":
                    EndureSetup(endure_sm, "shieldman");
                    break;
                case "mage":
                    EndureSetup(endure_mg, "mage");
                    break;
                case "none":
                    EndureSetup(endure_none, "none");
                    break;
                default:
                    break;
            }
        }



    }
    //Archmage
    public void OnAm_A_Focus_R_On()
    {
        am_a_focusMelee_on.SetActive(true);
        am_a_focusRange_on.SetActive(false);
        am_a_none_on.SetActive(true);
        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "archmage")
            {
                unitScript.focusRange = true;
                unitScript.focusMelee = false;
                unitScript.target = null;
            }
        }
    }
    public void OnAm_A_Focus_M_On()
    {
        am_a_focusMelee_on.SetActive(false);
        am_a_focusRange_on.SetActive(true);
        am_a_none_on.SetActive(true);
        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "archmage")
            {
                unitScript.focusRange = false;
                unitScript.focusMelee = true;
                unitScript.target = null;
            }
        }
    }

    public void OnAm_A_None_On()
    {
        am_a_focusMelee_on.SetActive(true);
        am_a_focusRange_on.SetActive(true);
        am_a_none_on.SetActive(false);
        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "archmage")
            {
                unitScript.focusRange = false;
                unitScript.focusMelee = false;
                unitScript.target = null;
            }
        }
    }



    public void Heal_OnTick()
    {
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;
        if (clickObj != null)
        {
            switch (clickObj.tag)
            {
                case "knight":
                    HealSetup(heal_kn, "knight");
                    break;
                case "halbert":
                    HealSetup(heal_h, "halbert");
                    break;
                case "crossbow":
                    HealSetup(heal_cb,"crossbow");
                    break;
                case "archer":
                    HealSetup(heal_ar,"archer");
                    break;
                case "archmage":
                    HealSetup(heal_am,"archmage");
                    break;
                case "shieldman":
                    HealSetup(heal_sm,"shieldman");
                    break;
                case "mage":
                    HealSetup(heal_mg,"mage");
                    break;
                case "none":
                    HealSetup(heal_none,"none");
                    break;
                default:
                    break;
            }
        }
    }

    public void FireSpark_OnTick()
    {
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;
        if (clickObj != null)
        {
            switch (clickObj.tag)
            {
                case "knight":
                    FireSparkSetup(fireSpark_kn,"knight");
                    break;
                case "halbert":
                    FireSparkSetup(fireSpark_h, "halbert");
                    break;
                case "crossbow":
                    FireSparkSetup(fireSpark_cb, "crossbow");
                    break;
                case "archer":
                    FireSparkSetup(fireSpark_ar, "archer");
                    break;
                case "archmage":
                    FireSparkSetup(fireSpark_am, "archmage");
                    break;
                case "shieldman":
                    FireSparkSetup(fireSpark_sm, "shieldman");
                    break;
                case "mage":
                    FireSparkSetup(fireSpark_mg, "mage");
                    break;
                case "none":
                    FireSparkSetup(fireSpark_none, "none");
                    break;
                default:
                    break;
            }
        }
    }


    //Mage
    public void OnMg_A_Focus_R_On()
    {
        mg_a_focusMelee_on.SetActive(true);
        mg_a_focusRange_on.SetActive(false);
        mg_a_none_on.SetActive(true);

        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "mage")
            {
                unitScript.focusRange = true;
                unitScript.focusMelee = false;
                unitScript.target = null;
            }
        }
    }

    public void OnMg_A_Focus_M_On()
    {
        mg_a_focusMelee_on.SetActive(false);
        mg_a_focusRange_on.SetActive(true);
        mg_a_none_on.SetActive(true);
        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "mage")
            {
                unitScript.focusRange = false;
                unitScript.focusMelee = true;
                unitScript.target = null;
            }
        }
    }

    public void OnMg_A_None_On()
    {
        mg_a_focusMelee_on.SetActive(true);
        mg_a_focusRange_on.SetActive(true);
        mg_a_none_on.SetActive(false);
        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "mage")
            {
                unitScript.focusRange = false;
                unitScript.focusMelee = false;
                unitScript.target = null;
            }
        }
    }



    public void Enhance_OnTick()
    {
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;
        if (clickObj != null)
        {
            switch (clickObj.tag)
            {
                case "knight":
                    EnhanceSetup(enhance_kn, "knight");
                    break;
                case "halbert":
                    EnhanceSetup(enhance_h, "halbert");
                    break;
                case "crossbow":
                    EnhanceSetup(enhance_cb, "crossbow");
                    break;
                case "archer":
                    EnhanceSetup(enhance_ar, "archer");
                    break;
                case "archmage":
                    EnhanceSetup(enhance_am, "archmage");
                    break;
                case "shieldman":
                    EnhanceSetup(enhance_sm, "shieldman");
                    break;
                case "mage":
                    EnhanceSetup(enhance_mg, "mage");
                    break;
                case "none":
                    EnhanceSetup(enhance_none, "none");
                    break;
                default:
                    break;
            }
        }
    }

    public void Gravity_OnTick()
    {
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;
        if (clickObj != null)
        {
            switch (clickObj.tag)
            {
                case "knight":
                    GravitySetup(gravity_kn, "knight");
                    break;
                case "halbert":
                    GravitySetup(gravity_h, "halbert");
                    break;
                case "crossbow":
                    GravitySetup(gravity_cb, "crossbow");
                    break;
                case "archer":
                    GravitySetup(gravity_ar, "archer");
                    break;
                case "archmage":
                    GravitySetup(gravity_am, "archmage");
                    break;
                case "shieldman":
                    GravitySetup(gravity_sm, "shieldman");
                    break;
                case "mage":
                    GravitySetup(gravity_mg, "mage");
                    break;
                case "none":
                    GravitySetup(gravity_none, "none");
                    break;
                default:
                    break;
            }
        }
    }

    //Archer
    public void OnAr_A_Focus_R_On()
    {
        ar_a_focusMelee_on.SetActive(true);
        ar_a_focusRange_on.SetActive(false);
        ar_a_none_on.SetActive(true);
        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "archer")
            {
                unitScript.focusRange = true;
                unitScript.focusMelee = false;
                unitScript.target = null;
            }
        }
    }
    public void OnAr_A_Focus_M_On()
    {
        ar_a_focusMelee_on.SetActive(false);
        ar_a_focusRange_on.SetActive(true);
        ar_a_none_on.SetActive(true);
        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "archer")
            {
                unitScript.focusRange = false;
                unitScript.focusMelee = true;
                unitScript.target = null;
            }
        }
    }

    public void OnAr_A_None_On()
    {
        ar_a_focusMelee_on.SetActive(true);
        ar_a_focusRange_on.SetActive(true);
        ar_a_none_on.SetActive(false);
        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "archer")
            {
                unitScript.focusRange = false;
                unitScript.focusMelee = false;
                unitScript.target = null;
            }
        }
    }

    //Crossbow
    public void OnCb_A_Focus_R_On()
    {
        cb_a_focusMelee_on.SetActive(true);
        cb_a_focusRange_on.SetActive(false);
        cb_a_none_on.SetActive(true);
        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "crossbow")
            {
                unitScript.focusRange = true;
                unitScript.focusMelee = false;
                unitScript.target = null;
            }
        }
    }
    public void OnCb_A_Focus_M_On()
    {
        cb_a_focusMelee_on.SetActive(false);
        cb_a_focusRange_on.SetActive(true);
        cb_a_none_on.SetActive(true);
        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "crossbow")
            {
                unitScript.focusRange = false;
                unitScript.focusMelee = true;
                unitScript.target = null;
            }
        }
    }

    public void OnCb_A_None_On()
    {
        cb_a_focusMelee_on.SetActive(true);
        cb_a_focusRange_on.SetActive(true);
        cb_a_none_on.SetActive(false);
        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "crossbow")
            {
                unitScript.focusRange = false;
                unitScript.focusMelee = false;
                unitScript.target = null;
            }
        }
    }

    //Halberd
    public void OnH_A_Focus_R_On()
    {
        h_a_focusMelee_on.SetActive(true);
        h_a_focusRange_on.SetActive(false);
        h_a_none_on.SetActive(true);
        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "halbert")
            {
                unitScript.focusRange = true;
                unitScript.focusMelee = false;
                unitScript.target = null;
            }
        }
    }
    public void OnH_A_Focus_M_On()
    {
        h_a_focusMelee_on.SetActive(false);
        h_a_focusRange_on.SetActive(true);
        h_a_none_on.SetActive(true);
        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "halbert")
            {
                unitScript.focusRange = false;
                unitScript.focusMelee = true;
                unitScript.target = null;
            }
        }
    }

    public void OnH_A_None_On()
    {
        h_a_focusMelee_on.SetActive(true);
        h_a_focusRange_on.SetActive(true);
        h_a_none_on.SetActive(false);
        foreach (var unit in manager.playerTeam)
        {
            Unit unitScript = unit.GetComponent<Unit>();
            if (unitScript.unitClass == "halbert")
            {
                unitScript.focusRange = false;
                unitScript.focusMelee = false;
                unitScript.target = null;
            }
        }
    }
}

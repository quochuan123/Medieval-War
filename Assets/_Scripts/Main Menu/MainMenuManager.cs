using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using JetBrains.Annotations;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public List<GameObject> knightForces;
    public Text warringText;
    public GameObject warringObj;
    public GameObject knightPrefab;
    public GameObject knightLineup;
   public GameObject knightLinedown;
    private SingletonScript sts;
    // Start is called before the first frame update
    void Start()
    {
        sts = FindObjectOfType<SingletonScript>();
        foreach(var unit in knightForces)
        {
            GameObject newKnight = Instantiate(knightPrefab, unit.transform.position, Quaternion.identity);
            newKnight.transform.SetParent(knightLineup.transform);
        }

        SoundManager sm = FindObjectOfType<SoundManager>();
        if(sm != null)
        {
            sm.MainMenuMusic();
        }
    }

    public void NewGame()
    {
        sts.volunteers = 10;
        sts.gold = 5000;
        List<ArmyInfor> armyList = new List<ArmyInfor>();
        armyList.Add(new ArmyInfor("Army 1", 1));
        sts.armies = armyList;
        sts.kingdomLvl = 1;
        sts.kingdomLvlExp = 0;
        sts.kingdomLvlCap = 20;
        sts.knightAmount = 10;
        sts.archerAmount = 10;
        sts.halbertAmount = 10;
        sts.shieldmanAmount = 10;
        sts.mageAmount = 10;
        sts.archmageAmount = 10;
        sts.crossbowAmount = 10;

        SceneManager.LoadScene("Village Map");
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/player.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveLoadManager data = JsonUtility.FromJson<SaveLoadManager>(json);

            // Gán dữ liệu từ file vào các biến hiện tại
            sts.volunteers = data._volunteers;
            sts.gold = data._gold;
            sts.armies = data._armies;
            sts.kingdomLvl = data._kingdomLvl;
            sts.kingdomLvlExp = data._kingdomLvlExp;
            sts.kingdomLvlCap = data._kingdomLvlCap;
            sts.knightAmount = data._knightAmount;
            sts.archerAmount = data._archerAmount;
            sts.halbertAmount = data._halbertAmount;
            sts.shieldmanAmount = data._shieldmanAmount;
            sts.mageAmount = data._mageAmount;
            sts.archmageAmount = data._archmageAmount;
            sts.crossbowAmount = data._crossbowAmount;
            Debug.Log("Game loaded from JSON!");
            SceneManager.LoadScene("Village Map");

        }
        else
        {
            StartCoroutine(ShowMessage());
        }
    }

    IEnumerator ShowMessage()
    {
        warringText.text = "You don't have any save files!";
        warringObj.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        warringObj.SetActive(false);

    }

    public void CombatEditor()
    {
        SceneManager.LoadScene("Test Scene");
    }

    public void Setting()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
    


}

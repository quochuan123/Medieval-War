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

    //Setting 
    public GameObject settingInterface;
    public Image volumeBar;
    public GameObject fullScreenOnBtn;
    public GameObject fullScreenOffBtn;
    public float volumeValue;

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
            volumeValue = sm.volumeValue;
            sm.ChangeSoundVolume(sm.volumeValue);

        }

        volumeBar.fillAmount = volumeValue;


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

        sts.isCompleteMistMountain = false;
        sts.isCompleteDawnValley = false;
        sts.isCompleteDeadKingdom = false;
        sts.isCompleteSkyfall = false;

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
            sts.isCompleteMistMountain = data._isCompleteMistMountain;
            sts.isCompleteDawnValley = data._isCompleteDawnValley;
            sts.isCompleteDeadKingdom = data._isCompleteDeadKingdom;
            sts.isCompleteSkyfall = data._isCompleteSkyfall;
            SoundManager sm = FindObjectOfType<SoundManager>();
            if (sm != null)
            {
                sm.volumeValue = data.volume;
                sm.ChangeSoundVolume(sm.volumeValue);
            }

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

    public void OpenSetting()
    {
        settingInterface.SetActive(true);
    }

    public void CloseSetting()
    {
        settingInterface.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void VolumeUp()
    {
        volumeValue += 0.2f;
        if(volumeValue > 1)
            volumeValue = 1;
        volumeBar.fillAmount = volumeValue;
        SoundManager sm = FindObjectOfType<SoundManager>();
        if(sm != null)
        {
            sm.ChangeSoundVolume(volumeValue);
        }
    }

    public void VolumeDown()
    {
        volumeValue -= 0.2f;
        if (volumeValue < 0)
            volumeValue = 0;
        volumeBar.fillAmount = volumeValue;
        SoundManager sm = FindObjectOfType<SoundManager>();
        if (sm != null)
        {
            sm.ChangeSoundVolume(volumeValue);
        }
    }

    public void FullScreenOn()
    {
        Screen.fullScreen = true;
        fullScreenOffBtn.SetActive(true);
        fullScreenOnBtn.SetActive(false);
    }

    public void FullScreenOff()
    {
        Screen.fullScreen = false;
        fullScreenOffBtn.SetActive(false);
        fullScreenOnBtn.SetActive(true);
    }



}

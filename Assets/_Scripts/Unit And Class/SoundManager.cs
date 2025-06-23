using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource backgroundMusic;
    public AudioSource buttonSound;
    public AudioSource interfaceMusic;

    [Space(30)]

    public AudioClip mainMenuClip;
    public AudioClip villagerClip;
    public AudioClip trainingInterfaceClip;
    public AudioClip campaignInterfaceClip;
    public AudioClip campaignMusic;
    public AudioClip strategyMusic;
    public AudioClip battleMusic;
    public AudioClip postbattleMusic;

    public void Awake()
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

    public void MainMenuMusic()
    {
        interfaceMusic.Stop();
        backgroundMusic.clip = mainMenuClip;
        backgroundMusic.Play();
    }

    public void VillagerMusic()
    {
        interfaceMusic.Stop();
        backgroundMusic.clip = villagerClip;
        backgroundMusic.Play();
    }

    public void TrainingInterfaceMusicOn()
    {
        backgroundMusic.Pause();
        interfaceMusic.clip = trainingInterfaceClip;
        interfaceMusic.Play();
    }

    public void InterfaceMusicOff()
    {
        backgroundMusic.UnPause();
        interfaceMusic.Stop();
    }

    public void CampaignInterfaceMusicOn()
    {
        backgroundMusic.Pause();
        interfaceMusic.clip = campaignInterfaceClip;
        interfaceMusic.Play();
    }

    public void CampaignMusic()
    {
        interfaceMusic.Stop();
            backgroundMusic.clip = campaignMusic;
        backgroundMusic.Play();
    }

    public void StrategyInterfaceMusic()
    {
        backgroundMusic.Stop();
        interfaceMusic.clip = strategyMusic;
        interfaceMusic.Play();
    }

    public void BattleMusic()
    {
        interfaceMusic.Stop();
        backgroundMusic.clip = battleMusic;
        backgroundMusic.Play();
    }

    public void PostBattleMusic()
    {
        backgroundMusic.Pause();
        interfaceMusic.clip = postbattleMusic;
        interfaceMusic.Play();
    }



}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Setting_Manager : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] GameObject SettingPanel;

    [SerializeField] Toggle MusicCheckBox;
    [SerializeField] Toggle SoundCheckBox;

    [SerializeField] AudioMixer MusicAudioMixer;
    [SerializeField] AudioMixer SoundAudioMixer;

    [SerializeField] AudioSource AudioSource;
  
    public static Setting_Manager Instance;

    private void Awake()
    {
        Instance = this; 
    }

    private void Start()
    {
        MusicCheckBox.isOn = References.MusicCheck;
        SoundCheckBox.isOn = References.SoundCheck;
    }

    public void ToggleMusic()
    {
        if (MusicCheckBox.isOn)
        {
            MusicAudioMixer.SetFloat("Volume", 0f);
            References.MusicCheck = true;
        }
        else
        {
            MusicAudioMixer.SetFloat("Volume", -80f);
            References.MusicCheck = false;

        }

        PlaySound();
    }
    public void ToggleSound()
    {
        if (SoundCheckBox.isOn)
        {
            SoundAudioMixer.SetFloat("Volume", 0f);
            References.SoundCheck = true;
        }
        else
        {
            SoundAudioMixer.SetFloat("Volume", -80f);
            References.SoundCheck = false;


        }

        PlaySound();
    }

    public void Toggle_Setting(bool value)
    {
        SettingPanel.SetActive(value);
        PlaySound();
    }

    public void Game_Pause()
    {
        SettingPanel.SetActive(true);
        PlaySound();
        Time.timeScale = 0f;
    }

    public void Game_Continue()
    {
        SettingPanel.SetActive(false);
        PlaySound();
        Time.timeScale = 1f;
    }

    public void Game_Play()
    {
        Time.timeScale = 1f;
        PlaySound();
        SceneManager.LoadScene(Scenes.Game.ToString());
    }

    public void Game_GoToHome()
    {
        PlaySound();
        SceneManager.LoadScene(Scenes.Home.ToString());
    }

    private void OnApplicationQuit()
    {
        References.SaveAccountData(References.account);
    }

    public void PlaySound()
    {
        AudioSource.Play();
    }
}

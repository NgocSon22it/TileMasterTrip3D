using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    [Header("Win/Lose")]
    [SerializeField] GameObject LosePanel;

    public static Setting_Manager Instance;

    private void Awake()
    {
        Instance = this;
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
    }

    public void Toggle_Setting(bool value)
    {
        SettingPanel.SetActive(value);
    }

    public void Game_Pause()
    {
        SettingPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Game_Continue()
    {
        SettingPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Game_GoToHome()
    {

    }
}

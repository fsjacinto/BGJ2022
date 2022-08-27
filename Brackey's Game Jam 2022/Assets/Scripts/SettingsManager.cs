using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;


    public void SetBGMVolume(float volumeValue)
    {
        AudioManager.instance.bgmGroup.audioMixer.SetFloat("bgmVolume", Mathf.Log10(volumeValue) * 20);
    }

    public void SetSFXVolume(float volumeValue)
    {
        AudioManager.instance.sfxGroup.audioMixer.SetFloat("sfxVolume", Mathf.Log10(volumeValue) * 20);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}

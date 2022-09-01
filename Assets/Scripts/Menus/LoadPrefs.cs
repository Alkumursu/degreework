using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadPrefs : MonoBehaviour
{
    [Header ("General Settings")]
    [SerializeField] private bool canUse = false;
    [SerializeField] private MainMenuScript menuController;

    [Header ("Volume Settings")]
    [SerializeField] private Slider volumeSlider = null;

    [Header ("Quality Level Settings")]
    [SerializeField] private TMP_Dropdown qualityDropDown;

    [Header ("Fullscreen Settings")]
    [SerializeField] private Toggle fullScreenToggle;

    private void Awake()
    {
        if (canUse)
        {
            if (PlayerPrefs.HasKey("masterVolume"))
            {
                float localVolume = PlayerPrefs.GetFloat("masterVolume");
                volumeSlider.value = localVolume;
                AudioListener.volume = localVolume;
            }
            else
            {
                //menuController.ResetButton("Audio");
            }
            if (PlayerPrefs.HasKey("masterQuality"))
            {
                int localQuality = PlayerPrefs.GetInt("masterQuality");
                qualityDropDown.value = localQuality;
                QualitySettings.SetQualityLevel(localQuality);
            }
            if (PlayerPrefs.HasKey("masterFullScreen"))
            {
                int localFullScreen = PlayerPrefs.GetInt("masterFullScreen");

                if(localFullScreen == 1)
                {
                    Screen.fullScreen = true;
                    fullScreenToggle.isOn = true;
                }
                else
                {
                    Screen.fullScreen = false;
                    fullScreenToggle.isOn = false;
                }
            }
        }
    }
}

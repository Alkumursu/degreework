using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    [Header("Volume settings")]
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private GameObject confirmBox;
    [SerializeField] private float defaultVolume = 1.0f;

    [Header("Levels to Load")]
    public string _newGameLevel;
    private string levelToLoad;
    [SerializeField]
    private GameObject noSavedGame = null;

    [Header("Graphic settings")]
    [SerializeField] private TMP_Dropdown qualityDropDown;
    [SerializeField] private Toggle fullScreenToggle;
    private int _qualityLevel;
    private bool _isFullScreen;

    [Header("Resolution")]
    public TMP_Dropdown resolutionDropDown;
    private Resolution[] resolutions;

    private void Start()
    {
        Application.targetFrameRate = 60;

        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionINdex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i]. width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionINdex = i;
            }
        }
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionINdex;
        resolutionDropDown.RefreshShownValue();
    }

    public void SetResolution(int resolutionINdex)
    {
        Resolution resolution = resolutions[resolutionINdex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    public void LoadGame()
    {
        //PlayerPrefs sis?lt?? kaikki tiedot mit? savetetussa kent?ss? on tehty!!!
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            noSavedGame.SetActive(true);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    
    
    //--- OPTIONS MENU ---//

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmBox());
    }

    public void SetFullScreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;
    }

    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }

    public void GraphicApply()
    {
        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);

        PlayerPrefs.SetInt("masterFullScreen", (_isFullScreen ? 1 : 0));
        Screen.fullScreen = _isFullScreen;

        StartCoroutine(ConfirmBox());
    }
    public void ResetButton(string MenuType)
    {
        if(MenuType == "Graphics")
        {
            qualityDropDown.value = 1;
            QualitySettings.SetQualityLevel(1);

            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropDown.value = resolutions.Length;
            GraphicApply();
        }
        if(MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            VolumeApply();
        }
    }

    public IEnumerator ConfirmBox()
    {
        confirmBox.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmBox.SetActive(false);
    }
}

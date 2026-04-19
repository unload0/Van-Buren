using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    [Header("UI Components")]
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("Audio Control")]
    public AudioMixer mainMixer;

    private Resolution[] allResolutions;
    private List<Resolution> filteredResolutions;

    void Start()
    {
        InitializeResolutions();

        InitializeQuality();

        // InitializeAudioOptions();
    }

    void InitializeResolutions()
    {
        allResolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResIndex = 0;

        for (int i = 0; i < allResolutions.Length; i++)
        {
            string option = allResolutions[i].width + " x " + allResolutions[i].height;

            if (!options.Contains(option))
            {
                options.Add(option);
                filteredResolutions.Add(allResolutions[i]);

                if (allResolutions[i].width == Screen.currentResolution.width &&
                    allResolutions[i].height == Screen.currentResolution.height)
                {
                    currentResIndex = options.Count - 1;
                }
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(delegate
        {
            SetResolution(resolutionDropdown.value);
        });

        qualityDropdown.onValueChanged.AddListener(delegate
        {
            SetQuality(qualityDropdown.value);
        });

        masterSlider.onValueChanged.AddListener(delegate
        {
            SetMasterVolume(masterSlider.value);
        });

        musicSlider.onValueChanged.AddListener(delegate
        {
            SetMusicVolume(musicSlider.value);
        });

        sfxSlider.onValueChanged.AddListener(delegate
        {
            SetSFXVolume(sfxSlider.value);
        });
    }

    void InitializeQuality()
    {
        qualityDropdown.ClearOptions();
        List<string> qualityNames = new List<string>(QualitySettings.names);
        qualityDropdown.AddOptions(qualityNames);
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue();
    }

    void InitializeAudioOptions()
    {
        float dbValue;
        bool result = mainMixer.GetFloat("MasterVol", out dbValue);

        if (result)
        {
            float linearVolume = Mathf.Pow(10, dbValue / 20);

            masterSlider.value = linearVolume;
        }
    }

    public void SetResolution(int index)
    {
        Resolution res = filteredResolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetMasterVolume(float volume)
    {
        mainMixer.SetFloat("MasterVol", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        mainMixer.SetFloat("MusicVol", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        mainMixer.SetFloat("SFXVol", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
    }

    public void ApplySettings()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Exited");
    }
}
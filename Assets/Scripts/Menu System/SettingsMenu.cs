using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MenuAsset
{
    /*
     * Code by Kristopher Kath
     */


    public class SettingsMenu : MonoBehaviour
    {
        [Tooltip("The first button selected to hover when navigating to Main Menu.")]
        [SerializeField] private GameObject menuFirstButton;

        [Header("Audio")]
        [Tooltip("The Audio Mixer to modify volume from.")]
        [SerializeField] private AudioMixer audioMixer;

        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider bgmSlider;
        [SerializeField] private Slider sfxSlider;
        


        [Header("Dropdowns | Choose only one.")]
        [Tooltip("Resolution Dropdown from TextMeshPro.")]
        [SerializeField] private TMPro.TMP_Dropdown TMProResolutionDropdown;
        [Tooltip("Default Resolution Dropdown.")]
        [SerializeField] private Dropdown defaultResolutionDropdown;

        private Resolution[] resolutions;
        private int currentResolutionIndex;

        private void Start()
        {
            SetupResolutionSettings();
            SetupAudio();
        }

        private void SetupResolutionSettings()
        {
            resolutions = Screen.resolutions;

            //Handles resolutions for default dropdown
            if (defaultResolutionDropdown != null)
            {
                defaultResolutionDropdown.ClearOptions();

                defaultResolutionDropdown.AddOptions(ResolutionsFunction());
                defaultResolutionDropdown.value = currentResolutionIndex;
                defaultResolutionDropdown.RefreshShownValue();
            }
            //Handles resolutions for TextMeshPro dropdown
            else if (TMProResolutionDropdown != null)
            {
                TMProResolutionDropdown.ClearOptions();

                TMProResolutionDropdown.AddOptions(ResolutionsFunction());
                TMProResolutionDropdown.value = currentResolutionIndex;
                TMProResolutionDropdown.RefreshShownValue();
            }
        }

        //Gets list of resolutions as a string and returns
        //Also sets default resolution
        private List<string> ResolutionsFunction()
        {
            //fill options list with strings of resolutions
            List<string> options = new List<string>();
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);

                //finds current running resolution to set as default
                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
            return options;
        }


        public void SetResolution(int resolutionIndex)
        {
            Resolution res = resolutions[resolutionIndex];
            Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        }

        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }

        public void ExitOptions()
        {
            EventSystem.current.SetSelectedGameObject(null);

            if (menuFirstButton != null)
                EventSystem.current.SetSelectedGameObject(menuFirstButton);
        }

#region Audio

        private void SetupAudio()
        {
            float masterVol = PlayerPrefs.GetFloat("masterVolume", 0);
            float bgmVol = PlayerPrefs.GetFloat("bgmVolume", 0);
            float sfxVol = PlayerPrefs.GetFloat("sfxVolume", 0);

            SetMasterSlider(masterVol);
            SetMasterVolume(masterVol);
            SetBGMSlider(bgmVol);
            SetBGMVolume(bgmVol);
            SetSFXSlider(sfxVol);
            SetSFXVolume(sfxVol);
        }

        public void SetMasterVolume(float volume)
        {
            audioMixer.SetFloat("masterVolume", volume);
            PlayerPrefs.SetFloat("masterVolume", volume);
        }

        public void SetBGMVolume(float volume)
        {
            audioMixer.SetFloat("bgmVolume", volume);
            PlayerPrefs.SetFloat("bgmVolume", volume);
        }

        public void SetSFXVolume(float volume)
        {
            audioMixer.SetFloat("sfxVolume", volume);
            PlayerPrefs.SetFloat("sfxVolume", volume);
        }

        public void SetMasterSlider(float value)
        {
            masterSlider.value = value;
        }

        public void SetBGMSlider(float value)
        {
            bgmSlider.value = value;
        }

        public void SetSFXSlider(float value)
        {
            sfxSlider.value = value;
        }

#endregion //Audio


    }
}
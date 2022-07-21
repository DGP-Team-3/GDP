using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMenu : MonoBehaviour
{

    [SerializeField] private Animator panelAnimator;

    [SerializeField] private RehomeManager rehomeManager;

    [Tooltip("All display windows that can be opened.")]
    [SerializeField] private List<GameObject> displayWindows;

    [SerializeField] private Button homeButton;
    [SerializeField] private Button discoverButton;
    [SerializeField] private Button storageButton;
    [SerializeField] private Button rehomeButton;
    [SerializeField] private Button settingsButton;


    //////////////////////////////////////////
    ///
    ///
    public void ToggleSideDisplay()
    {
        bool isOpen = panelAnimator.GetBool("show");
        panelAnimator.SetBool("show", !isOpen);
    }


    //////////////////////////////////////////
    ///
    ///
    public void OnHomeButtonClicked()
    {
        homeButton.interactable = false;
        discoverButton.interactable = true;
        storageButton.interactable = true;
        rehomeButton.interactable = true;
        settingsButton.interactable = true;
        ToggleSideDisplay();

    }


    //////////////////////////////////////////
    ///
    ///
    public void OnDiscoverButtonClicked()
    {
        homeButton.interactable = true;
        discoverButton.interactable = false;
        storageButton.interactable = true;
        rehomeButton.interactable = true;
        settingsButton.interactable = true;
        ToggleSideDisplay();
    }


    //////////////////////////////////////////
    ///
    ///
    public void OnStorageButtonClicked()
    {
        homeButton.interactable = true;
        discoverButton.interactable = true;
        storageButton.interactable = false;
        rehomeButton.interactable = true;
        settingsButton.interactable = true;
        ToggleSideDisplay();
    }


    //////////////////////////////////////////
    ///
    ///
    public void OnRehomeButtonClicked()
    {
        homeButton.interactable = true;
        discoverButton.interactable = true;
        storageButton.interactable = true;
        rehomeButton.interactable = false;
        settingsButton.interactable = true;

        rehomeManager.SetCatDisplay(null);

        ToggleSideDisplay();
    }


    //////////////////////////////////////////
    ///
    ///
    public void OnSettingsButtonClicked()
    {
        homeButton.interactable = true;
        discoverButton.interactable = true;
        storageButton.interactable = true;
        rehomeButton.interactable = true;
        settingsButton.interactable = false;
        ToggleSideDisplay();
    }

    //////////////////////////////////////////
    ///
    ///
    public void HideSelf()
    {
        gameObject.SetActive(false);
    }
}

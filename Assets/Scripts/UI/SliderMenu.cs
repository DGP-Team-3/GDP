using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMenu : MonoBehaviour
{

    [SerializeField] private Animator panelAnimator;

    [SerializeField] private RehomeManager rehomeManager;
    [SerializeField] private IDHandler idHandler;

    [Tooltip("All display windows that can be opened.")]
    [SerializeField] private List<GameObject> displayWindows;

    [SerializeField] private Button homeButton;
    [SerializeField] private Button discoverButton;
    [SerializeField] private Button storageButton;
    [SerializeField] private Button idButton;
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
        idButton.interactable = true;
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
        idButton.interactable = true;
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
        idButton.interactable = true;
        settingsButton.interactable = true;
        ToggleSideDisplay();
    }


    //////////////////////////////////////////
    ///
    ///
    public void OnIDButtonClicked()
    {
        homeButton.interactable = true;
        discoverButton.interactable = true;
        storageButton.interactable = true;
        idButton.interactable = false;
        settingsButton.interactable = true;

        idHandler.UpdateIDData();

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
        idButton.interactable = true;
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

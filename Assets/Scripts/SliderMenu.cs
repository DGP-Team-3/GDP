using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMenu : MonoBehaviour
{

    [Tooltip("The main canvas for UI elements.")]
    [SerializeField] private GameObject displayCanvas;
    [SerializeField] private Animator panelAnimator;

    [Space]

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
        CloseOpenWindows();
        displayCanvas.SetActive(false);
        homeButton.interactable = false;
        discoverButton.interactable = true;
        storageButton.interactable = true;
        rehomeButton.interactable = true;
        settingsButton.interactable = true;

    }


    //////////////////////////////////////////
    ///
    ///
    public void OnDiscoverButtonClicked()
    {
        CloseOpenWindows();
        displayCanvas.SetActive(true);
        homeButton.interactable = true;
        discoverButton.interactable = false;
        storageButton.interactable = true;
        rehomeButton.interactable = true;
        settingsButton.interactable = true;
    }


    //////////////////////////////////////////
    ///
    ///
    public void OnStorageButtonClicked()
    {
        CloseOpenWindows();
        //displayCanvas.SetActive(true);
        homeButton.interactable = true;
        discoverButton.interactable = true;
        storageButton.interactable = false;
        rehomeButton.interactable = true;
        settingsButton.interactable = true;
    }


    //////////////////////////////////////////
    ///
    ///
    public void OnRehomeButtonClicked()
    {
        CloseOpenWindows();
        //displayCanvas.SetActive(true);
        homeButton.interactable = true;
        discoverButton.interactable = true;
        storageButton.interactable = true;
        rehomeButton.interactable = false;
        settingsButton.interactable = true;
    }


    //////////////////////////////////////////
    ///
    ///
    public void OnSettingsButtonClicked()
    {
        CloseOpenWindows();
        //displayCanvas.SetActive(true);
        homeButton.interactable = true;
        discoverButton.interactable = true;
        storageButton.interactable = true;
        rehomeButton.interactable = true;
        settingsButton.interactable = false;
    }


    //////////////////////////////////////////
    ///
    ///
    private void CloseOpenWindows()
    {
        foreach (GameObject window in displayWindows)
        {
            window.SetActive(false);
        }
    }
}

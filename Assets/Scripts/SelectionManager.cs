using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    private Cat cat;
    public Image fullnessBar;
    public GameObject CatPopUpDisplay;
    public Button homeButton;
    public Button storageButton;

    void Update()
    {
        if (cat)
        {
            float fullnessPercent = cat.fullness / 100f;
            fullnessBar.fillAmount = Mathf.Clamp(fullnessPercent, 0, 1f);
        }
    }

    public void setCat(Cat newCat)
    {
        cat = newCat;
    }

    public void toggleSelection()
    {
        CatPopUpDisplay.SetActive(!CatPopUpDisplay.activeSelf);
        handleButtonActivity();
    }

    public void setDisplay(bool displayPopup)
    {
        CatPopUpDisplay.SetActive(displayPopup);
        handleButtonActivity();
    }

    private void handleButtonActivity()
    {
        if (CatPopUpDisplay.activeSelf)
        {
            homeButton.interactable = true;
            storageButton.interactable = false;
        } else
        {
            homeButton.interactable = false;
            storageButton.interactable = true;
        }
    }

}

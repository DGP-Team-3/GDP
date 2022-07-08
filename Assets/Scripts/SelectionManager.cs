using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public Cat cat;
    public Image fullnessBar;
    public GameObject CatPopUpDisplay;

    void Update()
    {
        float fullnessPercent = cat.fullness / 100f;
        fullnessBar.fillAmount = Mathf.Clamp(fullnessPercent, 0, 1f);
    }

    public void toggleSelection()
    {
        CatPopUpDisplay.SetActive(!CatPopUpDisplay.activeSelf);
    }

}

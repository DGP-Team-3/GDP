using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RehomeHistoryContainer : MonoBehaviour
{

    [SerializeField] private TMP_Text catNameField;
    [SerializeField] private TMP_Text ownerNameField;
    [SerializeField] private Image catImage;
    [SerializeField] private Image ownerImage;

    public void SetupDisplay(string catName, string ownerName)
    {
        catNameField.text = catName;
        ownerNameField.text = ownerName;
    }
    public Image GetCatPortraitField()
    {
        return catImage;
    }
    public Image GetOwnerPortraitField()
    {
        return ownerImage;
    }

    public string GetCatName()
    {
        return catNameField.text;
    }

    public string GetOwnerName()
    {
        return ownerNameField.text;
    }
}

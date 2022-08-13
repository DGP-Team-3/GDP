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
    private CatType catType;
    private int ownerIndex;

    public void SetupDisplay(string catName, CatType catType, string ownerName, int ownerIndex)
    {
        catNameField.text = catName;
        ownerNameField.text = ownerName;

        // These two are stored for saving purposes
        this.catType = catType;
        this.ownerIndex = ownerIndex;
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
    public CatType GetCatType()
    {
        return catType;
    }

    public string GetOwnerName()
    {
        return ownerNameField.text;
    }

    public int GetOwnerIndex()
    {
        return ownerIndex;
    }
}

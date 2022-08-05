using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class OwnerContainer : MonoBehaviour
{
    [SerializeField] private TMP_Text textField;
    [SerializeField] private Image ownerPortrait;
    [SerializeField] private Button rehomeButton;

    //////////////////////////////////////////
    ///
    ///
    private void Start()
    {
        GameManager.Instance.AddToClearList(gameObject);
    }

    //////////////////////////////////////////
    ///
    ///
    public Image GetOwnerPortrait()
    {
        return ownerPortrait;
    }


    //////////////////////////////////////////
    ///
    ///
    public Button GetRehomeButton()
    {
        return rehomeButton;
    }


    //////////////////////////////////////////
    ///
    ///
    public TMP_Text GetTextField()
    {
        return textField;
    }
}

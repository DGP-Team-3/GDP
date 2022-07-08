using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlaceholderDisplay : MonoBehaviour
{
    [SerializeField]
    private Text title;
    [SerializeField]
    private Text description;

    public void displayPlaceholder(Button disabledButton, Button activeButton, string title, string description)
    {
        this.title.text = title;
        this.description.text = description;

        disabledButton.interactable = false;
        activeButton.interactable = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CatPopUpHandler : MonoBehaviour
{
    [SerializeField] private CatData catData;
    [SerializeField] private Image catFullnessBar;
    [SerializeField] private Image catEntertainmentBar;
    [SerializeField] private Image catRelationshipImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Button storeCatButton;
    [SerializeField] private Button returnButton;

    [Space]
    [Space]

    [Header("Dialogue Boxes")]
    [SerializeField] private GameObject progressRelationshipDialogueBox;
    [SerializeField] private TMP_Text progressRelationshipText;
    [SerializeField] private GameObject maxRelationshipDialogueBox;
    [SerializeField] private TMP_Text maxRelationshipText;

    [Space]

    [SerializeField] private GameObject progressFeedDialogueBox;
    [SerializeField] private TMP_Text progressFeedText;
    [SerializeField] private GameObject maxFeedDialogueBox;
    [SerializeField] private TMP_Text maxFeedText;

    [Space]

    [SerializeField] private GameObject entertainmentDialogueBox;    
    [SerializeField] private TMP_Text entertainmentText;


    private Cat currentCat;
    private List<GameObject> dialogueBoxes = new List<GameObject>();



    //////////////////////////////////////////
    ///
    ///
    private void Awake()
    {
        dialogueBoxes.Add(progressRelationshipDialogueBox);
        dialogueBoxes.Add(maxRelationshipDialogueBox);
        dialogueBoxes.Add(progressFeedDialogueBox);
        dialogueBoxes.Add(maxFeedDialogueBox);
        dialogueBoxes.Add(entertainmentDialogueBox);
    }


    //////////////////////////////////////////
    /// 
    ///
    void OnDisable()
    {
        Unsubscribe();
    }

    //////////////////////////////////////////
    /// Give new cat to update images and values
    ///
    public void AssignCat(Cat newCat)
    {
        if (currentCat == newCat) return;

        Unsubscribe();

        currentCat = newCat;
        
        SetDisplay();
        
        Subscribe();

    }


    //////////////////////////////////////////
    /// Subscribe to given cat 
    ///
    private void Subscribe()
    {
        if (currentCat == null) return;

        currentCat.OnHungerUpdated += UpdateHungerUI;
        currentCat.OnEntertainmentUpdated += UpdateEntertainmentUI;
        currentCat.OnRelationshipUpdated += UpdateRelationshipUI;
    }

    //////////////////////////////////////////
    /// Unsubscribe from current cat 
    ///
    private void Unsubscribe()
    {
        if (currentCat == null) return;

        currentCat.OnHungerUpdated -= UpdateHungerUI;
        currentCat.OnEntertainmentUpdated -= UpdateEntertainmentUI;
        currentCat.OnRelationshipUpdated -= UpdateRelationshipUI;
    }


    //////////////////////////////////////////
    /// Updates UI values
    ///
    private void SetDisplay()
    {
        if (currentCat == null) return;

        nameText.text = currentCat.GetName();

        UpdateHungerUI(currentCat.Fullness);

        UpdateEntertainmentUI(currentCat.Entertainment);

        UpdateRelationshipUI(currentCat.Relationship);
    }

    //////////////////////////////////////////
    ///
    ///
    private void UpdateHungerUI(int fullness)
    {
        if (currentCat == null) return;


        float fullnessPercent = fullness / currentCat.MaxFullness;
        catFullnessBar.fillAmount = Mathf.Clamp(fullnessPercent, 0, 1f);

        progressFeedText.text = fullness + "/" + currentCat.MaxFullness;
        maxFeedText.text = currentCat.MaxFullness + "/" + currentCat.MaxFullness;

        //change open dialogue box potentially
        if (maxFeedDialogueBox.active || progressFeedDialogueBox.active)
        {
            OpenFullnessDialogueBox();
        }
    }

    //////////////////////////////////////////
    ///
    ///
    private void UpdateRelationshipUI(int relationship)
    {
        if (currentCat == null) return;

        List<Sprite> catRelationshipImages = catData.GetRelationshipImages();

        float catRelationshipRatio = currentCat.MaxRelationship / catRelationshipImages.Count;

        //update relationship image
        for (int i = 0; i < catRelationshipImages.Count; i++)
        {
            if (relationship <= catRelationshipRatio * (i + 1))
            {
                catRelationshipImage.overrideSprite = catRelationshipImages[i];
                break;
            }
        }

        //update text values
        progressRelationshipText.text = relationship + "/" + currentCat.MaxRelationship;
        maxRelationshipText.text = currentCat.MaxRelationship + "/" + currentCat.MaxRelationship;

        //change open dialogue box potentially
        if (maxRelationshipDialogueBox.active || progressRelationshipDialogueBox.active)
        {
            OpenRelationshipDialogueBox();
        }
    }

    //////////////////////////////////////////
    ///
    ///
    private void UpdateEntertainmentUI(int entertainment)
    {
        if (currentCat == null) return;

        float entertainmentPercent = entertainment / currentCat.MaxEntertainment;
        catEntertainmentBar.fillAmount = Mathf.Clamp(entertainmentPercent, 0, 1f);

        entertainmentText.text = entertainment + "/" + currentCat.MaxEntertainment;
    }


    //////////////////////////////////////////
    ///
    ///
    public void OpenRelationshipDialogueBox()
    {
        if (currentCat.Relationship == currentCat.MaxRelationship)
        {
            maxRelationshipDialogueBox.SetActive(true);
            progressRelationshipDialogueBox.SetActive(false);
        }
        else
        {
            maxRelationshipDialogueBox.SetActive(false);
            progressRelationshipDialogueBox.SetActive(true);
        }
    }

    public void CloseRelationshipDialogueBox()
    {
        maxRelationshipDialogueBox.SetActive(false);
        progressRelationshipDialogueBox.SetActive(false);
    }    

    //////////////////////////////////////////
    ///
    ///
    public void OpenEntertainmentDialogueBox()
    {
        entertainmentDialogueBox.SetActive(true);

    }

    //////////////////////////////////////////
    ///
    ///
    public void CloseEntertainmentDialogueBox()
    {
        entertainmentDialogueBox.SetActive(false);
    }

    //////////////////////////////////////////
    ///
    ///
    public void OpenFullnessDialogueBox()
    {
        if (currentCat.Fullness == currentCat.MaxFullness)
        {
            maxFeedDialogueBox.SetActive(true);
            progressFeedDialogueBox.SetActive(false);
        }
        else
        {
            maxFeedDialogueBox.SetActive(false);
            progressFeedDialogueBox.SetActive(true);
        }
    }

    //////////////////////////////////////////
    ///
    ///
    public void CloseFullnessDialogueBox()
    {
        maxFeedDialogueBox.SetActive(false);
        progressFeedDialogueBox.SetActive(false);
    }

    //////////////////////////////////////////
    ///
    ///
    public void CloseOpenDialogueBoxes()
    {
        foreach (GameObject dialogueBox in dialogueBoxes)
        {
            if (dialogueBox.active)
            {
                dialogueBox.SetActive(false);
            }
        }
    }

    //////////////////////////////////////////
    ///
    ///
    public void HideSelf()
    {
        gameObject.SetActive(false);
    }
}

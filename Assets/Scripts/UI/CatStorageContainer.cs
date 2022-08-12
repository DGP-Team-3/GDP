using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CatStorageContainer : MonoBehaviour
{
    [SerializeField] private CatData catData;
    [SerializeField] private CatDexData catDexData;

    [SerializeField] private TMP_Text nameField;
    [SerializeField] private TMP_Text firstTraitField;
    [SerializeField] private TMP_Text secondTraitField;
    [SerializeField] private TMP_Text textField;
    [SerializeField] private Image relationshipIcon;
    [SerializeField] private Image catPortraitImg;

    [SerializeField] private Button storageButton;

    [SerializeField] private Sprite storeCatSprite;
    [SerializeField] private Sprite takeOutCatSprite;

    private GameObject associatedCatGO;
    private Cat associatedCat;

    //////////////////////////////////////////
    ///
    ///
    private void OnEnable()
    {
        if (associatedCat == null) return;

        UpdateRelationshipText(associatedCat.Relationship);
        UpdateRelationshipDisplay(associatedCat.Relationship);
    }

    //////////////////////////////////////////
    ///
    ///
    private void OnDisable()
    {
        if (associatedCat == null) return;

        associatedCat.OnRelationshipUpdated -= UpdateRelationshipDisplay;
        associatedCat.OnRelationshipUpdated -= UpdateRelationshipText;
    }

    //////////////////////////////////////////
    ///
    ///
    public void SetupDisplay(GameObject catGO)
    {
        associatedCatGO = catGO;
        associatedCat = associatedCatGO.GetComponent<Cat>();

        nameField.text = associatedCat.GetName();
        firstTraitField.text = associatedCat.GetFirstTrait().ToString();
        secondTraitField.text = associatedCat.GetSecondTrait().ToString();
        catPortraitImg.sprite = catData.GetCatPortrait(associatedCat.CatType);

        associatedCat.OnRelationshipUpdated += UpdateRelationshipDisplay;
        associatedCat.OnRelationshipUpdated += UpdateRelationshipText;

        UpdateButtonDisplay();
        UpdateRelationshipText(associatedCat.Relationship);
        UpdateRelationshipDisplay(associatedCat.Relationship);
    }


    //////////////////////////////////////////
    ///
    ///
    private void UpdateRelationshipDisplay(int relationship)
    {
        if (associatedCat == null || relationshipIcon == null) return;

        List<Sprite> catRelationshipImages = catData.GetRelationshipImages();

        float catRelationshipRatio = associatedCat.MaxRelationship / (catRelationshipImages.Count - 1);

        for (int i = 0; i < catRelationshipImages.Count; i++)
        {
            if (relationship < catRelationshipRatio * (i + 1))
            {
                relationshipIcon.sprite = catRelationshipImages[i];
                break;
            }
        }
    }

    //////////////////////////////////////////
    ///
    ///
    private void UpdateRelationshipText(int relationship)
    {
        if (associatedCat == null || textField == null) return;

        // There 4 milestone texts
        // text 0 is shown at < 25
        // text 2 is shown at < 50
        // text 3 is shown at < 100
        // text 4 is shown at 100
        float catRelationshipRatio = associatedCat.MaxRelationship / 4;

        for (int i = 0; i <= 4; i++)
        {
            if (relationship < catRelationshipRatio * (i + 1))
            {
                textField.text = catDexData.GetMilestoneText(associatedCat.CatType, associatedCat.GetName(), i);
                break;
            }
        }
    }

    //////////////////////////////////////////
    ///
    ///
    public void UpdateButtonDisplay()
    {
        Image buttonImg = storageButton.gameObject.GetComponent<Image>();
        if (associatedCat.IsCatActive())
        {
            buttonImg.sprite = storeCatSprite;
        }
        else
        {
            buttonImg.sprite = takeOutCatSprite;
        }
    }

    //////////////////////////////////////////
    ///
    ///
    public GameObject GetAssociatedCat()
    {
        return associatedCatGO;
    }


    //////////////////////////////////////////
    ///
    ///
    public void StorageButtonClicked()
    {
        if (associatedCat.IsCatActive())
        {
            SFXPlayer.Instance.PlayOpenSound();
            GameManager.Instance.MakeCatInactive(associatedCatGO, true);
            UpdateButtonDisplay();
        }
        else
        {
            if (!GameManager.Instance.CanActivateMoreCats()) return;
            SFXPlayer.Instance.PlaySelectSound();
            GameManager.Instance.MakeCatActive(associatedCatGO, true);
            UpdateButtonDisplay();
        }
    }
}

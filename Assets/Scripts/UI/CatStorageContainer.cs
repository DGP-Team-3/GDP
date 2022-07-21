using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CatStorageContainer : MonoBehaviour
{
    [SerializeField] private CatData catData;

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
    private void OnDisable()
    {
        if (associatedCat == null) return;

        associatedCat.GetComponent<Cat>().OnRelationshipUpdated -= UpdateRelationshipDisplay;
    }

    private void Update()
    {
        UpdateRelationshipDisplay(associatedCat.Relationship);
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
        catPortraitImg.sprite = associatedCat.GetPortrait();

        associatedCat.OnRelationshipUpdated += UpdateRelationshipDisplay;

        UpdateButtonDisplay();

        //TODO: Setup text blurb
    }


    //////////////////////////////////////////
    ///
    ///
    private void UpdateRelationshipDisplay(int relationship)
    {
        if (associatedCat == null || relationshipIcon == null) return;

        List<Sprite> catRelationshipImages = catData.GetRelationshipImages();

        float catRelationshipRatio = associatedCat.MaxRelationship / catRelationshipImages.Count;

        for (int i = 0; i < catRelationshipImages.Count; i++)
        {
            if (relationship <= catRelationshipRatio * (i + 1))
            {
                relationshipIcon.overrideSprite = catRelationshipImages[i];
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
            buttonImg.overrideSprite = storeCatSprite;
        }
        else
        {
            buttonImg.overrideSprite = takeOutCatSprite;
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
            GameManager.Instance.MakeCatInactive(associatedCatGO, true);
            UpdateButtonDisplay();
        }
        else
        {
            if (!GameManager.Instance.CanActivateMoreCats()) return;

            GameManager.Instance.MakeCatActive(associatedCatGO, true);
            UpdateButtonDisplay();
        }
    }
}

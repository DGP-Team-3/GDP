using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CatDiscoveryContainer : MonoBehaviour
{

    [SerializeField] private TMP_Text nameField;
    [SerializeField] private TMP_Text firstTraitField;
    [SerializeField] private TMP_Text secondTraitField;
    [SerializeField] private Image relationshipIcon;
    [SerializeField] private Image catPortraitImg;
    [SerializeField] private Button fosterButton;


    private CatType catType;
    private Trait firstTrait;
    private Trait secondTrait;
    private string catName;


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
    public void FosterCat()
    {
        fosterButton.interactable = false;
        GameManager.Instance.CreateCat(catType, catName, firstTrait, secondTrait, 0, 100, 100);
    }

    public void EnableButton()
    {
        fosterButton.interactable = true;
    }

    //////////////////////////////////////////
    ///
    ///
    public void SetPortrait(Sprite portrait)
    {
        catPortraitImg.sprite = portrait;
    }


    //////////////////////////////////////////
    ///
    ///
    public void SetName(string name)
    {
        catName = name;
        nameField.text = name;
    }

    //////////////////////////////////////////
    ///
    ///
    public void SetSecondTrait(Trait trait)
    {
        secondTrait = trait;
        secondTraitField.text = trait.ToString();
    }


    //////////////////////////////////////////
    ///
    ///
    public void SetFirstTrait(Trait trait)
    {
        firstTrait = trait;
        firstTraitField.text = trait.ToString();
    }



    //////////////////////////////////////////
    ///
    ///
    public void SetCatType(CatType catType)
    {
        this.catType = catType;
    }
}

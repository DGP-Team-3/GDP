using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CatContainer : MonoBehaviour
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
    private Sprite catPortrait;
    private int relationshipValue;



    //////////////////////////////////////////
    ///
    ///
    public void FosterCat()
    {
        fosterButton.interactable = false;
        GameManager.Instance.CreateCat(catType, catName, firstTrait, secondTrait, catPortrait);
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
        catPortrait = portrait;
        catPortraitImg.sprite = portrait;
    }

    //////////////////////////////////////////
    ///
    ///
    public Sprite GetPortrait()
    {
        return catPortrait;
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
    public string GetName()
    {
        return catName;
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
    public Trait GetSecondTrait()
    {
        return secondTrait;
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
    public Trait GetFirstTrait()
    {
        return firstTrait;
    }


    //////////////////////////////////////////
    ///
    ///
    public void SetCatType(CatType catType)
    {
        this.catType = catType;
    }

    //////////////////////////////////////////
    ///
    ///
    public CatType GetCatType()
    {
        return catType;
    }


    //////////////////////////////////////////
    ///
    ///
    public Button GetFosterButton()
    {
        return fosterButton;
    }
}

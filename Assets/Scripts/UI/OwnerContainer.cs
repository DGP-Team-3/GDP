using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class OwnerContainer : MonoBehaviour
{
    [SerializeField] private TMP_Text textField;
    [SerializeField] private Image ownerPortrait;
    [SerializeField] private Button rehomeButton;
    [SerializeField] private TMP_Text ownerTextField;

    private string ownerName;
    private int ownerIndex;

    private Trait requiredTrait;
    private bool isRehomed = false;

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
    public void SetRequiredTrait(Trait trait)
    {
        requiredTrait = trait;
    }

    //////////////////////////////////////////
    ///
    ///
    public Trait GetRequiredTrait()
    {
        return requiredTrait;
    }

    //////////////////////////////////////////
    ///
    ///

    public void SetOwnerIndex(int ownerIndex)
    {
        this.ownerIndex = ownerIndex;
    }

    //////////////////////////////////////////
    ///
    ///

    public void SetOwnerName(string ownerName)
    {
        this.ownerName = ownerName;
        ownerTextField.text = ownerName;
    }

    //////////////////////////////////////////
    ///
    ///
    public string GetOwnerName()
    {
        return ownerName;
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
    public void SetRehomeButtonImage(Sprite buttonImage)
    {
        rehomeButton.gameObject.GetComponent<Image>().sprite = buttonImage;
    }


    //////////////////////////////////////////
    ///
    ///
    public TMP_Text GetTextField()
    {
        return textField;
    }

    //////////////////////////////////////////
    ///
    ///
    public bool GetIsRehomed()
    {
        return isRehomed;
    }

    //////////////////////////////////////////
    ///
    ///
    public void SetRehomed(bool isRehomed)
    {
        this.isRehomed = isRehomed;
    }
}

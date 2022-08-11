using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RehomeManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> containers;
    [SerializeField] private float timeTillOwnerReshuffle = 30f;
    [SerializeField] private CatData catData;

    [Header("Cat")]
    [SerializeField] private Image catImage;
    [SerializeField] private TMP_Text catNameText;
    [SerializeField] private TMP_Text firstTraitText;
    [SerializeField] private TMP_Text secondTraitText;

    [Header("Assets")]
    [SerializeField] private List<Sprite> ownerPortraits;
    [SerializeField] private Sprite rehomeButtonImage;
    [SerializeField] private Sprite successfulRehomeButtonImage;

    [Header("Text Assets")]
    [SerializeField] private List<string> failedResponces;
    [SerializeField] private List<string> successResponces;
    [SerializeField] private List<string> traitRequestText;

    private Cat selectedCat;

    private float reshuffleTimeElapsed = 0f;



    //////////////////////////////////////////
    ///
    ///
    private void Start()
    {
        GameManager.Instance.AddToClearList(gameObject);

        //GenerateAllOwners();
    }

    //////////////////////////////////////////
    ///
    ///
    private void Update()
    {
        //HandleCatReshuffle();
    }

    //////////////////////////////////////////
    ///
    ///
    private void HandleCatReshuffle()
    {
        reshuffleTimeElapsed += Time.deltaTime;

        if (reshuffleTimeElapsed >= timeTillOwnerReshuffle)
        {
            reshuffleTimeElapsed = 0f;

            GenerateAllOwners();

            EnableRehomeButtons();
        }
    }

    public void GenerateAllOwners()
    {
        // choose random container to guarantee a trait
        int index = UnityEngine.Random.Range(0, containers.Count);
        for (int i = 0; i < containers.Count; i++)
        {
            if (i == index)
            {
                Debug.Log("Trying to rig an owner");
                GenerateNewOwnerDisplay(containers[i], true);
            }
            else
            {
                GenerateNewOwnerDisplay(containers[i], false);
            }
        }
    }

    //////////////////////////////////////////
    ///
    ///
    private void GenerateNewOwnerDisplay(GameObject container, bool rigged)
    {
        OwnerContainer ownerContainer = container.GetComponent<OwnerContainer>();

        ownerContainer.SetRehomed(false);
        ownerContainer.SetRehomeButtonImage(rehomeButtonImage);

        int ownerIndex = Random.Range(0, ownerPortraits.Count);
        ownerContainer.GetOwnerPortrait().sprite = ownerPortraits[ownerIndex];

        Trait requiredTrait;
        if (rigged)
        {
            if (System.Enum.TryParse<Trait>(GetRiggedTrait(), out requiredTrait ))
            {

                Debug.Log("Rigged: " + requiredTrait);
            }
            else
            {
                requiredTrait = catData.GetRandomTrait();
            }
        }
        else
        {
            requiredTrait = catData.GetRandomTrait();
        }

        ownerContainer.SetRequiredTrait(requiredTrait);
        ownerContainer.GetTextField().text = traitRequestText[(int)requiredTrait];
    }

    private string GetRiggedTrait()
    {
        if (Random.Range(0, 2) == 0)
        {
            return firstTraitText.text;
        }
        else
        {
            return secondTraitText.text;
        }
    }

    //////////////////////////////////////////
    /// loads page with selected cat
    ///
    public void SetCatDisplay(GameObject cat)
    {
        selectedCat = null;

        if (cat == null)
        {
            DisableRehomeButtons();
            catImage.color = Color.clear;
            catNameText.text = " ";
            firstTraitText.text = " ";
            secondTraitText.text = " ";
        }
        else
        {
            selectedCat = cat.GetComponent<Cat>();
            catImage.overrideSprite = catData.GetCatSittingImage(selectedCat.CatType);
            catImage.color = Color.white;
            catNameText.text = selectedCat.GetName();
            firstTraitText.text = selectedCat.GetFirstTrait().ToString();
            secondTraitText.text = selectedCat.GetSecondTrait().ToString();
        }
    }


    //////////////////////////////////////////
    /// 
    ///
    private void EnableRehomeButtons()
    {
        foreach (GameObject container in containers)
        {
            container.GetComponent<OwnerContainer>().GetRehomeButton().interactable = true;
        }
    }


    //////////////////////////////////////////
    ///
    ///
    private void DisableRehomeButtons()
    {
        foreach (GameObject container in containers)
        {
            container.GetComponent<OwnerContainer>().GetRehomeButton().interactable = false;
        }
    }

    //////////////////////////////////////////
    ///
    ///
    private void DisableOtherRehomeButtons(int indexToNotDisable)
    {
        GameObject containerToNotDisable = containers[indexToNotDisable];
        foreach (GameObject container in containers)
        {
            if (containerToNotDisable != container)
            {
                container.GetComponent<OwnerContainer>().GetRehomeButton().interactable = false;
            }
        }
    }

    //////////////////////////////////////////
    ///
    ///
    private void DisableRehomeButton(int containerIdx)
    {
        containers[containerIdx].GetComponent<OwnerContainer>().GetRehomeButton().interactable = false;
    }

    //////////////////////////////////////////
    /// Initiates cat delete from game
    ///
    public void RehomeSelectedCat(int containerIndex)
    {
        OwnerContainer ownerContainer = containers[containerIndex].GetComponent<OwnerContainer>();

        if (ownerContainer.GetIsRehomed())
        {
            return;
        }

        bool success = false;
        if (selectedCat.GetFirstTrait() == ownerContainer.GetRequiredTrait() || selectedCat.GetSecondTrait() == ownerContainer.GetRequiredTrait())
        {
            success = true;
        }

        if (success)
        {
            SFXPlayer.Instance.PlayPurrSound();

            ownerContainer.SetRehomeButtonImage(successfulRehomeButtonImage);
            ownerContainer.SetRehomed(true);
            ownerContainer.GetTextField().text = successResponces[Random.Range(0, successResponces.Count)];

            DisableOtherRehomeButtons(containerIndex);

            GameManager.Instance.RemoveCat(selectedCat.gameObject);
            GameManager.Instance.IncrementNumCatsRehomed();
        }
        else
        {
            SFXPlayer.Instance.PlayMeow2Sound();

            DisableRehomeButton(containerIndex);
            ownerContainer.GetTextField().text = failedResponces[Random.Range(0, failedResponces.Count)];
        }
    }



}

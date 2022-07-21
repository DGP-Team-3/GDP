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

    private Cat selectedCat;

    private float reshuffleTimeElapsed = 0f;



    //////////////////////////////////////////
    ///
    ///
    private void Start()
    {
        foreach (GameObject container in containers)
        {
            GenerateNewOwnerDisplay(container);
        }
    }

    //////////////////////////////////////////
    ///
    ///
    private void Update()
    {
        HandleCatReshuffle();
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

            foreach (GameObject container in containers)
            {
                GenerateNewOwnerDisplay(container);
            }
        }
    }


    //////////////////////////////////////////
    ///
    ///
    private void GenerateNewOwnerDisplay(GameObject container)
    {
        OwnerContainer ownerContainer = container.GetComponent<OwnerContainer>();

        int index = Random.Range(0, ownerPortraits.Count);
        ownerContainer.GetOwnerPortrait().overrideSprite = ownerPortraits[index];

        //TODO: SET REQUEST TEXT
        ownerContainer.GetTextField().text = "I am looking for a cute cat!";
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
            EnableRehomeButtons();
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
    public void RehomeSelectedCat(int containerIndex)
    {
        DisableRehomeButtons();

        containers[containerIndex].GetComponent<OwnerContainer>().GetTextField().text = "Thank you!";

        //rehome the cat
        selectedCat.DestroyCat();
    }
}

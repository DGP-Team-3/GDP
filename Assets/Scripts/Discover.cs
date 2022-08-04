using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Discover : MonoBehaviour
{
    [SerializeField] private List<GameObject> containers;
    [SerializeField] private CatData catData;

    [SerializeField] private float timeTillCatReshuffle = 30f;

    private float reshuffleTimeElapsed = 0f;


    //////////////////////////////////////////
    ///
    ///
    private void Start()
    {
        GameManager.Instance.AddToClearList(gameObject);

        foreach (GameObject container in containers)
        {
            GenerateNewCatDisplay(container);
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

        if (reshuffleTimeElapsed >= timeTillCatReshuffle)
        {
            reshuffleTimeElapsed = 0f;
            
            foreach (GameObject container in containers)
            {
                GenerateNewCatDisplay(container);
                container.GetComponent<CatDiscoveryContainer>().EnableButton();
            }
        }
    }


    //////////////////////////////////////////
    ///
    ///
    private void GenerateNewCatDisplay(GameObject container)
    {
        CatDiscoveryContainer catContainer = container.GetComponent<CatDiscoveryContainer>();
        Trait firstTrait;
        Trait secondTrait;
        string catName;
        Sprite catPortrait;


        //TODO: Use probability instead for each cat type
        int catTypeIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(CatType)).Length);
        CatType catType = (CatType)catTypeIndex;


        catContainer.SetCatType(catType);


        //portrait
        catPortrait = catData.GetCatPortrait(catType);
        catContainer.SetPortrait(catPortrait);

        if (catData.IsUniqueCatType(catType))
        {
            //name
            catName = catData.GetUniqueCatName(catType);
            catContainer.SetName(catName);
        }
        else
        {
            //name
            catName = catData.GetRandomName();
            catContainer.SetName(catName);

            //traits
            firstTrait = catData.GetRandomTrait();
            secondTrait = catData.GetRandomTrait();
            while (firstTrait == secondTrait)
            {
                firstTrait = catData.GetRandomTrait();
                secondTrait = catData.GetRandomTrait();
            }

            catContainer.SetFirstTrait(firstTrait);
            catContainer.SetSecondTrait(secondTrait);

        }
    }

}


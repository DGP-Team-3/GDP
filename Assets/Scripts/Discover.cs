using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Discover : MonoBehaviour
{
    [SerializeField] private List<GameObject> containers;
    [SerializeField] private CatData catData;

    [SerializeField] private TMP_Text timerText;

    [SerializeField] private float timeTillCatReshuffle = 30f;
    [SerializeField] private int percentChanceOfUnique = 5;

    private List<CatType> availableUniqueCats;
    private float reshuffleTimeElapsed = 0f;


    //////////////////////////////////////////
    ///
    ///
    private void Start()
    {
        GameManager.Instance.AddToClearList(gameObject);

        bool generatedUnique = false;
        foreach (GameObject container in containers)
        {
            if (generatedUnique)
            {
                GenerateNewCatDisplay(container, false);
            }
            else
            {
                generatedUnique = GenerateNewCatDisplay(container, true);
            }
        }
    }

    //////////////////////////////////////////
    ///
    ///
    private void Update()
    {
        HandleCatReshuffle();
        HandleTimerDisplay();
    }

    //////////////////////////////////////////
    ///
    ///
    private void HandleTimerDisplay()
    {
        int timeElapsed = Mathf.FloorToInt(reshuffleTimeElapsed);
        int displayTimer = (int)timeTillCatReshuffle - timeElapsed;
        timerText.text = "Cats will refresh in " + displayTimer;
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
            bool generatedUnique = false;
            foreach (GameObject container in containers)
            {
                if (generatedUnique)
                {
                    GenerateNewCatDisplay(container, false);
                }
                else
                {
                    generatedUnique = GenerateNewCatDisplay(container, true);
                }
                container.GetComponent<CatDiscoveryContainer>().EnableButton();
            }
        }
    }


    //////////////////////////////////////////
    ///
    ///Returns true if it generates a unique cat
    private bool GenerateNewCatDisplay(GameObject container, bool canGenUnique)
    {
        CatDiscoveryContainer catContainer = container.GetComponent<CatDiscoveryContainer>();
        Trait firstTrait;
        Trait secondTrait;
        string catName;
        Sprite catPortrait;
        CatType catType;

        // The three conditions are
        // 1) Has not generated a Unique cat in this group of three
        // 2) a percentage chance roll
        // 3) that GameManager has finished loading the list of Available Unique Cats. There was a problem where this was executing before it loaded in
        if (canGenUnique && (UnityEngine.Random.Range(0, 100) <= percentChanceOfUnique) && GameManager.Instance.GetAvailableUniqueCats() != null)
        {
            availableUniqueCats = new List<CatType>(GameManager.Instance.GetAvailableUniqueCats());

            // Have to check if there are still unique cats available after checking if the list is valid
            if (availableUniqueCats.Count != 0)
            {
                
                catType = availableUniqueCats[UnityEngine.Random.Range(0, availableUniqueCats.Count)];
            }
            else
            {
                int catTypeIndex = UnityEngine.Random.Range(0, 5);
                catType = (CatType)catTypeIndex;
            }
        }
        else
        {
            int catTypeIndex = UnityEngine.Random.Range(0, 5);
            catType = (CatType)catTypeIndex;
        }

        catContainer.SetCatType(catType);

        //portrait
        catPortrait = catData.GetCatPortrait(catType);
        catContainer.SetPortrait(catPortrait);

        if (catData.IsUniqueCatType(catType))
        {
            //name
            catName = catData.GetUniqueCatName(catType);
            catContainer.SetName(catName);
            catContainer.SetCatType(catType);
            catContainer.SetFirstTrait(catData.GetCatPrefab(catType).GetComponent<Cat>().GetFirstTrait());
            catContainer.SetSecondTrait(catData.GetCatPrefab(catType).GetComponent<Cat>().GetSecondTrait());
            return true;
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
            return false;
        }
    }
}


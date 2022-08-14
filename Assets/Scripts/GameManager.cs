using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }


    [SerializeField] private CatData catData;

    [SerializeField] private List<GameObject> cats;

    [SerializeField] private int maxActiveCats = 3;

    [SerializeField] private Vector2 hidePos = new Vector2(-100, -100);

    [Min(0f)]
    [SerializeField] private float blackoutSpeed = 1f;
    
    [Tooltip("Time till autosaving occurs.")]
    [SerializeField] private float autoSaveTime = 30f;


    [SerializeField] private SpawnArea spawnArea;


    private float timeElapsedTillSave = 0f;
    private float blackoutTVal = 0f;


    private int currentActiveCats = 0;
    private List<GameObject> activeCats = new List<GameObject>();
    private List<Vector2> activeCatPositions = new List<Vector2>();

    private GameObject blackout;

    private Cat selectedCat;

    private bool isMinigameActive = false;
    public bool IsMinigameActive => isMinigameActive;    

    private bool isBlackingOut = false;
    public bool IsBlackingOut => isBlackingOut;


    private List<GameObject> gameObjectsToClear = new List<GameObject>();


    private int numCatsRehomed = 0;
    private int numSpecialCatsFound = 0;
    private int numNormalCatsFound = 0;
    [SerializeField] private List<GameObject> rehomeRecords;
    private List<CatType> availableUniqueCats;

    //////////////////////////////////////////
    ///
    ///
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); //Make persistant
        }
    }

    //////////////////////////////////////////
    ///
    ///
    private void Start()
    {
        availableUniqueCats = new List<CatType>();
        rehomeRecords = new List<GameObject>();
        //try loading saved data
        if (!LoadData())
        {
            CreateCat(CatType.loafCat, "Loaf", Trait.Loyal, Trait.Intelligent, 0, 100, 100, true);
            availableUniqueCats = new List<CatType> { CatType.frogCat, CatType.komiCat, CatType.momoCat, CatType.skyCat, CatType.witchCat };
            IncrementNumCatsFound(CatType.loafCat);
        }
    }

    //////////////////////////////////////////
    ///
    ///
    private void Update()
    {
        if (!IsMinigameActive)
        {
            timeElapsedTillSave += Time.deltaTime;

            if (timeElapsedTillSave >= autoSaveTime)
            {
                SaveData();
                timeElapsedTillSave = 0f;
            }
        }


        if (isBlackingOut)
        {
            blackoutTVal += Time.deltaTime * blackoutSpeed;
        }
    }

    //////////////////////////////////////////
    ///
    ///
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveData();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveData();
        }
    }


#region Data Management



    //////////////////////////////////////////
    /// Will increment the number of cats found based on uniqueness or not
    ///
    public void IncrementNumCatsFound(CatType catType)
    {
        if (catData.IsUniqueCatType(catType))
        {
            numSpecialCatsFound++;
            availableUniqueCats.Remove(catType);
        }
        else
        {
            numNormalCatsFound++;
        }
        SaveData();
    }
    
    public void IncrementNumCatsRehomed()
    {
        numCatsRehomed++;
        SaveData();
    }

    public int GetNumCatsRehomed()
    {
        return numCatsRehomed;
    }

    public int GetNumNormalCatsFound()
    {
        return numNormalCatsFound;
    }

    public int GetNumSpecialCatsFound()
    {
        return numSpecialCatsFound;
    }

    public List<CatType> GetAvailableUniqueCats()
    {
        return availableUniqueCats;
    }


    //////////////////////////////////////////
    ///
    ///
    private void SaveData()
    {
        int numCats = cats.Count;
        string[] names = new string[numCats];
        int[] catTypes = new int[numCats];
        int[] firstTraits = new int[numCats];
        int[] secondTraits = new int[numCats];
        int[] relationshipValues = new int[numCats];
        int[] fullnessValues = new int[numCats];
        int[] entertainmentValues = new int[numCats];
        bool[] activeStates = new bool[numCats];

        for (int i = 0; i < numCats; i++)
        {
            Cat cat = cats[i].GetComponent<Cat>();

            names[i] = cat.GetName();
            catTypes[i] = (int)cat.CatType;
            firstTraits[i] = (int)cat.GetFirstTrait();
            secondTraits[i] = (int)cat.GetSecondTrait();
            relationshipValues[i] = cat.Relationship;
            fullnessValues[i] = cat.Fullness;
            entertainmentValues[i] = cat.Entertainment;
            activeStates[i] = cat.IsCatActive();
        }

        string[] rehomedCatNames = new string[numCatsRehomed];
        int[] rehomedCatTypes = new int[numCatsRehomed];
        string[] ownerNames = new string[numCatsRehomed];
        int[] ownerIndexes = new int[numCatsRehomed];


        for (int j = 0; j < numCatsRehomed; j++)
        {
            RehomeRecord record = rehomeRecords[j].GetComponent<RehomeRecord>();
            rehomedCatNames[j] = record.GetCatName();
            rehomedCatTypes[j] = (int)record.GetCatType();
            ownerNames[j] = record.GetOwnerName();
            ownerIndexes[j] = record.GetOwnerIndex();
        }

        SaveSystem.SavePlayerData(numCats, names, catTypes, firstTraits, secondTraits, 
            relationshipValues, fullnessValues, entertainmentValues, activeStates, numNormalCatsFound, numSpecialCatsFound, numCatsRehomed,
            rehomedCatNames, rehomedCatTypes, ownerNames, ownerIndexes, availableUniqueCats);
    }


    //////////////////////////////////////////
    /// returns a bool value based on if data loading was successful or not, which is determined on a file existing already or not
    ///
    private bool LoadData()
    {
        //load previously saved data
        PlayerData data = SaveSystem.LoadPlayerData();

        if (data == null) return false;

        //Load each cat into game
        for (int i = 0; i < data.numCats; i++)
        {
            CreateCat((CatType)data.catTypes[i], data.names[i], (Trait)data.firstTraits[i], (Trait)data.secondTraits[i],
                data.relationshipValues[i], data.fullnessValues[i], data.entertainmentValues[i], data.activeStates[i]);
        }

        numNormalCatsFound = data.numNormalCatsFound;
        numSpecialCatsFound = data.numSpecialCatsFound;
        numCatsRehomed = data.numCatsRehomed;

        //Load Rehome History
        RehomeHistoryHandler rehomeHandler = FindObjectOfType<RehomeHistoryHandler>();
        for (int j = 0; j < numCatsRehomed; j++)
        {
            RecordRehome(data.rehomedCatNames[j], (CatType)data.rehomedCatTypes[j], data.ownerNames[j], data.ownerIndexes[j]);
        }

        for (int k = 0; k < data.availableUniqueCats.Length; k++)
        {
            availableUniqueCats.Add((CatType)data.availableUniqueCats[k]);
        }

        return true;
    }


#endregion //Data Management



#region Scene Management

    //////////////////////////////////////////
    ///
    ///
    private void SetMinigameActive(bool isMinigameActive)
    {
        this.isMinigameActive = isMinigameActive;
    }

    //////////////////////////////////////////
    /// Load feeding minigame
    ///
    public void LoadFeedingMiniGame()
    {
        StartCoroutine(Transition(SceneBuildData.feedingSceneBuildIndex));
    }    
    
    //////////////////////////////////////////
    /// Load entertainment minigame
    ///
    public void LoadEntertainmentMiniGame()
    {
        StartCoroutine(Transition(SceneBuildData.pettingSceneBuildIndex));
    }

    //////////////////////////////////////////
    /// Load main game scene
    ///
    public void LoadMainScene()
    {
        SaveData();

        SetMinigameActive(false);

        MoveAllActiveCats(true);

        SceneManager.LoadScene(SceneBuildData.mainGameSceneBuildIndex);
    }

    //////////////////////////////////////////
    ///
    /// 
    private IEnumerator Transition(int buildIndex)
    {
        SaveData();

        isBlackingOut = true;
        blackout = GameObject.FindGameObjectWithTag("Blackout");

        GameObject tutorialButton = GameObject.FindGameObjectWithTag("TutorialButton");
        if (tutorialButton != null)
        {
            tutorialButton.SetActive(false);
        }

        CatPopUpHandler popup = FindObjectOfType<CatPopUpHandler>();
        if (popup != null)
        {
            popup.HideSelf();
        }
        SliderMenu slider = FindObjectOfType<SliderMenu>();
        if (slider != null)
        {
            slider.HideSelf();
        }

        SpriteRenderer spriteRenderer = blackout.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        while (spriteRenderer.color != Color.black)
        {
            spriteRenderer.color = Color.Lerp(originalColor, Color.black, blackoutTVal);
            yield return null;
        }

        isBlackingOut = false;

        DeleteClearList();

        yield return new WaitForSeconds(1f);

        SetMinigameActive(true);
        SaveActiveCatPositions();
        MoveAllActiveCats(false);
        blackoutTVal = 0f;

        SceneManager.LoadScene(buildIndex);
    }


    //////////////////////////////////////////
    ///
    ///
    public void AddToClearList(GameObject go)
    {
        gameObjectsToClear.Add(go);
    }

    //////////////////////////////////////////
    /// Clears list and destroys game objects in scene
    ///
    private void DeleteClearList()
    {
        foreach (GameObject go in gameObjectsToClear)
        {
            Destroy(go);
        }

        gameObjectsToClear.Clear();
    }

#endregion //Scene Management



#region Cat Management

    //////////////////////////////////////////
    /// Adds cat to the collection
    ///
    public void CreateCat(CatType catType, string catName, Trait firstTrait, Trait secondTrait, 
        int currentRelationshipValue, int currentFullnessValue, int currentEntertainmentValue, bool tryActivateCat)
    {
        Vector2 pos = RetrieveRandomPosition();
        GameObject catGO = Instantiate(catData.GetCatPrefab(catType), pos, Quaternion.identity);
        DontDestroyOnLoad(catGO);
        cats.Add(catGO);

        Cat cat = catGO.GetComponent<Cat>();
        cat.InitCatValues(catName, firstTrait, secondTrait, catData.GetCatRelationshipValue(catType), currentRelationshipValue, currentFullnessValue, currentEntertainmentValue, catType);

        if (tryActivateCat && currentActiveCats < maxActiveCats)
        {
            MakeCatActive(catGO, false);
        }
        else
        {
            MakeCatInactive(catGO, false);
        }

        UpdateStorageSystem(catGO);
    }

    //////////////////////////////////////////
    /// Updated the storage display with the new cat
    ///
    private void UpdateStorageSystem(GameObject catGO)
    {
        CatStorageHandler storageHandler = FindObjectOfType<CatStorageHandler>();
        storageHandler.AddContainer(catGO);
    }

    private Vector2 RetrieveRandomPosition()
    {
        float xPos = Random.Range(spawnArea.XMinSpawn, spawnArea.XMaxSpawn);
        float yPos = Random.Range(spawnArea.YMinSpawn, spawnArea.YMaxSpawn);
        return new Vector2(xPos, yPos);
    }

    //////////////////////////////////////////
    /// Moves active cats to saved positions
    ///
    private void MoveAllActiveCats(bool isReturningToMainScene)
    {
        if (isReturningToMainScene)
        {
            for (int i = 0; i < activeCats.Count; i++)
            {
                activeCats[i].transform.position = activeCatPositions[i];
            }
        }
        else
        {
            for (int i = 0; i < activeCats.Count; i++)
            {
                activeCats[i].transform.position = hidePos;
            }
        }
    }

    //////////////////////////////////////////
    /// Check if a cat can be activated
    ///
    public bool CanActivateMoreCats()
    {
        if (currentActiveCats < maxActiveCats)
        {
            return true;
        }
        return false;
    }

    //////////////////////////////////////////
    ///
    ///
    private void SaveActiveCatPositions()
    {
        ClearActiveCatPositions();

        foreach (GameObject activeCat in activeCats)
        {
            activeCatPositions.Add(new Vector2(activeCat.transform.position.x, activeCat.transform.position.y));
        }
    }

    //////////////////////////////////////////
    ///
    ///
    private void ClearActiveCatPositions()
    {
        activeCatPositions.Clear();
    }

    //////////////////////////////////////////
    ///
    ///
    public void MakeCatActive(GameObject catGO, bool isPreviouslyInactive)
    {
        currentActiveCats++;
        catGO.GetComponent<Cat>().SetCatActive(true);
        activeCats.Add(catGO);

        if (isPreviouslyInactive)
        {
            catGO.transform.position = RetrieveRandomPosition();
        }
    }

    //////////////////////////////////////////
    ///
    ///
    public void MakeCatInactive(GameObject catGO, bool isPreviouslyActive)
    {
        if (isPreviouslyActive)
        {
            activeCats.Remove(catGO);
            currentActiveCats--;
        }

        catGO.transform.position = hidePos;
        catGO.GetComponent<Cat>().SetCatActive(false);
    }

    //////////////////////////////////////////
    ///
    ///
    public void SetSelectedCat(Cat selectedCat)
    {
        this.selectedCat = selectedCat;
    }

    //////////////////////////////////////////
    ///
    ///
    public Cat GetSelectedCat()
    {
        return this.selectedCat;
    }
    //////////////////////////////////////////
    ///
    ///
    public void IncreaseSelectedCatFullness ()
    {
        if (selectedCat == null) return;

        selectedCat.FeedCat();
    }

    //////////////////////////////////////////
    ///
    ///
    public void IncreaseSelectedCatEntertainment()
    {
        if (selectedCat == null) return;

        selectedCat.EntertainCat();
    }

    //////////////////////////////////////////
    ///
    ///
    public List<GameObject> GetCatList()
    {
        return cats;
    }

    public void RehomeCat(GameObject catGO, string ownerName, int ownerIndex)
    {
        Cat catScript = catGO.GetComponent<Cat>();
        RecordRehome(catScript.GetName(), catScript.CatType, ownerName, ownerIndex);
        RemoveCat(catGO);
    }
    public void RecordRehome(string catName, CatType catType, string ownerName, int ownerIndex)
    {
        GameObject record = new GameObject();
        DontDestroyOnLoad(record);
        record.AddComponent<RehomeRecord>();
        record.GetComponent<RehomeRecord>().InitRehomeRecord(catName, catType, ownerName, ownerIndex);
        if (Object.ReferenceEquals(rehomeRecords, null))
        {
            Debug.Log("rehomeRecords is null");
        }
        rehomeRecords.Add(record);
        RehomeHistoryHandler rehomeHandler = FindObjectOfType<RehomeHistoryHandler>();
        rehomeHandler.AddRehomeHistoryContainer(catName, catType, ownerName, ownerIndex);
    }

    //////////////////////////////////////////
    /// Removes cat data and destroys cat
    ///
    private void RemoveCat(GameObject catGO)
    {
        cats.Remove(catGO);

        for (int i = 0; i < activeCats.Count; i++)
        {
            if (activeCats[i] == catGO)
            {
                if (i < activeCatPositions.Count)
                {
                    activeCatPositions.RemoveAt(i);
                }

                activeCats.RemoveAt(i);
                break;
            }
        }

        //remove cat storage container
        CatStorageHandler storageHandler = FindObjectOfType<CatStorageHandler>();
        if (storageHandler != null)
        {
            storageHandler.DestroyContainer(catGO);
        }

        catGO.GetComponent<Cat>().DestroyCat();
    }

    #endregion //Cat Management


}

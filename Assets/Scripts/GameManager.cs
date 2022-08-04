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
        //temporary
        CreateCat(CatType.loafCat, "Loaf", Trait.Loyal, Trait.Intelligent, 0, 100, 100);
    }

    //////////////////////////////////////////
    ///
    ///
    private void Update()
    {
        if (isBlackingOut)
        {
            blackoutTVal += Time.deltaTime * blackoutSpeed;
        }
    }



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
        SetMinigameActive(false);

        MoveAllActiveCats(true);

        SceneManager.LoadScene(SceneBuildData.mainSceneBuildIndex);
    }

    //////////////////////////////////////////
    ///
    /// 
    private IEnumerator Transition(int buildIndex)
    {
        isBlackingOut = true;
        blackout = GameObject.FindGameObjectWithTag("Blackout");

        FindObjectOfType<CatPopUpHandler>().HideSelf();
        FindObjectOfType<SliderMenu>().HideSelf();

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
    public void CreateCat(CatType catType, string catName, Trait firstTrait, Trait secondTrait, int currentRelationshipValue, int currentFullnessValue, int currentEntertainmentValue)
    {
        Vector2 pos = RetrieveRandomPosition();
        GameObject catGO = Instantiate(catData.GetCatPrefab(catType), pos, Quaternion.identity);
        DontDestroyOnLoad(catGO);
        cats.Add(catGO);

        Cat cat = catGO.GetComponent<Cat>();
        cat.InitCatValues(catName, firstTrait, secondTrait, catData.GetCatRelationshipValue(catType), currentRelationshipValue, currentFullnessValue, currentEntertainmentValue, catType);

        if (currentActiveCats < maxActiveCats)
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
        SpawnArea area = FindObjectOfType<SpawnArea>();

        float xPos = Random.Range(area.XMinSpawn, area.XMaxSpawn);
        float yPos = Random.Range(area.YMinSpawn, area.YMaxSpawn);
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

    //////////////////////////////////////////
    /// Removes cat data and destroys cat
    ///
    public void RemoveCat(GameObject catGO)
    {
        cats.Remove(catGO);

        for (int i = 0; i < activeCats.Count; i++)
        {
            if (activeCats[i] == catGO)
            {
                activeCatPositions.RemoveAt(i);
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

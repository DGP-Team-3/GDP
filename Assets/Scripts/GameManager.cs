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


    private int currentActiveCats = 0;

    private List<GameObject> activeCats = new List<GameObject>();
    private List<Vector2> activeCatPositions = new List<Vector2>();

    private bool isMinigameActive = false;
    public bool IsMinigameActive => isMinigameActive;

    private Cat selectedCat;


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
    /// Adds cat to the collection
    ///
    public void CreateCat(CatType catType, string catName, Trait firstTrait, Trait secondTrait, Sprite catPortrait)
    {
        Vector2 pos = RetrieveRandomPosition();
        GameObject catGO = Instantiate(catData.GetCatPrefab(catType), pos, Quaternion.identity);
        DontDestroyOnLoad(catGO);
        cats.Add(catGO);

        Cat cat = catGO.GetComponent<Cat>();
        cat.InitCatValues(catName, firstTrait, secondTrait, catPortrait, catData.GetCatRelationshipValue(catType));

        if (currentActiveCats < maxActiveCats)
        {
            MakeCatActive(catGO, false);
        }
        else
        {
            MakeCatInactive(catGO, false);
        }
    }

    private Vector2 RetrieveRandomPosition()
    {
        SpawnArea area = FindObjectOfType<SpawnArea>();

        float xPos = Random.Range(area.XMinSpawn, area.XMaxSpawn);
        float yPos = Random.Range(area.YMinSpawn, area.YMaxSpawn);
        return new Vector2(xPos, yPos);
    }

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
        SetMinigameActive(true);

        SaveActiveCatPositions();
        MoveAllActiveCats(false);

        SceneManager.LoadScene(SceneBuildData.feedingSceneBuildIndex);
    }    
    
    //////////////////////////////////////////
    /// Load entertainment minigame
    ///
    public void LoadEntertainmentMiniGame()
    {
        SetMinigameActive(true);

        SaveActiveCatPositions();
        MoveAllActiveCats(false);

        SceneManager.LoadScene(SceneBuildData.pettingSceneBuildIndex);
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
        catGO.SetActive(true);
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
            catGO.transform.position = hidePos;
        }
        
        catGO.GetComponent<Cat>().SetCatActive(false);
        catGO.SetActive(false);
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
        selectedCat.FeedCat();
    }

    //////////////////////////////////////////
    ///
    ///
    public void IncreaseSelectedCatEntertainment()
    {
        selectedCat.EntertainCat();
    }
}

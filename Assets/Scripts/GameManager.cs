using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }


    [SerializeField] private List<Cat> cats;


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
    /// Load feeding minigame
    ///
    public void LoadFeedingMiniGame()
    {
        SceneManager.LoadScene(SceneBuildData.feedingSceneBuildIndex);
    }

    //////////////////////////////////////////
    /// Load main game scene
    ///
    public void LoadMainScene()
    {
        SceneManager.LoadScene(SceneBuildData.mainSceneBuildIndex);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedingMinigameManager : MonoBehaviour
{
    private static FeedingMinigameManager _instance;
    public static FeedingMinigameManager Instance { get { return _instance; } }


    [Tooltip("Amount of time to wait when showing an expression.")]
    [SerializeField] private float expressionWaitTime = 3f;

    [SerializeField] private ExpressionAudioPlayer expressionAudioPlayer;
    
    [Space]
    
    [Tooltip("List of different food items.")]
    [SerializeField] List<GameObject> foodObjects;

    [SerializeField] private FoodDeposit foodDeposit;

    [Space]

    [Min(0f)]
    [SerializeField] private float blackoutSpeed = 1f;

    [SerializeField] private GameObject blackout;
    [SerializeField] private GameObject greenCheck;


    private FoodType wantedFoodType;

    private bool isCatMad = false;
    private bool isCatHappy = false;
    private bool isBlackingOut = false;

    private float timeElapsed = 0f;
    private float blackoutTVal = 0f;

    int wantedFoodItemIndex;


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
        }
    }

    //////////////////////////////////////////
    ///
    /// 
    private void Start()
    {
        timeElapsed = 0f;

        wantedFoodItemIndex = Random.Range(0, foodObjects.Count);
        wantedFoodType = foodObjects[wantedFoodItemIndex].GetComponent<Food>().GetFoodType;
        foodDeposit.SetFoodSprite(wantedFoodType);
    }

    //////////////////////////////////////////
    ///
    /// 
    private void Update()
    {
        if (isCatMad)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > expressionWaitTime)
            {
                isCatMad = false;
                timeElapsed = 0f;
                foodDeposit.SetFoodSprite(wantedFoodType);
                EnableDragControls();
            }
        }

        if (isCatHappy)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > expressionWaitTime)
            {
                isCatHappy = false;
                TransitionToMainScene();
            }
        }

        if (isBlackingOut)
        {
            blackoutTVal += Time.deltaTime * blackoutSpeed;
        }
    }

    //////////////////////////////////////////
    /// Check if given food type is the wanted food type
    /// 
    public bool CheckValidFood(FoodType foodType)
    {
        if (wantedFoodType == foodType)
        {
            return true;
        }
        return false;
    }

    //////////////////////////////////////////
    ///
    /// 
    public void DisableDragControls()
    {
        DragController.Instance.gameObject.SetActive(false);
    }

    //////////////////////////////////////////
    ///
    /// 
    public void EnableDragControls()
    {
        DragController.Instance.gameObject.SetActive(true);
    }

    //////////////////////////////////////////
    ///
    /// 
    public void CorrectFoodDelivered()
    {
        DisableDragControls();
        expressionAudioPlayer.PlayHappySound();
        isCatHappy = true;
        foodDeposit.SetExpressionSprite(true);
        Destroy(foodObjects[wantedFoodItemIndex].gameObject);

        StartCoroutine(EnableGreenCheck());

        GameManager.Instance.IncreaseSelectedCatFullness();
    }

    private IEnumerator EnableGreenCheck()
    {
        yield return new WaitForSeconds(0.75f);
        greenCheck.SetActive(true);
    }

    //////////////////////////////////////////
    ///
    /// 
    public void IncorrectFoodDelivered()
    {
        DisableDragControls();
        expressionAudioPlayer.PlayMadSound();
        isCatMad = true;
        foodDeposit.SetExpressionSprite(false);
    }


    //////////////////////////////////////////
    ///
    /// 
    private void TransitionToMainScene()
    {
        StartCoroutine(Transition());
    }


    //////////////////////////////////////////
    ///
    /// 
    private IEnumerator Transition()
    {
        yield return new WaitForSeconds(1f);
        isBlackingOut = true;
        SpriteRenderer spriteRenderer = blackout.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;

        while (spriteRenderer.color != Color.black)
        {

            spriteRenderer.color = Color.Lerp(originalColor, Color.black, blackoutTVal);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        GameManager.Instance.LoadMainScene();
    }

}

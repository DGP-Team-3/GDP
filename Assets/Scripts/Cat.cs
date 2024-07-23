using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private string catName;
    [SerializeField] private Trait firstTrait;
    [SerializeField] private Trait secondTrait;

    [Space]

    [Tooltip("Number of seconds until cat's fullness value decreases.")]
    [Min(0f)]
    [SerializeField] private float hungerProcTime = 1f;

    [Tooltip("Number of seconds until cat's entertainment value decreases.")]
    [Min(0f)]
    [SerializeField] private float entertainmentProcTime = 1f;

    [Tooltip("Amount to modify relationship by. Negative values decrease and positive increase.")]
    [SerializeField] private int relationshipDecreaseAmount = 5;

    [Tooltip("Amount to increase relationship by. Negative values decrease and positive increase.")]
    [Min(0f)]
    [SerializeField] private int relationshipIncreaseAmount = 10;

    [Tooltip("Amount to increase fullness by. Negative values decrease and positive increase.")]
    [Min(0f)]
    [SerializeField] private int feedAmount = 85;

    [Tooltip("Amount to increase entertainment. Negative values decrease and positive increase.")]
    [Min(0f)]
    [SerializeField] private int entertainAmount = 90;

    [Tooltip("Max value of fullness. Fullness is modified over time or feeding.")]
    [Min(0f)]
    [SerializeField] private int maxFullness = 100;
    public float MaxFullness => maxFullness;

    [Tooltip("Max value of entertainment. Entertainment is modified over time or playing.")]
    [Min(0f)]
    [SerializeField] private int maxEntertainment = 100;
    public float MaxEntertainment => maxEntertainment;
    
    [Tooltip("Max value of relationship. Relationship is decreased when not fed or played with, increased by interactions (minigames).")]
    [Min(0f)]
    [SerializeField] private int maxRelationship = 100;
    public float MaxRelationship => maxRelationship;
    
    [Space]

    [SerializeField] private Animator animator;

    
    private int _fullness;
    public int Fullness => _fullness;


    private int _relationship;
    public int Relationship => _relationship;


    private int _entertainment;
    public int Entertainment => _entertainment;

    // Timers
    private float hungerTimer = 0f;
    private float entertainmentTimer = 0f;

    private bool isActive = true;

    private CatType catType;
    public CatType CatType => catType;


    public delegate void HungerUpdated(int fullness);
    public event HungerUpdated OnHungerUpdated;

    public delegate void RelationshipUpdated(int relationship);
    public event RelationshipUpdated OnRelationshipUpdated;

    public delegate void EntertainmentUpdated(int entertainment);
    public event EntertainmentUpdated OnEntertainmentUpdated;

    
    void Update()
    {
        if (IsCatActive() && !GameManager.Instance.IsMinigameActive)
        {
            HandleHunger();
            HandleEntertainment();
        }
    }

    
    /// <summary>
    /// Initializes cat values.
    /// Called when fostering a generated cat or when populating when loading data
    /// </summary>
    /// <param name="name">Name of the cat.</param>
    /// <param name="traitOne">First triat.</param>
    /// <param name="traitTwo">Second triat.</param>
    /// <param name="relationshipMaxValue">Max relationship value.</param>
    /// <param name="currentRelationshipValue">Current relationship value.</param>
    /// <param name="currentFullnessValue">Current value of fullness.</param>
    /// <param name="currentEntertainmentValue">Current value of entertainment.</param>
    /// <param name="catType">Type of cat.</param>
    public void InitCatValues(string name, Trait traitOne, Trait traitTwo, int relationshipMaxValue, int currentRelationshipValue, int currentFullnessValue, int currentEntertainmentValue, CatType catType)
    {
        catName = name;

        //for common cats. Unique cat traits set by prefab
        if (traitOne != null && traitTwo != null)
        {
            firstTrait = traitOne;
            secondTrait = traitTwo;
        }

        this.catType = catType;

        maxRelationship = relationshipMaxValue;
        maxFullness = 100;
        maxEntertainment = 100;


        _relationship = currentRelationshipValue;
        _fullness = currentFullnessValue;
        _entertainment = currentEntertainmentValue;
    }
    
    /// update hunger value
    private void HandleHunger()
    {
        hungerTimer += Time.deltaTime;

        if (hungerTimer >= hungerProcTime)
        {
            _fullness = Mathf.Clamp(--_fullness, 0, maxFullness);
            OnHungerUpdated?.Invoke(_fullness);
            hungerTimer = 0f;

            if (_fullness == 0)
            {
                ModifyRelationship(relationshipDecreaseAmount);
            }
        }
    }

    /// update entertainment value
    private void HandleEntertainment()
    {
        entertainmentTimer += Time.deltaTime;

        if (entertainmentTimer >= entertainmentProcTime)
        {
            _entertainment = Mathf.Clamp(--_entertainment, 0, maxEntertainment);
            OnEntertainmentUpdated?.Invoke(_entertainment);
            entertainmentTimer = 0f;

            if (_entertainment == 0)
            {
                ModifyRelationship(relationshipDecreaseAmount);
            }
        }
    }

    /// update fullness value
    public void FeedCat()
    {
        _fullness = Mathf.Clamp(_fullness + feedAmount, 0, maxFullness);
        ModifyRelationship(relationshipIncreaseAmount);
        OnHungerUpdated?.Invoke(_fullness);
    }

    /// update entertainment value
    public void EntertainCat()
    {
        _entertainment = Mathf.Clamp(_entertainment + entertainAmount, 0, maxEntertainment);
        ModifyRelationship(relationshipIncreaseAmount);
        OnEntertainmentUpdated?.Invoke(_entertainment);
    }
    
    /// update relationship value
    private void ModifyRelationship(int value)
    {
        _relationship = Mathf.Clamp(value + _relationship, 0, maxRelationship);
        OnRelationshipUpdated?.Invoke(_relationship);
    }

    #region getters
    
    public string GetName()
    {
        return catName;
    }
    
    public Trait GetFirstTrait()
    {
        return firstTrait;
    }

    public Trait GetSecondTrait()
    {
        return secondTrait;
    }
    
    public bool IsCatActive()
    {
        return isActive;
    }
    
    #endregion getters

    #region setters

    public void SetCatActive(bool active)
    {
        isActive = active;
    }

    #endregion setters
    
    
    public void DestroyCat()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Used to update the stats of a cat over given ammount of time.
    /// </summary>
    /// <param name="secondsElapsed">Seconds elapsed since last instance of play.</param>
    public void ApplyTimeElapsed(int secondsElapsed)
    {
        ApplyCatValueChanges(secondsElapsed, Entertainment, entertainmentProcTime, ref entertainmentTimer, ref _entertainment, maxEntertainment);
        OnEntertainmentUpdated?.Invoke(_entertainment);
        ApplyCatValueChanges(secondsElapsed, Fullness, hungerProcTime, ref hungerTimer, ref _fullness, maxFullness);
        OnHungerUpdated?.Invoke(_fullness);
    }
    
    private void ApplyCatValueChanges(int secondsElapsed, int previousCatStatValue, float statValueProcTime, ref float statValueTimer, ref int valueToModify, int maxStatValue)
    {
        //check if secondsElapsed is less than the proc times. If so then add that time to each respective timer.
        if (secondsElapsed < statValueProcTime)
        {
            statValueTimer += secondsElapsed;
            return;
        }
        //Check how many procs occur and apply changes. For each proc passed 0 reduce relationship value.
        else
        {
            int totalProcs = secondsElapsed / (int)statValueProcTime;
            int procsTillZero = previousCatStatValue;

            //if total procs wont affect relationship value
            if (totalProcs <= procsTillZero)
            {
                valueToModify -= totalProcs;
                Mathf.Clamp(valueToModify, 0, maxStatValue);
                return;
            }
            else
            {
                valueToModify = 0;
                totalProcs -= procsTillZero;
                _relationship = Mathf.Clamp(_relationship - totalProcs, 0, maxRelationship);
                OnRelationshipUpdated?.Invoke(_relationship);
            }
        }
    }
}

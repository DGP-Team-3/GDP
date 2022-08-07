using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private string catName;
    [SerializeField] private Trait firstTrait;
    [SerializeField] private Trait secondTrait;

    [Space]

    [Tooltip("Seconds till cat's fullness decreases.")]
    [Min(0f)]
    [SerializeField] private float hungerProcTime = 1f;

    [Tooltip("Seconds till cat's entertainment decreases.")]
    [Min(0f)]
    [SerializeField] private float entertainmentProcTime = 1f;

    [Tooltip("Amount to decrease relationship.")]
    [SerializeField] private int relationshipDecreaseAmount = 5;

    [Tooltip("Amount to increase relationship.")]
    [Min(0f)]
    [SerializeField] private int relationshipIncreaseAmount = 10;

    [Tooltip("Amount to increase fullness.")]
    [Min(0f)]
    [SerializeField] private int feedAmount = 85;

    [Tooltip("Amount to increase entertainment.")]
    [Min(0f)]
    [SerializeField] private int entertainAmount = 90;

    [Min(0f)]
    [SerializeField] private int maxFullness = 100;
    public float MaxFullness => maxFullness;

    [Min(0f)]
    [SerializeField] private int maxRelationship = 100;
    public float MaxRelationship => maxRelationship;

    [Min(0f)]
    [SerializeField] private int maxEntertainment = 100;
    public float MaxEntertainment => maxEntertainment;

    [Space]

    [SerializeField] private Animator animator;

    [SerializeField] public Sprite pettingGameSprite;



    private int _fullness;
    public int Fullness => _fullness;


    private int _relationship;
    public int Relationship => _relationship;


    private int _entertainment;
    public int Entertainment => _entertainment;


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



    //////////////////////////////////////////
    ///
    ///
    void Update()
    {
        if (IsCatActive() && !GameManager.Instance.IsMinigameActive)
        {
            HandleHunger();
            HandleEntertainment();
        }
    }

    //////////////////////////////////////////
    /// 
    /// Note: Called when fostering a generated cat or when populating when loading data
    ///
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

    //////////////////////////////////////////
    /// update hunger value
    ///
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

    //////////////////////////////////////////
    /// update entertainment value
    ///
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

    //////////////////////////////////////////
    /// update fullness value
    ///
    public void FeedCat()
    {
        _fullness = Mathf.Clamp(_fullness + feedAmount, 0, maxFullness);
        ModifyRelationship(relationshipIncreaseAmount);
        OnHungerUpdated?.Invoke(_fullness);
    }

    //////////////////////////////////////////
    /// update entertainment value
    ///
    public void EntertainCat()
    {
        _entertainment = Mathf.Clamp(_entertainment + entertainAmount, 0, maxEntertainment);
        ModifyRelationship(relationshipIncreaseAmount);
        OnEntertainmentUpdated?.Invoke(_entertainment);
    }

    //////////////////////////////////////////
    /// update relationship value
    ///
    public void ModifyRelationship(int value)
    {
        _relationship = Mathf.Clamp(value + _relationship, 0, maxRelationship);
        OnRelationshipUpdated?.Invoke(_relationship);
    }

    //////////////////////////////////////////
    /// 
    ///
    public string GetName()
    {
        return catName;
    }

    //////////////////////////////////////////
    /// 
    ///
    public Trait GetFirstTrait()
    {
        return firstTrait;
    }

    //////////////////////////////////////////
    /// 
    ///
    public Trait GetSecondTrait()
    {
        return secondTrait;
    }

    //////////////////////////////////////////
    /// 
    ///
    public void SetCatActive(bool active)
    {
        isActive = active;
    }


    //////////////////////////////////////////
    /// 
    ///
    public bool IsCatActive()
    {
        return isActive;
    }


    //////////////////////////////////////////
    /// Removes cat from game
    ///
    public void DestroyCat()
    {
        Destroy(gameObject);
    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private string catName;
    [SerializeField] private Sprite catPortrait;
    [SerializeField] private Trait firstTrait;
    [SerializeField] private Trait secondTrait;
    
    [Space]

    [Tooltip("Seconds till cat's fullness decreases.")]
    [Min(0f)]
    [SerializeField] private float hungerProcTime = 1f;    
    
    [Tooltip("Seconds till cat's entertainment decreases.")]
    [Min(0f)]
    [SerializeField] private float entertainmentProcTime = 1f;

    [Tooltip("Amount to decrease relationship when hunger or entertainment is 0.")]
    [Min(0f)]
    [SerializeField] private int relationshipDecreaseValue = 5;

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



    private int _fullness;
    public int Fullness => _fullness;


    private int _relationship;
    public int Relationship => _relationship;


    private int _entertainment;
    public int Entertainment => _entertainment;


    private float hungerTimer = 0f;
    private float entertainmentTimer = 0f;



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
        HandleHunger();
        HandleEntertainment();
    }

    //////////////////////////////////////////
    /// Called when fostering a generated cat
    ///
    public void InitCatValues(string name, Trait traitOne, Trait traitTwo, Sprite portrait, int relationshipMaxValue)
    {
        catName = name;
        firstTrait = traitOne;
        secondTrait = traitTwo;
        catPortrait = portrait;
        
        maxFullness = 100;
        maxEntertainment = 100;
        maxRelationship = relationshipMaxValue;


        _fullness = maxFullness;
        _relationship = 0;
        _entertainment = maxEntertainment;
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
                ModifyRelationship(relationshipDecreaseValue);
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
                ModifyRelationship(relationshipDecreaseValue);
            }
        }
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
    public Sprite GetPortrait()
    {
        return catPortrait;
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
}

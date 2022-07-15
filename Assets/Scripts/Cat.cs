using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [Tooltip("How many seconds till increaseing cat's hunger.")]
    [Min(0f)]
    [SerializeField] private float hungerProcTime = 1f;
    
    [SerializeField] private Sprite catPortrait;

    [Min(0f)]
    [SerializeField] private int maxFullness = 100;
    public float MaxFullness => maxFullness;

    [Min(0f)]
    [SerializeField] private int maxRelationship = 100;
    public float MaxRelationship => maxRelationship;

    [Min(0f)]
    [SerializeField] private int maxEntertainment = 100;
    public float MaxEntertainment => maxEntertainment;


    private int _fullness;
    public int Fullness => _fullness;


    private int _relationship;
    public int Relationship => _relationship;


    private int _entertainment;
    public int Entertainment => _entertainment;


    private float hungerTimer = 0f;



    public delegate void HungerUpdated(int fullness);
    public event HungerUpdated OnHungerUpdated;   
    
    public delegate void RelationshipUpdated(int relationship);
    public event RelationshipUpdated OnRelationshipUpdated;    
    
    public delegate void EntertainmentUpdated(int entertainment);
    public event EntertainmentUpdated OnEntertainmentUpdated;


    private void Awake()
    {
        _fullness = maxFullness;
        _relationship = maxRelationship / 2;
        _entertainment = maxEntertainment;
    }


    void Update()
    {
        HandleHunger();
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
        }
    }

    //////////////////////////////////////////
    /// update relationship value
    ///
    public void AddRelationship(int value)
    {
        _relationship = Mathf.Clamp(value + _relationship, 0, maxRelationship);
    }    
    
    //////////////////////////////////////////
    /// update entertainment value
    ///
    public void AddEntertainment(int value)
    {
        _entertainment = Mathf.Clamp(value + _entertainment, 0, maxEntertainment);
    }

    //////////////////////////////////////////
    /// 
    ///
    public Sprite GetPortrait()
    {
        return catPortrait;
    }
}

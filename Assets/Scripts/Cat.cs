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


    private int _fullness;
    public int Fullness => _fullness;


    private int _relationship;
    public int Relationship => _relationship;

    private int _entertainment;
    public int Entertainment => _entertainment;


    private float hungerTimer = 0f;

    private Trait[] traits;

    private SelectionManager selectionManager;



    public delegate void HungerUpdated(int fullness);
    public event HungerUpdated OnHungerUpdated;   
    
    public delegate void RelationshipUpdated(int relationship);
    public event RelationshipUpdated OnRelationshipUpdated;    
    
    public delegate void EntertainmentUpdated(int entertainment);
    public event EntertainmentUpdated OnEntertainmentUpdated;


    private void Awake()
    {
        _fullness = maxFullness;
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
    /// 
    ///
    public Sprite GetPortrait()
    {
        return catPortrait;
    }
}

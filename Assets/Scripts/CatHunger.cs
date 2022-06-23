using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CatHunger : MonoBehaviour
{
    [Tooltip("The cats max fullness value.")]
    [Min(0)]
    [SerializeField] private float maxCatFullness = 100f;

    [Tooltip("Amount to fill cat.")]
    [Min(0)]
    [SerializeField] private float feedAmount = 25f;

    [Tooltip("Amount hunger grows by.")]
    [Min(0)]
    [SerializeField] private float hungerAmount = 10f;

    [Tooltip("Amount of time in seconds till hunger grows.")]
    [Min(0)]
    [SerializeField] private float timeTillHungerGrows = 30f;

    [SerializeField] private CatRelationship relations;

    public UnityEvent hungerChanged;

    private float timeElapsedSinceHungerGrowth;
    
    private float _currentCatFullness;
    public float currentCatFullness => _currentCatFullness;

    private void Awake()
    {
        timeElapsedSinceHungerGrowth = 0f;
        _currentCatFullness = 0f;
    }

    void Update()
    {
        timeElapsedSinceHungerGrowth += Time.deltaTime;

        //Hunger grows
        if (timeElapsedSinceHungerGrowth >= timeTillHungerGrows)
        {
            print("Cat growing hungry!");
            timeElapsedSinceHungerGrowth = 0f;
            
            _currentCatFullness = Mathf.Clamp(_currentCatFullness - hungerAmount, 0, maxCatFullness);
            hungerChanged?.Invoke();

            //if cat is extremely hungry then decrease relations
            if (Mathf.Approximately(_currentCatFullness, 0))
            {
                relations.DecreaseRelations();
            }
        }
    }

    public void FeedCat()
    {
        if (!Mathf.Approximately(_currentCatFullness, maxCatFullness))
        {
            print("Cat fed!");
            _currentCatFullness = Mathf.Clamp(_currentCatFullness + feedAmount, 0, maxCatFullness);
            hungerChanged?.Invoke();
            relations.IncreaseRelations();
        }
    }
}

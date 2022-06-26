using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CatRelationship : MonoBehaviour
{

    [Min(0)]
    [SerializeField] private int maxCatRelationship = 100;
    
    [Min(0)]
    [SerializeField] private int increaseRelationsAmount = 8;
    
    [Min(0)]
    [SerializeField] private int decreaseRelationsAmount = 10;

    [Space]

    [SerializeField] private GameObject maxRelationshipIndicator;

    private int _currentRelations;
    public int currentRelations => _currentRelations;

    public UnityEvent relationsChanged;

    void Awake()
    {
        _currentRelations = 0;
    }

    public void IncreaseRelations()
    {
        _currentRelations = Mathf.Clamp(_currentRelations + increaseRelationsAmount, 0, maxCatRelationship);
        relationsChanged?.Invoke();

        if (_currentRelations == maxCatRelationship)
        {
            maxRelationshipIndicator.SetActive(true);
        }
    }

    public void DecreaseRelations()
    {
        _currentRelations = Mathf.Clamp(_currentRelations - decreaseRelationsAmount, 0, maxCatRelationship);
        relationsChanged?.Invoke();

        if (maxRelationshipIndicator.active)
        {
            maxRelationshipIndicator.SetActive(false);
        }
    }
}

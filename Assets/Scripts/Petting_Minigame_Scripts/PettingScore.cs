using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PettingScore : MonoBehaviour
{
    [Tooltip("The amount of points gained when succesfully tapping a circle.")]
    [Min(0)]
    [SerializeField] private float pointAmount;



    private float _score;
    public float score => _score;

    public UnityEvent scoreChanged;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AwardPoints()
    {
        print("Award points!" + _score);
        _score = _score + pointAmount;
        scoreChanged?.Invoke();
    }
}

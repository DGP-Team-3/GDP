using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetCircle : MonoBehaviour
{
    PettingScore pettingScore;
    GameObject scoreManager;


    public UnityEvent circleClicked;


    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager");
        pettingScore = scoreManager.GetComponent<PettingScore>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        print("Circle clicked!");
        pettingScore.AwardPoints();
        //circleClicked?.Invoke();
        Destroy(this.gameObject);
    }
}

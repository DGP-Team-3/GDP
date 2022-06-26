using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTETouch : MonoBehaviour
{

    public float qteTimeRemaining = 3f;
    private bool qteTimerRunning = false;
    public Text qteTimer;
    public Text qteText;
    public Text scoreText;

    public float waitTimeRemaining = 5f;
    private bool waitTimerRunning = false;

    private int score = 0;

    void Start()
    {
        waitTimerRunning = true;
        scoreText.text = "Score: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (waitTimerRunning)
        {
            qteText.text = "Wait!";
            qteTimer.text = "";
            if (waitTimeRemaining > 0)
            {
                waitTimeRemaining -= Time.deltaTime;
            }

            else
            {
                waitTimerRunning = false;
                qteTimeRemaining = 3f;
                qteTimerRunning = true;
            }
        }

        else if (qteTimerRunning)
        {
            qteText.text = "Tap the square!";
            if (qteTimeRemaining > 0)
            {
                if (Input.touchCount > 0)
                {
                    score++;
                    qteTimerRunning = false;
                    waitTimeRemaining = 5f;
                    waitTimerRunning = true;
                    scoreText.text = "Score: " + score.ToString();
                }
                qteTimer.text = (Mathf.FloorToInt(qteTimeRemaining % 60)+1).ToString();
                qteTimeRemaining -= Time.deltaTime;
            }
            else
            {
                score--;
                qteTimerRunning = false;
                waitTimeRemaining = 5f;
                waitTimerRunning = true;
                scoreText.text = "Score: " + score.ToString();
            }
        }
    }
}

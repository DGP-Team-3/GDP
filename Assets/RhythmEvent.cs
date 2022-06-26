using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class RhythmEvent : MonoBehaviour
{
    public float qteTimeRemaining = 3f;
    private bool qteTimerRunning = false;
    public Text qteText;
    public Text scoreText;

    public float waitTimeRemaining = 5f;
    private bool waitTimerRunning = false;

    private int score = 0;

    public Image outerSquare;

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
            qteText.text = "Tap the circle!";
            if (qteTimeRemaining > 0)
            {
                if (Input.touchCount > 0)
                {
                    if (qteTimeRemaining < 1.2 && qteTimeRemaining > 1.0)
                    {
                        score++;
                        qteTimeRemaining = 0;
                        qteTimerRunning = false;
                        waitTimeRemaining = 5f;
                        waitTimerRunning = true;
                        scoreText.text = "Score: " + score.ToString();
                    }
                    else
                    {
                        qteTimeRemaining = 0;
                    }
                }
                outerSquare.rectTransform.localScale = new Vector3(qteTimeRemaining/2, qteTimeRemaining/2, 1);
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

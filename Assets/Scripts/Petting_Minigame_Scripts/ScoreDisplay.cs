using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private GameObject scoreUIObject;

    [SerializeField] private PettingScore scoreObject;

    private TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = scoreUIObject.GetComponent<TMP_Text>();
    }

    public void UpdateFullnessText()
    {
        scoreText.text = "Score: " + scoreObject.score;
    }
}

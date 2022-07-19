using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PettingGameManager : MonoBehaviour
{
    [SerializeField] GameObject targetCirclePrefab;
    private Collider2D spawnArea;

    [Tooltip("The amount of points gained when succesfully tapping a circle.")]
    [Min(0)]
    [SerializeField] private float pointAmount;

    [SerializeField] private TMP_Text scoreText;

    private float _score;
    public float score => _score;

    // Start is called before the first frame update
    void Start()
    {
        spawnArea = GetComponent<Collider2D>();
        SpawnCircle();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CircleClicked()
    {
        AwardPoints();
        SpawnCircle();
    }

    void SpawnCircle()
    {
        var x_pos = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
        var y_pos = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
        GameObject target = Instantiate(targetCirclePrefab, new Vector3(x_pos, y_pos), Quaternion.identity);
        TargetCircleScript script = target.GetComponent<TargetCircleScript>();
        script.setOwner(this.gameObject);
    }
    public void AwardPoints()
    {
        print("Award points!" + _score);
        _score = _score + pointAmount;
        UpdateFullnessText();
    }

    public void UpdateFullnessText()
    {
        scoreText.text = "Score: " + _score;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PettingGameManager : MonoBehaviour
{
    [SerializeField] GameObject targetCirclePrefab;

    private Collider2D[] spawnAreas;
    private Collider2D spawnArea;

    [Tooltip("The amount of points gained when succesfully tapping a circle.")]
    [Min(0)]
    [SerializeField] private float pointGain;

    [Tooltip("The amount of points needed to win.")]
    [Min(0)]
    [SerializeField] private float pointWin;

    [SerializeField] private TMP_Text scoreText;

    [Min(0f)]
    [SerializeField] private float blackoutSpeed = 1f;

    [SerializeField] private GameObject blackout;
    [SerializeField] private GameObject greenCheck;


    
    private bool isBlackingOut = false;
    private float blackoutTVal = 0f;
    
    
    private float score;



    // Start is called before the first frame update
    void Start()
    {
        spawnAreas = GetComponents<Collider2D>();
        SpawnCircle();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBlackingOut)
        {
            blackoutTVal += Time.deltaTime * blackoutSpeed;
        }
    }

    public void CircleClicked()
    {
        AwardPoints();
        if (!CheckForWin())
        {
            SpawnCircle();
        }
    }

    void SpawnCircle()
    {
        var i = Random.Range(0, spawnAreas.Length);
        var x_pos = Random.Range(spawnAreas[i].bounds.min.x, spawnAreas[i].bounds.max.x);
        var y_pos = Random.Range(spawnAreas[i].bounds.min.y, spawnAreas[i].bounds.max.y);
        GameObject target = Instantiate(targetCirclePrefab, new Vector3(x_pos, y_pos), Quaternion.identity);
        TargetCircleScript script = target.GetComponent<TargetCircleScript>();
        script.setOwner(this.gameObject);
    }
    void AwardPoints()
    {
        score += pointGain;
        UpdateFullnessText();
    }

    void UpdateFullnessText()
    {
        scoreText.text = "Score: " + score;
    }

    bool CheckForWin()
    {
        if (score >= pointWin)
        {
            greenCheck.SetActive(true);
            GameManager.Instance.IncreaseSelectedCatEntertainment();
            StartCoroutine(Transition());
            return true;
        }
        return false;
    }

    //////////////////////////////////////////
    ///
    /// 
    private IEnumerator Transition()
    {
        yield return new WaitForSeconds(1f);
        isBlackingOut = true;
        SpriteRenderer spriteRenderer = blackout.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;

        while (spriteRenderer.color != Color.black)
        {

            spriteRenderer.color = Color.Lerp(originalColor, Color.black, blackoutTVal);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        GameManager.Instance.LoadMainScene();
    }
}


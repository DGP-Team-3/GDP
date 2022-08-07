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

    [SerializeField] private GameObject catSprite;
    [SerializeField] private GameObject blackout;
    [SerializeField] private GameObject greenCheck;
    [SerializeField] private GameObject progressBar;

    [Tooltip("List of happy cat icons.")]
    [SerializeField] List<GameObject> happyCatIcons;
    [Tooltip("List of mad cat icons.")]
    [SerializeField] List<GameObject> madCatIcons;

    private GameObject cat;

    
    private bool isBlackingOut = false;
    private bool isLosing = false;
    private float blackoutTVal = 0f;
    
    
    private float score;
    private int strikes = 0;


    // Start is called before the first frame update
    void Start()
    {
        progressBar.GetComponent<ProgressBar>().SetMax(pointWin);
        cat = Instantiate(GameManager.Instance.GetSelectedCat().gameObject, new Vector3 (0, 0, 0), Quaternion.identity);
        cat.GetComponent<CatAI>().selectCat();
        cat.transform.localScale = new Vector3(16, 16, 1);
        cat.GetComponent<CatAI>().GetSprite().flipX = false;
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

    public void CircleExpired()
    {
        happyCatIcons[strikes].SetActive(false);
        madCatIcons[strikes].SetActive(true);
        strikes++;
        if (strikes >= 3)
        {
            cat.GetComponent<CatAI>().PlayLose();
            isLosing = true;
            StartCoroutine(LoseTransition());
            StartCoroutine(MoveCatOffScreen(cat.transform, new Vector3(-13, 0, 0), 2.0f));
            return;
        }
        SpawnCircle();
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
        progressBar.GetComponent<ProgressBar>().UpdateFill(score);
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
            cat.GetComponent<CatAI>().PlayWin();
            GameManager.Instance.IncreaseSelectedCatEntertainment();
            StartCoroutine(WinTransition());
            return true;
        }
        return false;
    }

    //////////////////////////////////////////
    ///
    /// 
    private IEnumerator WinTransition()
    {
        yield return new WaitForSeconds(2f);
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
    private IEnumerator LoseTransition()
    {
        yield return new WaitForSeconds(2f);
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

    private IEnumerator MoveCatOffScreen(Transform transform, Vector3 position, float time)
    {
        yield return new WaitForSeconds(0.5f);
        var startingPos = transform.position;
        var t = 0.0f;
        while (t < 1)
        {
            t += Time.deltaTime / time;
            transform.position = Vector3.Lerp(startingPos, position, t);
            yield return null;
        }
    }
}


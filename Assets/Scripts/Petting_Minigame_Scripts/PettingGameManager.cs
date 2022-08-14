using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PettingGameManager : MonoBehaviour
{
    [SerializeField] GameObject targetCirclePrefab;

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

    [Header("Audio")]
    [SerializeField] private AudioSource successSFX;
    [SerializeField] private AudioSource failureSFX;
    [SerializeField] private AudioSource pettingSFX;

    private Collider2D[] spawnAreas;
    
    private GameObject cat;
    private CatAI catAI;
    
    private bool isBlackingOut = false;
    private float blackoutTVal = 0f;
    
    private float score;
    private int strikes = 0;


    void Start()
    {
        progressBar.GetComponent<ProgressBar>().SetMax(pointWin);
        cat = Instantiate(GameManager.Instance.GetSelectedCat().gameObject, new Vector3 (0, 0, 0), Quaternion.identity);
        catAI = cat.GetComponent<CatAI>();

        catAI.selectCat();
        cat.transform.localScale = new Vector3(16, 16, 1);
        catAI.GetSprite().flipX = false;

        spawnAreas = GetComponents<Collider2D>();
        SpawnCircle();
    }


    void Update()
    {
        if (isBlackingOut)
        {
            blackoutTVal += Time.deltaTime * blackoutSpeed;
        }
    }

    public void CircleClicked()
    {
        pettingSFX.Play();
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
            catAI.PlayLose();
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
        GameObject target = Instantiate(targetCirclePrefab, new Vector3(x_pos, y_pos, -1), Quaternion.identity);
        TargetCircleScript script = target.GetComponent<TargetCircleScript>();
        script.setOwner(gameObject);
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
            catAI.PlayWin();
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
        greenCheck.SetActive(true);
        successSFX.Play();
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
        failureSFX.Play();
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


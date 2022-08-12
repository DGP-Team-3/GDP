using UnityEngine;

public class TargetCircleScript : MonoBehaviour
{
    [Tooltip("Time it takes to disappear.")]
    [Min(0.1f)]
    [SerializeField] private float totalTime;
    private float timer;
    private SpriteRenderer mRenderer;

    GameObject _owner;

    private void Start()
    {
        timer = totalTime;
        mRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        mRenderer.material.SetFloat("_Arc1", (360 - ((timer / totalTime) * 360)));
        if (timer <= 0.0f)
        {
            TimedOut();
        }
    }

    public void setOwner(GameObject owner)
    {
        _owner = owner;
    }

    private void OnMouseDown()
    {
        if (_owner == null) return;
        _owner.GetComponent<PettingGameManager>().CircleClicked();
        Destroy(gameObject);
    }

    private void TimedOut()
    {
        if (_owner == null) return;
        _owner.GetComponent<PettingGameManager>().CircleExpired();
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetCircleScript : MonoBehaviour
{
    [Tooltip("Time it takes to disappear.")]
    [Min(0.1f)]
    [SerializeField] private float time;
    GameObject _owner;

    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0.0f)
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

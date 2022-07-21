using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetCircleScript : MonoBehaviour
{
    GameObject _owner;

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
}

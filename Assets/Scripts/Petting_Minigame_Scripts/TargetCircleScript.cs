using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetCircleScript : MonoBehaviour
{
    GameObject _owner;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setOwner(GameObject owner)
    {
        _owner = owner;
    }

    private void OnMouseDown()
    {
        print("Circle clicked!");
        _owner.GetComponent<PettingGameManager>().CircleClicked();
        Destroy(this.gameObject);
    }
}

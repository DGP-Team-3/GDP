using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discover : MonoBehaviour
{
    [SerializeField] private List<GameObject> containers;
    [SerializeField] private CatData catData;

    [SerializeField] private float timeTillCatReshuffle = 30f;

    private float reshuffleTimeElapsed = 0f;

    //////////////////////////////////////////
    ///
    ///
    private void Update()
    {
        HandleCatReshuffle();
    }


    //////////////////////////////////////////
    ///
    ///
    private void HandleCatReshuffle()
    {
        reshuffleTimeElapsed += Time.deltaTime;

        if (reshuffleTimeElapsed >= timeTillCatReshuffle)
        {
            reshuffleTimeElapsed = 0f;
            
            foreach (GameObject container in containers)
            {
                GenerateNewCatDisplay(container);
            }
        }
    }


    //////////////////////////////////////////
    ///
    ///
    private void GenerateNewCatDisplay(GameObject container)
    {
        //Pick random cat type
        //take container and get children objects that need to be updated
        //name
        //portrait
        //traits
    }


    //////////////////////////////////////////
    ///
    ///
    public void FosterCat(int containerIndex)
    {

    }
}

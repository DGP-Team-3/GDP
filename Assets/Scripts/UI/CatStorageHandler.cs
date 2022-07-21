using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStorageHandler : MonoBehaviour
{

    [SerializeField] private GameObject catStoragePrefab;
    [SerializeField] private GameObject containerParentObject;
    
    private List<GameObject> catStorageContainters = new List<GameObject>();


    private void Start()
    {
        RefreshStorage(GameManager.Instance.GetCatList());
    }


    //////////////////////////////////////////
    /// Adds a container for the given cat
    ///
    public void AddContainer(GameObject cat)
    {
        GameObject container = Instantiate(catStoragePrefab);
        container.transform.SetParent(containerParentObject.transform);

        catStorageContainters.Add(container);

        container.GetComponent<CatStorageContainer>().SetupDisplay(cat);
    }


    //////////////////////////////////////////
    /// Takes a cat game object and removes its associated container from list and the game
    ///
    public void DestroyContainer(GameObject catGO)
    {
        for (int i = 0; i < catStorageContainters.Count; i++)
        {
            if (catGO == catStorageContainters[i].GetComponent<CatStorageContainer>().GetAssociatedCat())
            {
                GameObject container = catStorageContainters[i];
                catStorageContainters.RemoveAt(i);
                Destroy(container);
            }
        }
    }


    //////////////////////////////////////////
    /// Deletes current container listing and generates new one
    ///
    private void RefreshStorage(List<GameObject> cats)
    {
        DeleteCurrentStorage();

        foreach (GameObject cat in cats)
        {
            AddContainer(cat);
        }
    }

    //////////////////////////////////////////
    /// Clears entire container list and gameobject
    ///
    private void DeleteCurrentStorage()
    {
        foreach (GameObject container in catStorageContainters)
        {
            Destroy(container);
        }
        catStorageContainters.Clear();
    }

}

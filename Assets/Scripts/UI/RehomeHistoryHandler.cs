using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RehomeHistoryHandler : MonoBehaviour
{
    [SerializeField] private GameObject rehomeHistoryPrefab;
    [SerializeField] private GameObject containerParentObject;

    [Header("Assets")]
    [Tooltip("Make sure this is identical to the list in RehomeManager")]
    [SerializeField] private List<Sprite> ownerPortraits;
    //this is just to get the cat portraits
    [SerializeField] private CatData catData;

    private List<GameObject> rehomeHistoryContainers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.AddToClearList(gameObject);

    }

    public void AddRehomeHistoryContainer(string catName, CatType catType, string ownerName, int ownerIndex)
    {

        GameObject container = Instantiate(rehomeHistoryPrefab);
        container.transform.SetParent(containerParentObject.transform);

        rehomeHistoryContainers.Add(container);

        RehomeHistoryContainer rehomeHistoryContainerScript = container.GetComponent<RehomeHistoryContainer>();
        rehomeHistoryContainerScript.SetupDisplay(catName, catType, ownerName, ownerIndex);
        rehomeHistoryContainerScript.GetCatPortraitField().sprite = catData.GetCatPortrait(catType);
        rehomeHistoryContainerScript.GetOwnerPortraitField().sprite = ownerPortraits[ownerIndex];
    }

    public List<GameObject> GetRehomeHistoryContainers()
    {
        return rehomeHistoryContainers;
    }
}
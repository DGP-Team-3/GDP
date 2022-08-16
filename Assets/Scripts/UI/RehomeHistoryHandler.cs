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

        RefreshRehomeHistory(GameManager.Instance.GetRehomeRecords());
    }

    public void AddRehomeHistoryContainer(GameObject rehomeRecordGO)
    {
        RehomeRecord record = rehomeRecordGO.GetComponent<RehomeRecord>();
        GameObject container = Instantiate(rehomeHistoryPrefab);
        container.transform.SetParent(containerParentObject.transform);

        rehomeHistoryContainers.Add(container);
        container.transform.localScale = new Vector3(1.0707f, 1.0707f, 1.0707f);

        RehomeHistoryContainer rehomeHistoryContainerScript = container.GetComponent<RehomeHistoryContainer>();
        rehomeHistoryContainerScript.SetupDisplay(record.GetCatName(), record.GetOwnerName());
        rehomeHistoryContainerScript.GetCatPortraitField().sprite = catData.GetCatPortrait(record.GetCatType());
        rehomeHistoryContainerScript.GetOwnerPortraitField().sprite = ownerPortraits[record.GetOwnerIndex()];
    }

    public void RefreshRehomeHistory(List<GameObject> records)
    {
        foreach (GameObject container in rehomeHistoryContainers)
        {
            Destroy(container);
        }
        rehomeHistoryContainers.Clear();
        foreach(GameObject record in records)
        {
            AddRehomeHistoryContainer(record);
        }
    }
}

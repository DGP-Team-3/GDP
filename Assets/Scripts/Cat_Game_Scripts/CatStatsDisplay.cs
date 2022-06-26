using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CatStatsDisplay : MonoBehaviour
{
    [SerializeField] private GameObject relationsUIObject;
    [SerializeField] private GameObject fullnessUIObject;

    [SerializeField] private CatRelationship relationship;
    [SerializeField] private CatHunger hunger;

    private TMP_Text relationsText;
    private TMP_Text fullnessText;


    // Start is called before the first frame update
    void Start()
    {
        relationsText = relationsUIObject.GetComponent<TMP_Text>();
        fullnessText = fullnessUIObject.GetComponent<TMP_Text>();

        UpdateRelationshipText();
        UpdateFullnessText();
    }

    public void UpdateRelationshipText()
    {
        relationsText.text = "Relationship: " + relationship.currentRelations;
    }    
    
    public void UpdateFullnessText()
    {
        fullnessText.text = "Fullness: " + hunger.currentCatFullness;
    }
}

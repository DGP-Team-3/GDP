using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatPopUpHandler : MonoBehaviour
{
    [SerializeField] private Image catFullnessBar;
    [SerializeField] private Image catRelationshipImage;
    [SerializeField] private Image catEntertainmentBar;


    private Cat currentCat;


    //////////////////////////////////////////
    /// 
    ///
    void OnDisable()
    {
        Unsubscribe();
    }

    //////////////////////////////////////////
    /// Give new cat to update images and values
    ///
    public void AssignCat(Cat newCat)
    {
        Unsubscribe();

        currentCat = newCat;
        ResetDisplay();

        Subscribe();
    }


    //////////////////////////////////////////
    /// Subscribe to given cat 
    ///
    private void Subscribe()
    {
        if (currentCat == null) return;

        currentCat.OnHungerUpdated += UpdateHungerBar;
        currentCat.OnEntertainmentUpdated += UpdateEntertainmentBar;
        currentCat.OnRelationshipUpdated += UpdateRelationshipBar;
    }

    //////////////////////////////////////////
    /// Unsubscribe from current cat 
    ///
    private void Unsubscribe()
    {
        if (currentCat == null) return;

        currentCat.OnHungerUpdated -= UpdateHungerBar;
        currentCat.OnEntertainmentUpdated -= UpdateEntertainmentBar;
        currentCat.OnRelationshipUpdated -= UpdateRelationshipBar;
    }


    //////////////////////////////////////////
    ///
    ///
    private void ResetDisplay()
    {
        if (currentCat == null) return;

        float fullnessPercent = currentCat.Fullness / currentCat.MaxFullness;
        catFullnessBar.fillAmount = Mathf.Clamp(fullnessPercent, 0, 1f);

        float relationshipPercent = currentCat.Relationship / 100f;
        catRelationshipImage.fillAmount = Mathf.Clamp(relationshipPercent, 0, 1f);

        float entertainmentPercent = currentCat.Entertainment / 100f;
        catEntertainmentBar.fillAmount = Mathf.Clamp(entertainmentPercent, 0, 1f);
    }

    //////////////////////////////////////////
    ///
    ///
    private void UpdateHungerBar(int fullness)
    {
        if (currentCat == null) return;
        
        float fullnessPercent = fullness / currentCat.MaxFullness;
        catFullnessBar.fillAmount = Mathf.Clamp(fullnessPercent, 0, 1f);
    }

    //////////////////////////////////////////
    ///
    ///
    private void UpdateRelationshipBar(int relationship)
    {
        if (currentCat == null) return;

        float relationshipPercent = relationship / 100f;
        catRelationshipImage.fillAmount = Mathf.Clamp(relationshipPercent, 0, 1f);
    }

    //////////////////////////////////////////
    ///
    ///
    private void UpdateEntertainmentBar(int entertainment)
    {
        if (currentCat == null) return;

        float entertainmentPercent = entertainment / 100f;
        catEntertainmentBar.fillAmount = Mathf.Clamp(entertainmentPercent, 0, 1f);
    }
}

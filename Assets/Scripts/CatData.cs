using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//note for polish: keep basic cat types at start. Have int for num of basic cats. put smaller chance for unique cats.
public enum CatType
{
    loafCat,
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CatData", order = 1)]
public class CatData : ScriptableObject
{
    [Header("Cats")]
    [SerializeField] private List<GameObject> catPrefabs;
    [SerializeField] private List<int> relationshipValues;

    [Header("Sprites")]
    [SerializeField] private List<Sprite> catRelationshipImages;
    [SerializeField] private List<Sprite> catPortraits;

    [Header("Names")]
    [SerializeField] private List<string> catNames;
    [SerializeField] private List<string> uniqueCatNames;



    //////////////////////////////////////////
    ///
    ///
    public Sprite GetCatPortrait(CatType cat)
    {
        return catPortraits[(int)cat];
    }

    //////////////////////////////////////////
    ///
    ///
    public string PickRandomName()
    {
        int index = UnityEngine.Random.Range(0, catNames.Count);
        return catNames[index];
    }

    //////////////////////////////////////////
    ///
    ///
    public string GetUniqueCatName(CatType cat)
    {
        return uniqueCatNames[(int)cat];
    }

    //////////////////////////////////////////
    ///
    ///
    public int GetCatRelationshipValue(CatType cat)
    {
        return relationshipValues[(int)cat];
    }

    //////////////////////////////////////////
    ///
    ///
    public Trait GetRandomTrait()
    {
        int index = UnityEngine.Random.Range(0, Enum.GetValues(typeof(Trait)).Length);
        return (Trait)index;
    }

    public GameObject GetCatPrefab(CatType cat)
    {
        return catPrefabs[(int)cat];
    }

    public List<Sprite> GetRelationshipImages()
    {
        return catRelationshipImages;
    }
}

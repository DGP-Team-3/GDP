using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//note for polish: keep basic cat types at start and unique cats at end of enum.
//Have int for num of basic cats. put smaller chance for unique cats.
public enum CatType
{
    loafCat,
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CatData", order = 1)]
public class CatData : ScriptableObject
{
    [Header("Cats")]
    [Min(0)]
    [SerializeField] private int numUniqueCatTypes = 0;
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
    public string GetRandomName()
    {
        int index = UnityEngine.Random.Range(0, catNames.Count);
        return catNames[index];
    }

    //////////////////////////////////////////
    ///
    ///
    public string GetUniqueCatName(CatType cat)
    {
        if (!IsUniqueCatType(cat)) return null;

        int numCatTypes = Enum.GetValues(typeof(CatType)).Length;
        int numCommonCatTypes = numCatTypes - numUniqueCatTypes;

        int index = numCommonCatTypes - (int)cat;

        return uniqueCatNames[index];
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

    //////////////////////////////////////////
    ///
    ///
    public GameObject GetCatPrefab(CatType cat)
    {
        return catPrefabs[(int)cat];
    }

    //////////////////////////////////////////
    ///
    ///
    public List<Sprite> GetRelationshipImages()
    {
        return catRelationshipImages;
    }

    //////////////////////////////////////////
    /// Checks if given cat type is a unique cat type
    ///
    public bool IsUniqueCatType(CatType catType)
    {
        int numCatTypes = Enum.GetValues(typeof(CatType)).Length;
        int numCommonCatTypes = numCatTypes - numUniqueCatTypes;
        
        if ((int)catType < numCommonCatTypes)
        {
            return false;
        }
        return true;
    }
}

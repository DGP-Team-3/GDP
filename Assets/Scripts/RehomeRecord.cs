using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RehomeRecord : MonoBehaviour
{
    private string catName;
    private CatType catType;
    private string ownerName;
    private int ownerIndex;

    public void InitRehomeRecord(string catName, CatType catType, string ownerName, int ownerIndex)
    {
        this.catName = catName;
        this.catType = catType;
        this.ownerName = ownerName;
        this.ownerIndex = ownerIndex;
    }
    public string GetCatName()
    {
        return catName;
    }

    public CatType GetCatType()
    {
        return catType;
    }
    
    public string GetOwnerName()
    {
        return ownerName;
    }

    public int GetOwnerIndex()
    {
        return ownerIndex;
    }
}

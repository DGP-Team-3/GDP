using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int numCats;
    public int[] catTypes;
    public int[] firstTraits;
    public int[] secondTraits;
    public int[] relationshipValues;
    public int[] fullnessValues;
    public int[] entertainmentValues;
    public string[] names;
    public bool[] activeStates;



    //////////////////////////////////////////
    /// Class Constructor
    ///
    public PlayerData(int numCats, string[] names, int[] catTypes, int[] firstTraits, int[] secondTraits, 
        int[] relationshipValues, int[] fullnessValues, int[] entertainmentValues, bool[] activeStates)
    {
        this.numCats = numCats;
        
        this.names = new string[numCats];
        this.catTypes = new int[numCats];
        this.firstTraits = new int[numCats];
        this.secondTraits = new int[numCats];
        this.relationshipValues = new int[numCats];
        this.fullnessValues = new int[numCats];
        this.entertainmentValues = new int[numCats];
        this.activeStates = new bool[numCats];
        
        for (int i = 0; i < numCats; i++)
        {
            this.names[i] = names[i];
            this.catTypes[i] = catTypes[i];
            this.firstTraits[i] = firstTraits[i];
            this.secondTraits[i] = secondTraits[i]; 
            this.relationshipValues[i] = relationshipValues[i];
            this.fullnessValues[i] = fullnessValues[i];
            this.entertainmentValues[i] = entertainmentValues[i];
            this.activeStates[i] = activeStates[i];
        }
        
    }
}

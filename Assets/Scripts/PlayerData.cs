using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int numCats;
    public int numNormalCatsFound;
    public int numSpecialCatsFound;
    public int numCatsRehomed;
    public int[] catTypes;
    public int[] firstTraits;
    public int[] secondTraits;
    public int[] relationshipValues;
    public int[] fullnessValues;
    public int[] entertainmentValues;
    public string[] names;
    public bool[] activeStates;
    public string[] rehomedCatNames;
    public int[] rehomedCatTypes;
    public string[] ownerNames;
    public int[] ownerIndexes;
    public int[] availableUniqueCats;


    //////////////////////////////////////////
    /// Class Constructor
    ///
    public PlayerData(int numCats, string[] names, int[] catTypes, int[] firstTraits, int[] secondTraits, 
        int[] relationshipValues, int[] fullnessValues, int[] entertainmentValues, bool[] activeStates, int numNormalCatsFound, int numSpecialCatsFound, int numCatsRehomed,
        string[] rehomedCatNames, int[] rehomedCatTypes, string[] ownerNames, int[] ownerIndexes, List<CatType> availableUniqueCats)
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
        this.availableUniqueCats = new int[availableUniqueCats.Count];


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

        this.numNormalCatsFound = numNormalCatsFound;
        this.numSpecialCatsFound = numSpecialCatsFound;
        this.numCatsRehomed = numCatsRehomed;

        this.rehomedCatNames = new string[numCatsRehomed];
        this.rehomedCatTypes = new int[numCatsRehomed];
        this.ownerNames = new string[numCatsRehomed];
        this.ownerIndexes = new int[numCatsRehomed];

        for (int j = 0; j < numCatsRehomed; j++)
        {
            this.rehomedCatNames[j] = rehomedCatNames[j];
            this.rehomedCatTypes[j] = rehomedCatTypes[j];
            this.ownerNames[j] = ownerNames[j];
            this.ownerIndexes[j] = ownerIndexes[j];
        }

        for (int k = 0; k < availableUniqueCats.Count; k++)
        {
            this.availableUniqueCats[k] = (int)availableUniqueCats[k];
        }
    }
}

using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{


    //////////////////////////////////////////
    /// Save player data to disk
    ///
    public static void SavePlayerData(int numCats, string[] names, int[] catTypes, int[] firstTraits, int[] secondTraits, 
        int[] relationshipValues, int[] fullnessValues, int[] entertainmentValues, bool[] activeStates, int numNormalCatsFound, int numSpecialCatsFound, int numCatsRehomed,
        string[] rehomedCatNames, int[] rehomedCatTypes, string[] ownerNames, int[] ownerIndexes, List<CatType> availableUniqueCats)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        string path = GetPath();
        
        FileStream stream = new FileStream(path, FileMode.Create);
        
        PlayerData data = new PlayerData(numCats, names, catTypes, firstTraits, secondTraits, 
            relationshipValues, fullnessValues, entertainmentValues, activeStates, numNormalCatsFound, numSpecialCatsFound, numCatsRehomed,
            rehomedCatNames, rehomedCatTypes, ownerNames, ownerIndexes, availableUniqueCats);

        formatter.Serialize(stream, data);
        
        stream.Close();
    }

    //////////////////////////////////////////
    /// Loads player data from disk
    ///
    public static PlayerData LoadPlayerData()
    {
        string path = GetPath();

        if (DoesSaveDataExist())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }


    //////////////////////////////////////////
    /// Deletes player data from disk
    ///
    public static void DeletePlayerData()
    {
        if (DoesSaveDataExist())
        {
            File.Delete(GetPath());
        }
    }

    //////////////////////////////////////////
    ///
    ///
    public static bool DoesSaveDataExist()
    {
        return File.Exists(GetPath());
    }

    //////////////////////////////////////////
    ///
    ///
    private static string GetPath()
    {
        return Application.persistentDataPath + "/playerdata.cats";

    }
}

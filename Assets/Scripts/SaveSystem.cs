using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{


    //////////////////////////////////////////
    /// Save player data to disk
    ///
    public static void SavePlayerData(int numCats, string[] names, int[] catTypes, int[] firstTraits, int[] secondTraits, 
        int[] relationshipValues, int[] fullnessValues, int[] entertainmentValues, bool[] activeStates, int numNormalCatsFound, int numSpecialCatsFound, int numCatsRehomed)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        string path = GetPath();
        
        FileStream stream = new FileStream(path, FileMode.Create);
        
        PlayerData data = new PlayerData(numCats, names, catTypes, firstTraits, secondTraits, 
            relationshipValues, fullnessValues, entertainmentValues, activeStates, numNormalCatsFound, numSpecialCatsFound, numCatsRehomed);

        formatter.Serialize(stream, data);
        
        stream.Close();

        Debug.Log("Data Saved");
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

            Debug.Log("Save file loaded in " + GetPath());

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + GetPath());
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
            Debug.Log("Saved data deleted.");
        }
        else
        {
            Debug.LogError("No existing data found in " + GetPath());
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

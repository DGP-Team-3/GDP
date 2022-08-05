using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class DataEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Delete Saved Data"))
        {
            Debug.Log("Deleting saved data");
            SaveSystem.DeletePlayerData();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnArea", order = 1)]
public class SpawnArea : ScriptableObject
{
    [SerializeField] private float xMinSpawn = -7;
    public float XMinSpawn => xMinSpawn;

    [SerializeField] private float xMaxSpawn = 7;
    public float XMaxSpawn => xMaxSpawn;

    [SerializeField] private float yMinSpawn = -4;
    public float YMinSpawn => yMinSpawn;

    [SerializeField] private float yMaxSpawn = 1;
    public float YMaxSpawn => yMaxSpawn;


}

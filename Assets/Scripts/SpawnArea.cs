using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [SerializeField] private float xMinSpawn;
    public float XMinSpawn => xMinSpawn;

    [SerializeField] private float xMaxSpawn;
    public float XMaxSpawn => xMaxSpawn;

    [SerializeField] private float yMinSpawn;
    public float YMinSpawn => yMinSpawn;

    [SerializeField] private float yMaxSpawn;
    public float YMaxSpawn => yMaxSpawn;


}

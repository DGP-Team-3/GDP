using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DexData", menuName = "ScriptableObjects/CatDexData", order = 2)]
public class CatDexData : ScriptableObject
{
    [Header("CatDex")]
    [SerializeField] private List<string> milestone1;
    [SerializeField] private List<string> milestone2;
    [SerializeField] private List<string> milestone3;
    [SerializeField] private List<string> milestone4;

    public string GetMilestoneText(CatType cat, string catName, int milestone)
    {
        switch(milestone)
        {
            case 0:
                if ((int)cat < 5)
                {
                    return catName + milestone1[Random.Range(0, 2)];
                }
                else
                {
                    return milestone1[(int)cat-2];
                }
            case 1:
                if ((int)cat < 5)
                {
                    return catName + milestone2[Random.Range(0, 2)];
                }
                else
                {
                    return milestone2[(int)cat-2];
                }
            case 2:
                if ((int)cat < 5)
                {
                    return catName + milestone3[Random.Range(0, 2)];
                }
                else
                {
                    return milestone3[(int)cat-2];
                }
            case 3:
                if ((int)cat < 5)
                {
                    return catName + milestone3[Random.Range(0, 2)];
                }
                else
                {
                    return milestone3[(int)cat-2];
                }
            default:
                if ((int)cat < 5)
                {
                    return catName + milestone4[Random.Range(0, 2)];
                }
                else
                {
                    return milestone4[(int)cat-2];
                }
        }
    }
}

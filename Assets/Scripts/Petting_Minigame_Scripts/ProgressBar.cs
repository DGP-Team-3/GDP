using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private float maximum = 100;
    [SerializeField] private Image fill;

    public void UpdateFill(float newAmount)
    {
        fill.fillAmount = newAmount / maximum;
    }

    public void SetMax(float max)
    {
        maximum = max;
    }
}

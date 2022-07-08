using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodType
{
    Shrimp = 0,
    Beef = 1,
    Chicken = 2,
}

public class Food : MonoBehaviour
{
    [SerializeField] private FoodType foodType;
    public FoodType GetFoodType => foodType;
}

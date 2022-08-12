using UnityEngine;

public enum FoodType
{
    Shrimp,
    Beef,
    Chicken
}

public class Food : MonoBehaviour
{
    [SerializeField] private FoodType foodType;
    public FoodType GetFoodType => foodType;
}

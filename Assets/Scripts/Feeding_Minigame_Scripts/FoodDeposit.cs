using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDeposit : MonoBehaviour
{
    [Tooltip("The renderer for thought bubbles.")]
    [SerializeField] private SpriteRenderer bubbleSpriteRenderer;

    [Header("Sprite Assets")]
    [SerializeField] private Sprite chickenSprite;
    [SerializeField] private Sprite shrimpSprite;
    [SerializeField] private Sprite beefSprite;
    [SerializeField] private Sprite happySprite;
    [SerializeField] private Sprite madSprite;


    //////////////////////////////////////////
    /// Sets the bubble renderer with a food sprite
    /// 
    public void SetFoodSprite(FoodType foodType)
    {
        if (foodType == FoodType.Shrimp)
        {
            bubbleSpriteRenderer.sprite = shrimpSprite;
        }
        if (foodType == FoodType.Beef)
        {
            bubbleSpriteRenderer.sprite = beefSprite;
        }
        if (foodType == FoodType.Chicken)
        {
            bubbleSpriteRenderer.sprite = chickenSprite;
        }
    }

    //////////////////////////////////////////
    /// Sets the bubble renderer with an emotion sprite
    /// 
    public void SetExpressionSprite(bool isRightFood)
    {
        if (isRightFood)
        {
            bubbleSpriteRenderer.sprite = happySprite;
        }
        else
        {
            bubbleSpriteRenderer.sprite = madSprite;
        }
    }
}

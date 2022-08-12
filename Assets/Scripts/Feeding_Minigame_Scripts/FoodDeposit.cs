using UnityEngine;

public class FoodDeposit : MonoBehaviour
{
    [Tooltip("The renderer for thought bubbles.")]
    [SerializeField] private SpriteRenderer bubbleSpriteRenderer;
    [SerializeField] private Animator mAnimator;
    [SerializeField] private Animation mAnim;

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
            mAnimator.Play("food_think_shrimp");
        }
        if (foodType == FoodType.Beef)
        {
            mAnimator.Play("food_think_meat");
        }
        if (foodType == FoodType.Chicken)
        {
            mAnimator.Play("food_think_chicken");
        }
    }

    //////////////////////////////////////////
    /// Sets the bubble renderer with an emotion sprite
    /// 
    public void SetExpressionSprite(bool isRightFood, FoodType foodType)
    {
        if (isRightFood)
        {
            if (foodType == FoodType.Shrimp)
            {
                mAnimator.Play("food_eat_shrimp");
            }
            if (foodType == FoodType.Beef)
            {
                mAnimator.Play("food_eat_meat");
            }
            if (foodType == FoodType.Chicken)
            {
                mAnimator.Play("food_eat_chicken");
            }
        }
        else
        {
            if (foodType == FoodType.Shrimp)
            {
                mAnimator.Play("food_wrong_shrimp");
            }
            if (foodType == FoodType.Beef)
            {
                mAnimator.Play("food_wrong_meat");
            }
            if (foodType == FoodType.Chicken)
            {
                mAnimator.Play("food_wrong_chicken");
            }
        }
    }
}
using UnityEngine;

public class Draggable : MonoBehaviour
{
    [Tooltip("The amount of time to move from dropped pos to.")]
    [SerializeField] private float movementTime = 15f;

    [Tooltip("The name of the tag for a valid drop.")]
    [SerializeField] private string validDropTagName = "ValidDrop";

    [SerializeField] private Collider2D collider;

    private Vector3 returnPosition;

    private System.Nullable<Vector3> movementDestination;

    private bool _isDragging;
    public bool IsDragging => _isDragging;



    //////////////////////////////////////////
    ///
    ///
    private void Awake()
    {
        returnPosition = transform.position;
    }

    //////////////////////////////////////////
    ///
    ///
    private void FixedUpdate()
    {
        if (!movementDestination.HasValue) return;

        if (_isDragging)
        {
            movementDestination = null;
            return;
        }

        if (transform.position == movementDestination)
        {
            gameObject.layer = Layer.Default;
            movementDestination = null;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, movementDestination.Value, movementTime * Time.fixedDeltaTime);
        }
    }

    //////////////////////////////////////////
    ///
    /// 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(validDropTagName))
        {
            movementDestination = returnPosition;
            return;
        }

        //Correct item placed
        FoodType draggedFoodType = gameObject.GetComponent<Food>().GetFoodType;
        if (FeedingMinigameManager.Instance.CheckValidFood(draggedFoodType))
        {
            movementDestination = other.transform.position;
            FeedingMinigameManager.Instance.CorrectFoodDelivered(draggedFoodType);
        }
        else
        {
            movementDestination = returnPosition;
            FeedingMinigameManager.Instance.IncorrectFoodDelivered(draggedFoodType);
        }
    }



    //////////////////////////////////////////
    /// Set this and children gameobject layers
    /// 
    public void SetLayer(int layer)
    {
        Transform[] children = GetComponentsInChildren<Transform>();

        gameObject.layer = layer;

        foreach (Transform child in children)
        {
            child.gameObject.layer = layer;
        }
    }

    //////////////////////////////////////////
    /// 
    /// 
    public void SetDragging(bool val) 
    {
        _isDragging = val;
    }
}

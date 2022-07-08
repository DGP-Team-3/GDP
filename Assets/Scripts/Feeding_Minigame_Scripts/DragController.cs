using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragController : MonoBehaviour
{
    private static DragController _instance;
    public static DragController Instance { get { return _instance; } }


    private bool isDragActive = false;
    private Vector2 screenPos;
    private Vector3 worldPos;

    private Draggable currentDragged;
    public Draggable CurrentDragged => currentDragged;



    //////////////////////////////////////////
    ///
    ///
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    //////////////////////////////////////////
    ///
    ///
    private void Update()
    {
        //Handle Dropping
        if (isDragActive && (Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            Drop();
            return;
        }

        //Update position data
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            screenPos = new Vector2(mousePos.x, mousePos.y);
        }
        else if (Input.touchCount > 0)
        {
            screenPos = Input.GetTouch(0).position;
        }
        //invalid input type
        else
        {
            return;
        }


        worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        //Handle Dragging
        if(isDragActive)
        {
            Drag();
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (hit.collider == null) return;

            Draggable draggable = hit.transform.gameObject.GetComponent<Draggable>();
            if (draggable == null) return;

            currentDragged = draggable;
            InitDrag();
        }
    }

    //////////////////////////////////////////
    /// Initiate dragging
    ///
    private void InitDrag()
    {
        UpdateDragStatus(true);
    }

    //////////////////////////////////////////
    /// Drag the draggable object
    ///
    private void Drag()
    {
        currentDragged.transform.position = new Vector2(worldPos.x, worldPos.y);
    }

    //////////////////////////////////////////
    /// Drop draggable object
    ///
    private void Drop()
    {
        UpdateDragStatus(false);
    }

    //////////////////////////////////////////
    /// Update draggable object drag data
    ///
    void UpdateDragStatus(bool isDragging)
    {
        currentDragged.SetDragging(isDragging);
        isDragActive = isDragging;
        currentDragged.SetLayer((isDragging ? Layer.Dragging : Layer.Default));
    }
}

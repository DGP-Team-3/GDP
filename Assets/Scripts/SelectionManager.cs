using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{

    private static SelectionManager _instance;
    public static SelectionManager Instance { get { return _instance; } }
    

    [SerializeField] private GameObject catPopUpDisplay;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button storageButton;

    private Vector2 screenPos;
    private Vector3 worldPos;
    private CatPopUpHandler popupHandler;



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

        popupHandler = catPopUpDisplay.GetComponent<CatPopUpHandler>();
    }

    //////////////////////////////////////////
    ///
    ///
    private void Update()
    {
        HandleInput();
    }


    //////////////////////////////////////////
    /// Gathers input positions
    ///
    private void HandleInput()
    {
        //Update position data
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector2(mousePos.x, mousePos.y);

            HandleCatSelection(mousePos);
        }
        else if (Input.touchCount > 0)
        {
            screenPos = Input.GetTouch(0).position;

            HandleCatSelection(screenPos);
        }
    }

    //////////////////////////////////////////
    /// Checks for selecting a cat
    ///
    private void HandleCatSelection(Vector2 inputPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(inputPosition, Vector2.zero);

        if (hit.collider == null) return;

        Cat cat = hit.transform.gameObject.GetComponent<Cat>();

        if (cat == null)
        {
            DeselectCat();
            return;
        }

        SelectCat(cat);
    }


    //////////////////////////////////////////
    ///
    ///
    private void SelectCat(Cat newCat)
    {
        popupHandler.AssignCat(newCat);
        catPopUpDisplay.SetActive(true);
        HandleButtonActivity();
    }

    //////////////////////////////////////////
    ///
    ///
    public void DeselectCat()
    {
        popupHandler.AssignCat(null);
        catPopUpDisplay.SetActive(false);
        HandleButtonActivity();
    }

    //////////////////////////////////////////
    /// 
    ///
    private void HandleButtonActivity()
    {
        if (catPopUpDisplay.activeSelf)
        {
            homeButton.interactable = true;
            storageButton.interactable = false;
        } 
        else
        {
            homeButton.interactable = false;
            storageButton.interactable = true;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{

    private static SelectionManager _instance;
    public static SelectionManager Instance { get { return _instance; } }
    

    [SerializeField] private GameObject catPopUpDisplay;
    [SerializeField] private LayerMask collidableLayers;

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
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector2(mousePos.x, mousePos.y);

            HandleCatSelection(mousePos);
        }
        else if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 screenPos = Input.GetTouch(0).position;

            HandleCatSelection(screenPos);
        }
    }

    //////////////////////////////////////////
    /// Checks for selecting a cat
    ///
    private void HandleCatSelection(Vector2 inputPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(inputPosition, Vector2.zero, 10, collidableLayers);

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
    public void SelectCat(Cat newCat)
    {
        popupHandler.AssignCat(newCat);
        catPopUpDisplay.SetActive(true);
        GameManager.Instance.SetSelectedCat(newCat);
    }

    //////////////////////////////////////////
    ///
    ///
    public void DeselectCat()
    {
        popupHandler.AssignCat(null);
        catPopUpDisplay.SetActive(false);
        GameManager.Instance.SetSelectedCat(null);
    }

}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace MenuAsset
{
    /*
     * Code by Kristopher Kath
     */


    [RequireComponent(typeof(Image))]
    public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Tooltip("The page this button is associated to. For opening and closing the associated page.")]
        [SerializeField] private GameObject buttonPage;

        [Tooltip("The tab group this button belongs to.")]
        [SerializeField] private TabGroup tabGroup;

        [Tooltip("Image to be used on button.")]
        [SerializeField] private Image background;

        [Tooltip("Event for when button is selected.")]
        [SerializeField] private UnityEvent onTabSelected;
        [Tooltip("Event for when button is deselected.")]
        [SerializeField] private UnityEvent onTabDeselected;

        void Awake()
        {
            background = GetComponent<Image>();
            tabGroup.Subscribe(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            tabGroup.OnTabSelected(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            tabGroup.OnTabEnter(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tabGroup.OnTabExit(this);
        }

        //Event when button selected
        public void Select()
        {
            buttonPage.SetActive(true);
            onTabSelected?.Invoke();
        }

        //Event when button deselected
        public void Deselect()
        {
            buttonPage.SetActive(false);
            onTabDeselected?.Invoke();
        }

        //Sets the background sprite
        public void SetBackgroundSprite(Sprite s)
        {
            background.sprite = s;
        }
    }
}
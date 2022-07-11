using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject PanelMenu;

    public void ShowHideMenu()
    {
        if (PanelMenu)
        {
            Animator animator = PanelMenu.GetComponent<Animator>();
            if (animator)
            {
                bool isOpen = animator.GetBool("show");
                animator.SetBool("show", !isOpen);
            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace MenuAsset
{
    /*
     * Code by Kristopher Kath
     */


    public class TabGroup : MonoBehaviour
    {
        [SerializeField] private Sprite tabIdle;
        [SerializeField] private Sprite tabHover;
        [SerializeField] private Sprite tabActive;

        [Tooltip("The initial active button when page is opened. (Optional)")]
        [SerializeField] private TabButton initialActiveTab;

        private List<TabButton> tabButtons; //list of tab buttons subscribed to tab group
        private TabButton selectedTab;

        public void Start()
        {
            // Responsible for setting up initial open page
            if (initialActiveTab == null && tabButtons.Count > 0)
            {
                initialActiveTab = tabButtons[0];
            }

            selectedTab = initialActiveTab;

            if (initialActiveTab != null)
            {
                OnTabSelected(initialActiveTab);
            }
        }

        //Adds a button to the list of buttons
        public void Subscribe(TabButton button)
        {
            if (tabButtons == null)
            {
                tabButtons = new List<TabButton>();
            }

            tabButtons.Add(button);
        }

        //Event that occurs when mouse hovers over selected button
        public void OnTabEnter(TabButton button)
        {
            ResetTabs();
            if (selectedTab == null || button != selectedTab)
            {
                button.SetBackgroundSprite(tabHover);
            }
        }

        //Event when button is exited
        public void OnTabExit(TabButton button)
        {
            ResetTabs();
        }

        //Event when tab button is selected
        public void OnTabSelected(TabButton button)
        {
            if (selectedTab != null)
            {
                selectedTab.Deselect();
            }

            selectedTab = button;
            selectedTab.Select();

            ResetTabs();
            button.SetBackgroundSprite(tabActive);
        }

        //Sets the image of all the button to idle state
        public void ResetTabs()
        {
            foreach (TabButton button in tabButtons)
            {
                if (selectedTab != null && button == selectedTab) { continue; } //If this button is selected then do not set it to idle state (keep as selected state)
                button.SetBackgroundSprite(tabIdle);
            }
        }
    }
}
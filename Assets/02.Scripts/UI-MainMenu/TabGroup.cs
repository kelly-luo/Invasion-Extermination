/*TabGroup UI Custom Button controller 
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This class is a custom button class, it handles clicks but when a it is clicked
 * it stays clicked. It also handles animations 
 *
 * 
 * AUT University - 2020 - Yuki Liyanage
 * Used Tutorial: https://www.youtube.com/watch?v=211t6r12XPQ - Game Dev Guide
 * 
 * Revision History
 *  ~~~~~~~~~~~~~~~~
 *  21.04.2020 Creation date (Yuki)
 *  12.06.2020 Refactored, and removed uncessary code (Yuki)
 *  
 *  
 *  Unity support packages
 */
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;

    public Color tabIdle;
    public Color tabHover;
    public Color tabActive;

    public TabButton selectedTab;

    public PanelGroup panelGroup;

    public TabButton initial;

    [SerializeField] bool keyDown;

    public string controls;


    /*
     * Awake()
     *  ~~~~~~~~~~~~~~~~
     *  Reset tabs to initailize it
     */
    public void Awake()
    {
        ResetTabs();
    }
    /*
     * Subscribe()
     *  ~~~~~~~~~~~~~~~~
     * This method attachs a tab button object to this tab button group.
     */
    public void Subscribe(TabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
        if (button == initial) OnTabSelected(button);
    }
    /*
     * OnTabEnter()
     *  ~~~~~~~~~~~~~~~~
     * When a tab button is being hovered over, it resets tabs and changes the color of the button that is being hovered over
     */
    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if(selectedTab == null || button != selectedTab)
        button.background.color = tabHover;
    }
    /*
     * OnTabExit()
     *  ~~~~~~~~~~~~~~~~
     * When a tab button leaves being hovered over, it resets tabs.
     */
    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }
    /*
     * OnTabSelected()
     *  ~~~~~~~~~~~~~~~~
     * When a tab button us clicked, it deselects the current one, and selects the tab that has been clicked on
     */
    public void OnTabSelected(TabButton button)
    {
        if(selectedTab != null)
        {
            selectedTab.Deselect();
        }

        selectedTab = button;

        selectedTab.Select();

        ResetTabs();
        button.background.color = tabActive;
        int index = button.transform.GetSiblingIndex();

        if(panelGroup != null)
        {
            panelGroup.SetPageIndex(button.transform.GetSiblingIndex());
        }
    }
    /*
     * ResetTabs()
     *  ~~~~~~~~~~~~~~~~
     * Resets tabs, changes all tab colours back to original color (except for the tab that has been selected)
     */
    public void ResetTabs()
    {
        foreach(TabButton button in tabButtons)
        {
            if(selectedTab!=null && button == selectedTab) { continue; }
                button.background.color = tabIdle;
        }
    }
    /*
     * SetActive()
     *  ~~~~~~~~~~~~~~~~
     * Set the tab active depending on the index
     */
    public void SetActive(int index)
    {

        foreach(TabButton t in tabButtons)
        {
            if(t.transform.GetSiblingIndex() == index)
            {
                SetActive(t);
                return;

            }
        }

    }
    /*
     * SetActive()
     *  ~~~~~~~~~~~~~~~~
     * Set the tab active
     */
    public void SetActive(TabButton activeTab)
    {
        if(activeTab != null)
        {
            OnTabSelected(activeTab);
        }
    }


}

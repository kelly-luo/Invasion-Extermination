using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;

    public Color tabIdle;
    public Color tabHover;
    public Color tabActive;

    public TabButton selectedTab;

    public PanelGroup panelGroup;

    public TabButton initial;

   // public List<GameObject> objectsToSwap;

    [SerializeField] bool keyDown;

    public string controls;



    public void Awake()
    {
        ResetTabs();
    }
    public void Subscribe(TabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
        if (button == initial) OnTabSelected(button);
 

    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if(selectedTab == null || button != selectedTab)
        button.background.color = tabHover;
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

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
        //for(int i = 0; i < objectsToSwap.Count; i++)
        //{
        //    if(i == index) objectsToSwap[i].SetActive(true);
        //    else objectsToSwap[i].SetActive(false);
           
        //}


        if(panelGroup != null)
        {
            panelGroup.SetPageIndex(button.transform.GetSiblingIndex());
        }
    }

    public void ResetTabs()
    {
        foreach(TabButton button in tabButtons)
        {
            if(selectedTab!=null && button == selectedTab) { continue; }
                button.background.color = tabIdle;
        }
    }


    public void Update()
    {
        if (Input.GetAxis(controls) != 0)
        {
            if (!keyDown)
            {

                if (Input.GetAxis(controls) < 0)
                {
                    if (selectedTab.transform.GetSiblingIndex() > 0)
                    {
                        SetActive(selectedTab.transform.GetSiblingIndex() - 1);
                    }
                }

                if (Input.GetAxis(controls) > 0)
                {
                    if (selectedTab.transform.GetSiblingIndex() < (transform.childCount-1))
                    {
                        SetActive(selectedTab.transform.GetSiblingIndex() + 1);
                    }
                }

                keyDown = true;
            }
        }
        else
        {
            keyDown = false;
        }
    }

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


        //OnTabSelected(tabButtons[index]);
    }

    public void SetActive(TabButton activeTab)
    {
        if(activeTab != null)
        {
            OnTabSelected(activeTab);
        }
    }

    //public int getTabIndex()
    //{
    //    if (selectedTab == null) return -1;

    //    int count = 0;
    //    foreach(TabButton tabButton in tabButtons){
    //        if(selectedTab == tabButton)
    //        {
    //            return count;
    //        }
    //        count++;
    //    }
    //    return -1;
    //}

    //public string temqpwe()
    //{


    //    string sadad = "";
    //    foreach (TabButton tabButton in tabButtons)
    //    {
    //        sadad += tabButton.name;


    //    }
    //    return sadad;
    //}


}

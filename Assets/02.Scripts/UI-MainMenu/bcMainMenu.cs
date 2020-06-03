using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class bcMainMenu : ButtonClicked
{
    private MenuButton menuButton;
    public GameObject frame;
    public GameObject inView;

    void Start()
    {
        menuButton = GetComponent<MenuButton>();
    }

    public override void ButtonEvent(PointerEventData eventData)
    {

        if (frame.activeSelf == false)
        {
            GobalFrame.bringToView(frame, menuButton);
        }
        else
        {
            GobalFrame.leaveView(frame);
        }

        inView = GobalFrame.frameInView;
    }

    public GameObject getInView()
    {
        return inView;
    }

    public void setFrame(GameObject frame)
    {
        this.frame = frame;
    }

    public override void ButtonHover(PointerEventData eventData)
    {
       
    }

    public override void ButtonHoverExit(PointerEventData eventData)
    {
      
    }
}

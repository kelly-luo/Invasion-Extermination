using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionClicked : ButtonClicked
{
    private MyMenuButton menuButton;
    public GameObject frame;
    public GameObject inView;

    void Start()
    {
        menuButton = GetComponent<MyMenuButton>();
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

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ButtonClicked : MonoBehaviour
{
    private bool ButtonClickEvent;
    void Start()
    {
        ButtonClickEvent = false;
    }

    public abstract void ButtonEvent(MyMenuButton menuButton);
    public void ButtonEventEnd() 
    {
        ButtonClickEvent = false;
    }
        
}

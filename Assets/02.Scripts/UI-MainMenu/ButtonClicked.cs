using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ButtonClicked : MonoBehaviour
{
    private bool ButtonClickEvent;
    void Start()
    {
        ButtonClickEvent = false;
    }

    public abstract void ButtonEvent(PointerEventData eventData);
    public abstract void ButtonHover(PointerEventData eventData);
    public abstract void ButtonHoverExit(PointerEventData eventData);
    public void ButtonEventEnd() 
    {
        ButtonClickEvent = false;
    }
        
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class bcExit : ButtonClicked
{
    public bool isEnding = false;
    public override void ButtonEvent(PointerEventData eventData)
    {
        Application.Quit();
    }

    public override void ButtonHover(PointerEventData eventData)
    {
       
    }

    public override void ButtonHoverExit(PointerEventData eventData)
    {

    }

    public bool getIsEnding()
    {
        return isEnding;
    }
    private void OnApplicationQuit()
    {
        isEnding = true;
    }

}

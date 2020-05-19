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

    public bool getIsEnding()
    {
        return isEnding;
    }
    private void OnApplicationQuit()
    {
        isEnding = true;
    }

}

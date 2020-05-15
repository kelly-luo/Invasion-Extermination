using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bcExit : ButtonClicked
{
    public bool isEnding = false;
    public override void ButtonEvent(MyMenuButton menuButton)
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

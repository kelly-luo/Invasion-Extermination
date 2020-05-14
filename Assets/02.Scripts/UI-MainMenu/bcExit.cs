using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bcExit : ButtonClicked
{
    public override void ButtonEvent(MyMenuButton menuButton)
    {
        Application.Quit();
    }

}

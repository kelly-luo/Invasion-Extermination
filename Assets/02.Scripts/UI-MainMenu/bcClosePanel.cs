using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bcClosePanel : ButtonClicked
{
    public GameObject panel;
    public override void ButtonEvent(MyMenuButton menuButton)
    {
        if (panel != null) panel.SetActive(false);
    }
}

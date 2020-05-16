using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplySettings : ButtonClicked
{
    
    public Dropdown resoultionCombo;
    public Toggle windowToggleMode;

  
    public override void ButtonEvent(MyMenuButton menuButton)
    {
        bool windowsMode = windowToggleMode.isOn;
        string restext = resoultionCombo.options[resoultionCombo.value].text;
        string[] restextSp = restext.Split('x');

        int width = int.Parse(restextSp[0]);
        int height = int.Parse(restextSp[1]);

        Screen.SetResolution(width, height, windowsMode);   
    }
}

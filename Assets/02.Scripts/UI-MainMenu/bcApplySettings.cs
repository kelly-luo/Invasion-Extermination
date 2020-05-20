using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class bcApplySettings : ButtonClicked
{
    
    public Dropdown resoultionCombo;
    public Toggle windowToggleMode;

  
    public override void ButtonEvent(PointerEventData eventData)
    {
        bool windowsMode = windowToggleMode.isOn;
        string restext = resoultionCombo.options[resoultionCombo.value].text;
        string[] restextSp = restext.Split('x');

        int width = int.Parse(restextSp[0]);
        int height = int.Parse(restextSp[1]);

        Screen.SetResolution(width, height, windowsMode);   
    }
}

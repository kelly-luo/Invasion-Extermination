﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MenuRight : ButtonClicked
{
    public MapSelect mapSelect;
    public override void ButtonEvent(MyMenuButton menuButton)
    {
         mapSelect.index += 1;
    }

}


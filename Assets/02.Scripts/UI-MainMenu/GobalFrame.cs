using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GobalFrame : MonoBehaviour
{
    public static GameObject frameInView;
    public static MenuButton currentButton;
    // Start is called before the first frame update
  

    public static void bringToView(GameObject newFrame, MenuButton newButton)
    {
     
        if (frameInView != null)
        {
            if (frameInView != newFrame)
            {
                frameInView.SetActive(false);
                currentButton = newButton;
                frameInView = newFrame;
                frameInView.SetActive(true);
            }
        }
        else
        {
            currentButton = newButton;
            frameInView = newFrame;
            frameInView.SetActive(true);
        }
    }
    public static void leaveView(GameObject currentFrame)
    {
        if (frameInView == currentFrame)
        {
            frameInView.SetActive(false);
            frameInView = null;
        }

    }
}

/*GobalFrame Class
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This class handles what frame is in view
 * 
 * AUT University - 2020 - Yuki Liyanage
 * 
 * Revision History
 *  ~~~~~~~~~~~~~~~~
 *  16.04.2020 Creation date (Yuki)
 *  12.06.2020 Refactored, and removed unnecessary code (Yuki)
 *  
 *  
 *  Unity support packages
 */
using UnityEngine;

public class GobalFrame 
{
    public static GameObject frameInView;
    public static MenuButton currentButton;

    /*
     * BringToView()
     *  ~~~~~~~~~~~~~~~~
     *  Brings the new frame into view and set the preivous frame out of view
     */
    public static void BringToView(GameObject newFrame, MenuButton newButton)
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
    /*
     * LeaveView()
     *  ~~~~~~~~~~~~~~~~
     *  Bring current frame out of view
     */
    public static void LeaveView(GameObject currentFrame)
    {
        if (frameInView == currentFrame)
        {
            frameInView.SetActive(false);
            frameInView = null;
        }

    }
}

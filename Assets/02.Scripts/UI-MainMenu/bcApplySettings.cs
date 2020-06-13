/*bcApplySettings MenuButton
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This class handles the inputs that the user wants as their settings, and applies it
 * 
 * AUT University - 2020 - Yuki Liyanage
 * 
 * Revision History
 *  ~~~~~~~~~~~~~~~~
 *  21.04.2020 Creation date (Yuki)
 *  12.06.2020 Refactored, and removed unnecessary code (Yuki)
 *  
 *  
 *  Unity support packages
 */
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class bcApplySettings : ButtonClicked
{
    
    public Dropdown resoultionCombo;
    public Toggle windowToggleMode;

    /*
   * ButtonEvent()
   *  ~~~~~~~~~~~~~~~~
   *  Handles when a button is clicked on. It takes the user's input and change the settings of the game
   *  on the user inputs.
   */
    public override void ButtonEvent(PointerEventData eventData)
    {
        bool windowsMode = windowToggleMode.isOn;
        string restext = resoultionCombo.options[resoultionCombo.value].text;
        string[] restextSp = restext.Split('x');

        int width = int.Parse(restextSp[0]);
        int height = int.Parse(restextSp[1]);

        Screen.SetResolution(width, height, windowsMode);   
    }

    #region UNUSED_BUTTON_CLICK_METHODS
    public override void ButtonHover(PointerEventData eventData)
    {
       
    }

    public override void ButtonHoverExit(PointerEventData eventData)
    {

    }
    #endregion
}

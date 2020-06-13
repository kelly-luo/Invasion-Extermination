/*bcExit MenuButton
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This button handles when the user wants to quit the game completely
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
using UnityEngine.EventSystems;

public class bcExit : ButtonClicked
{
    public bool isEnding = false;
    /*
    * ButtonEvent()
    *  ~~~~~~~~~~~~~~~~
    *  Handles when a button is clicked on. Closes the game, completely.
    */
    public override void ButtonEvent(PointerEventData eventData)
    {
        Application.Quit();
    }
     /*
     * getIsEnding()
     *  ~~~~~~~~~~~~~~~~
     *  returns if the game is ending or not
     */
    public bool getIsEnding()
    {
        return isEnding;
    }
    /*
     * OnApplicationQuit()
     *  ~~~~~~~~~~~~~~~~
     *  Handles when the game is about to quit, make isEnding true
     */
    private void OnApplicationQuit()
    {
        isEnding = true;
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

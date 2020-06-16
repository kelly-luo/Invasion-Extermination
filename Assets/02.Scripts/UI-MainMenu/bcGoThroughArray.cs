/*bcGoThroughArray MenuButton
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This button handles when the user clicks on a direction button. It has a direction
 * which tells the array if it is going left or right (up or down) a array
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

public class bcGoThroughArray : ButtonClicked
{
 
    [SerializeField] private int direction = 0;
    public int Direction
    {
        get {
            clicked = false;
            return direction; 
        }
    }
    private bool clicked = false;

    /*
     * ButtonEvent()
     *  ~~~~~~~~~~~~~~~~
     *  Handles the button click. Sets clicked to true, so that the controller class can see if the button was clicked
     */
    public override void ButtonEvent(PointerEventData eventData)
    {
        clicked = true;
    }

    /*
     * IsClick()
     *  ~~~~~~~~~~~~~~~~
     *  Checks if the button has been clicked
     */
    public bool IsClick()
    {
        return clicked;
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

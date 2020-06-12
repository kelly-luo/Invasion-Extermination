/*ButtonClicked Abstract Button Event
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This class handles when the user interacts with a menu button. 
 * This handles when the button is clicked on, when the mouse is hovering on the button, and when the mouse leaves button
 * 
 * AUT University - 2020 - Yuki Liyanage
 * 
 * Revision History
 *  ~~~~~~~~~~~~~~~~
 *  16.04.2020 Creation date (Yuki)
 *  12.06.2020 Refactored, and removed uncessary code (Yuki)
 *  
 *  
 *  Unity support packages
 */
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ButtonClicked : MonoBehaviour
{
    /*
     * ButtonEvent()
     *  ~~~~~~~~~~~~~~~~
     *  Handles when a button is clicked on
     */
    public abstract void ButtonEvent(PointerEventData eventData);
    /*
     * ButtonHover()
     *  ~~~~~~~~~~~~~~~~
     *  Handles when a button is hovered on
     */
    public abstract void ButtonHover(PointerEventData eventData);
    /*
     * ButtonHoverExit()
     *  ~~~~~~~~~~~~~~~~
     *  Handles when a button leaves hover on the button
     */
    public abstract void ButtonHoverExit(PointerEventData eventData);

}

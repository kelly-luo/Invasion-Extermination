/*bcShowPanel MenuButton
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This button handles displaying certain panels
 * 
 * AUT University - 2020 - Yuki Liyanage
 * 
 * Revision History
 *  ~~~~~~~~~~~~~~~~
 *  01.06.2020 Creation date (Yuki)
 *  21.06.2020 Refactored, and removed unnecessary code & added Hovering functions to buttons (Yuki)
 *  
 *  
 *  UnityEngine support packages
 */
using UnityEngine;
using UnityEngine.EventSystems;

public class bcShowPanel : ButtonClicked
{
    public GameObject nextPanel;
    public GameObject currentPanel;
    /*
    * ButtonEvent()
    *  ~~~~~~~~~~~~~~~~
    *  Handles when a button is clicked on. disables current panel and shows the next panel
    */
    public override void ButtonEvent(PointerEventData eventData)
    {
        currentPanel.SetActive(false);
        nextPanel.SetActive(true);
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

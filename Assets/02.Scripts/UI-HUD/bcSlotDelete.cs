/*bcSlotDelete MenuButton
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This button handles when the user wants to delete a gun from their inventory
 * 
 * AUT University - 2020 - Yuki Liyanage
 * 
 * Revision History
 *  ~~~~~~~~~~~~~~~~
 *  22.05.2020 Creation date (Yuki)
 *  21.06.2020 Refactored, and removed unnecessary code (Yuki)
 *  
 *  
 *  UnityEngine support packages
 */
using UnityEngine;
using UnityEngine.EventSystems;

public class bcSlotDelete : ButtonClicked
{
    public InventoryManager inventoryManager;
    public GameObject Slot;
    /*
    * ButtonEvent()
    *  ~~~~~~~~~~~~~~~~
    *  Handles when a button is clicked on. Removes the gun from player invenotry.
    */
    public override void ButtonEvent(PointerEventData eventData)
    {
        inventoryManager.RemoveItem(Slot.GetComponent<bcSlotSelect>().InstanceId);
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

/*bcBuyAmmo MenuButton
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This button handles when the user wants to buy gun from the item shop
 * 
 * AUT University - 2020 - Yuki Liyanage
 * 
 * Revision History
 *  ~~~~~~~~~~~~~~~~
 *  11.06.2020 Creation date (Yuki)
 *  21.06.2020 Refactored, and removed unnecessary code (Yuki)
 *  
 *  
 *  UnityEngine support packages
 */
using UnityEngine.EventSystems;

public class bcBuyGun : ButtonClicked
{
    public ShopManager manager;
    /*
    * ButtonEvent()
    *  ~~~~~~~~~~~~~~~~
    *  Handles when a button is clicked on. Buys Gun from item shop, via ShopManager class
    */
    public override void ButtonEvent(PointerEventData eventData)
    {
        manager.BuyGun();
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

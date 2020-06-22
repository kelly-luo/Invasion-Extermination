/*bcCredits MenuButton
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This class plays the credits
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
using UnityEngine.SceneManagement;

public class bcCredits : ButtonClicked
{
    /*
    * ButtonEvent()
    *  ~~~~~~~~~~~~~~~~
    *  Loads Credits
    */
    public override void ButtonEvent(PointerEventData eventData)
    {
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
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

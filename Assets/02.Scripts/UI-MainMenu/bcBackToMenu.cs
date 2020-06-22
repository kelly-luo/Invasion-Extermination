/*bcBackToMenu MenuButton
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This button handles when the user wants to go back to main menu
 * 
 * AUT University - 2020 - Yuki Liyanage
 * 
 * Revision History
 *  ~~~~~~~~~~~~~~~~
 *  01.06.2020 Creation date (Yuki)
 *  12.06.2020 Refactored, and removed unnecessary code (Yuki)
 *  
 *  
 *  Unity support packages
 */
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class bcBackToMenu : ButtonClicked
{

    /*
   * ButtonEvent()
   *  ~~~~~~~~~~~~~~~~
   *  Handles when a button is clicked on. Goes back to main menu scene.
   */
    public override void ButtonEvent(PointerEventData eventData)
    {
        Destroy(GameObject.Find("GameManager"));
        SceneManager.LoadScene("MainMenuV2", LoadSceneMode.Single);
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

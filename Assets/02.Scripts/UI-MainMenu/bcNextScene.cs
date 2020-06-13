/*bcNextScene MenuButton
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This button handles when the button goes to the next scene
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
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class bcNextScene : ButtonClicked
{
    public MapSelect mapSelect;
    private string mapID;
    /*
     * ButtonEvent()
     *  ~~~~~~~~~~~~~~~~
     *  Handles the button click. Gets the mapid from mapSelect, and loads next scene
     */
    public override void ButtonEvent(PointerEventData eventData)
    {
        mapID = mapSelect.getMap();
        SceneManager.LoadScene(mapID, LoadSceneMode.Single);
       
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

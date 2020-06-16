/*PanelGroup Class
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This class handles what panel is being shown to the player
 * 
 * AUT University - 2020 - Yuki Liyanage
 * Used Tutorial: https://www.youtube.com/watch?v=211t6r12XPQ - Game Dev Guide
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

public class PanelGroup : MonoBehaviour
{
    public GameObject[] panels;
    public TabGroup tabGroup;
    public int panelIndex;

    /*
     * Awake()
     *  ~~~~~~~~~~~~~~~~
     *  Shows the first panel
     */
    private void Awake()
    {
        ShowCurrentPanel();
    }

    /*
     * ShowCurrentPanel()
     *  ~~~~~~~~~~~~~~~~
     *  Shows the current panel. And turn off all other panels
     */
    public void ShowCurrentPanel()
    {
        for(int i = 0; i < panels.Length; i++)
        {
            if (i == panelIndex) panels[i].gameObject.SetActive(true);
            else panels[i].gameObject.SetActive(false);
        }
    }
    /*
     * SetPageIndex()
     *  ~~~~~~~~~~~~~~~~
     *  Set the panel index, and update the view panel
     */
    public void SetPageIndex(int index)
    {
        panelIndex = index;
        ShowCurrentPanel();
    }
}

/*MapSelect Class allows going through the class
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This handles what map the user is going to pick
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
using UnityEngine.UI;

public class MapSelect : MonoBehaviour
{

    public Image imageComp;

    public MapClass currentMap;

    private int index;

    public MapClass[] maps;

    public bcGoThroughArray[] leftrightbuttons;

    /*
    * Start()
    *  ~~~~~~~~~~~~~~~~
    *  initialize, the map starting point, starting at index 0 
    */
    void Start()
    {
        index = 0;
        updatePanel(index);
    }
    /*
    * Update()
    *  ~~~~~~~~~~~~~~~~
    *  Checks if the direction button has been clicked
    */
    void Update()
    {
        for(int i = 0; i< maps.Length; i++)
        {
            if (leftrightbuttons[i].IsClick())
            {
                updatePanel(leftrightbuttons[i].Direction);
            }
        }

    }
    /*
    * updatePanel()
    *  ~~~~~~~~~~~~~~~~
    *  Sets the image of map according to the map scene using the index
    */
    private void updatePanel(int direction)
    {
        index += direction;
        if (index >= maps.Length) index = 0;
        else if (index < 0) index = maps.Length - 1;

        currentMap = maps[index];
        imageComp.sprite = currentMap.mapImage;
    }
    /*
    * getMap()
    *  ~~~~~~~~~~~~~~~~
    *  gets map id (scene id), to load the scene
    */
    public string getMap()
    {
        return currentMap.mapID;
    }

}

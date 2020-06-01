using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class bcNextScene : ButtonClicked
{
    public MapSelect mapSelect;
    private string mapID;

    public override void ButtonEvent(PointerEventData eventData)
    {
        mapID = mapSelect.getMap();
        SceneManager.LoadScene(mapID, LoadSceneMode.Single);
       
    }

}

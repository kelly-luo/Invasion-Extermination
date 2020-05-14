using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bcNextScene : ButtonClicked
{
    public MapSelect mapSelect;
    public string mapID;

    public override void ButtonEvent(MyMenuButton menuButton)
    {
        mapID = mapSelect.getMap();
        SceneManager.LoadScene(mapID, LoadSceneMode.Additive);
    }

}

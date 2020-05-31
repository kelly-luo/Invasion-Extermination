using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class bcBackToMenu : ButtonClicked
{
    public override void ButtonEvent(PointerEventData eventData)
    {
        SceneManager.LoadScene("MainMenuV2", LoadSceneMode.Single);
    }
}

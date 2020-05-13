using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMenuButtonGroup : MonoBehaviour
{
    public List<MyMenuButton> buttons;
  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //When clicked
    public void OnTabEnter(MyMenuButton button)
    {
        button.pressed = true;
    }

    //When Exit
    public void OnTabExit(MyMenuButton button)
    {
        button.setState("deselected");
    }

    //When Hover
    public void OnTabSelected(MyMenuButton button)
    {
        button.setState("selected");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserInputManager 
{
    float GetAxis(string inputKey);

    bool GetKeyUp(KeyCode key);

    bool GetKeyDown(KeyCode key);

    bool GetMouseButtonUp(int button);
}

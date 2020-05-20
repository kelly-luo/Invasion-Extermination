using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTranslate : ICharacterTranslate
{
    public Transform Character { get; set; }
    public float Speed { get; set; } = 1.5f;
    public bool IsSitting { get; set; }
    public Vector3 MoveDirection { get; set; }
    private IUnityServiceManager unityService = UnityServiceManager.Instance;

    private bool isRunning;
    public bool IsRunning
    {
        get
        {
            return isRunning;
        }
        set
        {
            if (value)
            {
                Speed = 2.5f;
            }
            else
            {
                Speed = 1.5f;
            }
        }
    }

    public MonsterTranslate(Transform character)
    {
        this.Character = character; 
    }

    public void TranslateCharacter(Vector3 moveDir)
    {
        MoveDirection = moveDir;
        Character.Translate(moveDir.normalized * Speed * unityService.DeltaTime, Space.World);
    }
}

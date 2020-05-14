﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTranslate : ICharacterTranslate
{
    public Transform Character { get; set; }
    public float Speed { get; set; } = 1.5f;
    public Vector3 MoveDirection { get; set; }

    private bool isRunning = false;
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
                Speed = 3.0f;
            }
            else
            {
                Speed = 1.5f;
            }
        }
    }

    private bool isSitting = false;
    public bool IsSitting
    {
        get
        {
            return isSitting;
        }
        set
        {
            if (value)
            {
                Speed = 1f;
            }

        }
    }

    public PlayerTranslate(Transform character)
    {
        this.Character = character;
    }


    public void TranslateCharacter(Vector3 moveDir)
    {
        MoveDirection = moveDir;
        Character.Translate(moveDir.normalized * Speed * Time.deltaTime, Space.World);
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerStats data = SaveSystem.LoadPlayer();

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        Character.position = position;
    }

}

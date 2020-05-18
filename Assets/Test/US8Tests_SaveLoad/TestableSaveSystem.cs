using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestableSaveSystem :ISaveSystem
{
    //private readonly ISaveSystem _externalSaveSystem;

    //public TestableSaveSystem(ISaveSystem _externalSaveSystem)
    //{
    //    this._externalSaveSystem = _externalSaveSystem;
    //}

    public void Save(PlayerInformation player)
    {
        SaveSystem.SavePlayer(player);
    }

    public PlayerStats Load()
    {
        return SaveSystem.LoadPlayer();
    }
}

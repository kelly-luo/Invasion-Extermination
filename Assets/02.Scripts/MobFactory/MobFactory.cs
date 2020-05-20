using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobFactory<T> where T : MobFactory<T>, new()
{
    private static T instance = new T();

    public static T Instance
    {
        get
        {
            return instance;
        }
    }

    public abstract GameObject CreateMob();

}

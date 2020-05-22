using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanFactory : MobFactory
{
    public GameObject Avatar;

    public override GameObject CreateMob()
    {
        return Instantiate(Avatar);
    }
}

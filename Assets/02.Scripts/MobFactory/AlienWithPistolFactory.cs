using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienWithPistolFactory : MobFactory
{
    public GameObject pistol;
    public GameObject Avatar;

    public override GameObject CreateMob()
    {
        Instantiate(Avatar);
        Instantiate(pistol);

        var monsterController = Avatar.GetComponent<MonsterController>();
        monsterController.EquipWeapon(pistol);

        return Avatar;
    }
}

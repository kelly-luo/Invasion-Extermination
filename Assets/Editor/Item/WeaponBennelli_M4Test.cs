﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class WeaponBennelli_M4Test
    {
        WeaponBennelli_M4 weaponClass;
        GameObject weapon;
        GameObject target;

        [SetUp]
        public void SetUpTest()
        {
            weapon = new GameObject();
            weaponClass = weapon.AddComponent<WeaponBennelli_M4>();

            target = new GameObject();

            target.transform.position = new Vector3(0f, 30f, 50f);

            BoxCollider boxCollider = target.AddComponent<BoxCollider>();
            boxCollider.size = new Vector3(10f, 10f, 10f);

        }

        // A Test behaves as an ordinary method
        [Test]
        public void BulletRunOut_BulletRunOutTest()
        {
            this.SetUpTest();

            var a = 1;
            weaponClass.NumOfBullet = weaponClass.MaxBullet;
            weaponClass.OnBulletRunOut += () => { a++; };

            for (int i = 0; i < weaponClass.MaxBullet; i++)
                weaponClass.NumOfBullet--;

            Assert.AreEqual(2, a);

            // Use the Assert class to test conditions
        }

        [Test]
        public void BulletRunOut_ReloadTest()
        {
            this.SetUpTest();

            var a = 1;
            var NumOfBulletLeft = 13;

            weaponClass.NumOfBullet = weaponClass.MaxBullet;
            weaponClass.OnReload += () => { a++; };

            for (int i = 0; i < weaponClass.MaxBullet; i++)
                weaponClass.NumOfBullet--;
            //Player manually reload when they run out of ammo
            weaponClass.Reload(ref NumOfBulletLeft);

            Assert.AreEqual(2, a);
            Assert.AreEqual(0, NumOfBulletLeft);
            Assert.AreEqual(13, weaponClass.NumOfBullet);
        }

        [UnityTest]
        public IEnumerator Fire_Test()
        {
            this.SetUpTest();
            yield return null;

            weaponClass.gameObject.transform.LookAt(target.transform);

            GameObject bulletHitObject = weaponClass.Fire();

            Assert.AreEqual(target, bulletHitObject);
        }

        [UnityTest]
        public IEnumerator Fire_OnShotFireTest()
        {
            this.SetUpTest();
            yield return null;

            var a = 1;
            weaponClass.OnShotFire += () => { a++; };

            weaponClass.Fire();

            Assert.AreEqual(2, a);
        }

    }
}

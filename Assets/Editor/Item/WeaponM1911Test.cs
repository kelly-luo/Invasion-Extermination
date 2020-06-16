﻿using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class WeaponM1911Test
    {
        WeaponM1911 weaponClass;
        GameObject weapon;
        GameObject target;

        int a = 1;
        [SetUp]
        public void SetUpTest()
        {
            target = new GameObject();
            weapon = new GameObject();
            weaponClass = weapon.AddComponent<WeaponM1911>();
            a = 1;
            target.transform.position = new Vector3(0f, 13f, 13f);
            target.layer = LayerMask.NameToLayer("Obstacle");
            BoxCollider boxCollider = target.AddComponent<BoxCollider>();
            boxCollider.size = new Vector3(10f, 10f, 10f);

        }
        [Test]
        public void BulletRunOut_BulletRunOutTest()
        {
            var a = 1;
            weaponClass.NumOfBullet = weaponClass.MaxBullet;
            weaponClass.OnBulletRunOut += () => { a++; };

            for (int i = 0; i < weaponClass.MaxBullet; i++)
                weaponClass.NumOfBullet--;

            Assert.AreEqual(2, a);

        }

        [Test]
        public void BulletRunOut_ReloadTest()
        {

            var a = 1;
            var NumOfBulletLeft = weaponClass.MaxBullet;

            weaponClass.NumOfBullet = weaponClass.MaxBullet;
            weaponClass.OnReload += () => { a++; };

            for (int i = 0; i < weaponClass.MaxBullet; i++)
                weaponClass.NumOfBullet--;
            //Player manually reload when they run out of ammo
            weaponClass.Reload(ref NumOfBulletLeft);

            Assert.AreEqual(2, a);
            Assert.AreEqual(0, NumOfBulletLeft);
            Assert.AreEqual(weaponClass.MaxBullet, weaponClass.NumOfBullet);
        }

        [UnityTest]
        public IEnumerator Fire_Test()
        {
            var unitService = Substitute.For<IUnityServiceManager>();
            unitService.DeltaTime.Returns(1);
            unitService.TimeAtFrame.Returns(5);// this value need to be high else return null 
            weaponClass.UnityService = unitService;
            weaponClass.SetLayer();

            weaponClass.gameObject.transform.LookAt(target.transform);
            yield return null;
            GameObject bulletHitObject = weaponClass.Fire(weaponClass.gameObject.transform.position,
                weaponClass.gameObject.transform.forward);

            Assert.AreEqual(target, bulletHitObject);

        }

        [UnityTest]
        public IEnumerator Fire_OnShotFireTest()
        {
            var unitService = Substitute.For<IUnityServiceManager>();
            unitService.DeltaTime.Returns(1);
            unitService.TimeAtFrame.Returns(5);// this value need to be high else return null 
            weaponClass.UnityService = unitService;
            weaponClass.OnShotFire += () => { a++; };

            weaponClass.Fire(weaponClass.gameObject.transform.position,
                weaponClass.gameObject.transform.forward);

            Assert.AreEqual(2, a);
            yield return null;

        }

        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(target);
            Object.DestroyImmediate(weapon);
        }
    }
}

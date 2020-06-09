using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class WeaponBennelli_M4Test2
    {
        WeaponBennelli_M4 weaponClass;
        GameObject weapon;
        GameObject target;

        [SetUp]
        public void SetUpTest()
        {
            target = new GameObject();
            weapon = new GameObject();
            weaponClass = weapon.AddComponent<WeaponBennelli_M4>();
            weapon.transform.position = new Vector3(0f, 0f, 0f);

            target.transform.position = new Vector3(0f, 20f, 0f);
            target.layer = LayerMask.NameToLayer("Obstacle");
            BoxCollider boxCollider = target.AddComponent<BoxCollider>();
            boxCollider.size = new Vector3(10f, 10f, 10f);

        }


        // A Test behaves as an ordinary method
        [Test]
        public void BulletRunOut_BulletRunOutTest()
        {     

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
        public IEnumerator Fire_OnShotFireTest()
        {
            var unitService = Substitute.For<IUnityServiceManager>();
            unitService.DeltaTime.Returns(1);
            unitService.TimeAtFrame.Returns(5);// this value need to be high else return null 
            weaponClass.UnityService = unitService;
            target.transform.position = new Vector3(0f, 30f, 0f);

            int a = 1;
            weaponClass.OnShotFire = () => { a+=1; };

            weaponClass.Fire(weaponClass.gameObject.transform.position, weaponClass.gameObject.transform.forward);
            yield return null;
            Assert.AreEqual(2, a);
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

        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(target);
            Object.DestroyImmediate(weapon);
        }
    }
}

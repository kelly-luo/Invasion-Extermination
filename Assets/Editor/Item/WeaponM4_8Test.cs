using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class WeaponM4_8Test
    {
        WeaponM4_8 weaponClass;
        GameObject weapon;
        GameObject target;

        [SetUp]
        public void SetUpTest()
        {
            target = new GameObject();
            weapon = new GameObject();
            weaponClass = weapon.AddComponent<WeaponM4_8>();

            target.transform.position = new Vector3(0f, 30f, 50f);
            target.layer = LayerMask.NameToLayer("Obstacle");
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
            this.SetUpTest();

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
            this.SetUpTest();
            var unitService = Substitute.For<IUnityServiceManager>();
            unitService.DeltaTime.Returns(1);
            unitService.TimeAtFrame.Returns(5);// this value need to be high else return null 
            weaponClass.UnityService = unitService;
            yield return null;

            var a = 1;
            weaponClass.OnShotFire += () => { a++; };

            weaponClass.Fire(weaponClass.gameObject.transform.position, weaponClass.gameObject.transform.forward);

            Assert.AreEqual(2, a);

        }

    }

}

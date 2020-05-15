using System.Collections;
using NSubstitute;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class FirstPersonViewTest
    {
        FirstPersonView firstPersonView;
        GameObject targetGameObject;
        GameObject cameraGameObject;
        GameObject cameraRigGameObject;
        IUnityServiceManager mock;

        [SetUp]
        public void SetUpTest()
        {
            firstPersonView = new FirstPersonView();
            targetGameObject = new GameObject();
            cameraGameObject = new GameObject();
            cameraRigGameObject = new GameObject();

            firstPersonView.XSensitivity = 1;
            firstPersonView.YSensitivity = 1;

            firstPersonView.Init(targetGameObject.transform, cameraRigGameObject.transform, cameraGameObject.transform, mock);
            firstPersonView.IsSmooth = false;
            firstPersonView.IsClampOnRotatingXAxis = false;
        }

        // A Test behaves as an ordinary method
        [Test]
        public void RotateView_RotationTest()
        {
            var unityService = Substitute.For<IUnityServiceManager>();
            unityService.GetAxis("Mouse X").Returns(1);
            unityService.GetAxis("Mouse Y").Returns(1);

            firstPersonView.UnityService = unityService;

            firstPersonView.RotateView();

            var cameraRigRot = Quaternion.Euler(0f, 0f, 0f) * Quaternion.Euler(0f, 1f, 0f);
            var cameraRot = Quaternion.Euler(0f, 0f, 0f) * Quaternion.Euler(-1f, 0f, 0f);

            Assert.AreEqual(cameraRot.eulerAngles.x, cameraGameObject.transform.rotation.eulerAngles.x);
            Assert.AreEqual(cameraRigRot.eulerAngles.y, cameraRigGameObject.transform.rotation.eulerAngles.y);
        }

        [Test]
        public void RotateView_RotationMinClampTest()   
        {
            SetUpTest();

            var unityService = Substitute.For<IUnityServiceManager>();
            unityService.GetAxis("Mouse Y").Returns(179); // as checking clamp this need to be higher than 90 and lowe than 179

            firstPersonView.UnityService = unityService;
            firstPersonView.IsClampOnRotatingXAxis = true;

            firstPersonView.RotateView();

            Assert.Greater(cameraGameObject.transform.rotation.eulerAngles.x, 360 + firstPersonView.MinAngleOnRotatingXAxis - 1);
            Assert.Less(cameraGameObject.transform.rotation.eulerAngles.x, 360 + firstPersonView.MinAngleOnRotatingXAxis + 1);
        }

        [Test]
        public void RotateView_RotationMaxClampTest()
        {
            SetUpTest();

            var unityService = Substitute.For<IUnityServiceManager>();
            unityService.GetAxis("Mouse Y").Returns(-110); // as checking clamp this need to be Lower than -90 and higher than -179

            firstPersonView.UnityService = unityService;
            firstPersonView.IsClampOnRotatingXAxis = true;

            firstPersonView.RotateView();

            Assert.Greater(cameraGameObject.transform.rotation.eulerAngles.x,firstPersonView.MaxAngleOnRotatingXAxis - 1);
            Assert.Less(cameraGameObject.transform.rotation.eulerAngles.x, firstPersonView.MaxAngleOnRotatingXAxis + 1);
        }

        [Test]
        public void SetCameraPos_Test()
        {
            SetUpTest();

            firstPersonView.OffSetX = 1.0f;
            firstPersonView.OffSetY = 1.0f;
            firstPersonView.OffSetZ = 1.0f;

            firstPersonView.SetCameraPos();
            //need more thinik
            Assert.AreEqual(new Vector3(0.0f + firstPersonView.OffSetX, 0.0f + firstPersonView.OffSetY, 0.0f) 
                + cameraRigGameObject.transform.forward * firstPersonView.OffSetZ, cameraRigGameObject.transform.position);
        }

    }
}

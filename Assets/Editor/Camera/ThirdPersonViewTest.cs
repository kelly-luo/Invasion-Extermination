using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ThirdPersonViewTest
    {
        ThirdPersonView thirdPersonView;
        GameObject targetGameObject;
        GameObject cameraGameObject;
        GameObject cameraRigGameObject;
        IUnityServiceManager mock;

        [SetUp]
        public void SetUpTest()
        {
            thirdPersonView = new ThirdPersonView();
            targetGameObject = new GameObject();
            cameraGameObject = new GameObject();
            cameraRigGameObject = new GameObject();

            thirdPersonView.XSensitivity = 1;
            thirdPersonView.YSensitivity = 1;

            thirdPersonView.Init(targetGameObject.transform, cameraRigGameObject.transform, cameraGameObject.transform, mock);
            thirdPersonView.IsSmooth = false;
            thirdPersonView.IsClampOnRotatingXAxis = false;
        }

        // A Test behaves as an ordinary method
        [Test]
        public void RotateView_RotationTest()
        {
            var unityService = Substitute.For<IUnityServiceManager>();
            unityService.GetAxis("Mouse X").Returns(1);
            unityService.GetAxis("Mouse Y").Returns(1);

            thirdPersonView.UnityService = unityService;

            thirdPersonView.RotateView();

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

            thirdPersonView.UnityService = unityService;
            thirdPersonView.IsClampOnRotatingXAxis = true;

            thirdPersonView.RotateView();

            Assert.Greater(cameraGameObject.transform.rotation.eulerAngles.x, 360 + thirdPersonView.MinAngleOnRotatingXAxis - 1);
            Assert.Less(cameraGameObject.transform.rotation.eulerAngles.x, 360 + thirdPersonView.MinAngleOnRotatingXAxis + 1);
        }

        [Test]
        public void RotateView_RotationMaxClampTest()
        {
            SetUpTest();

            var unityService = Substitute.For<IUnityServiceManager>();
            unityService.GetAxis("Mouse Y").Returns(-110); // as checking clamp this need to be Lower than -90 and higher than -179

            thirdPersonView.UnityService = unityService;
            thirdPersonView.IsClampOnRotatingXAxis = true;

            thirdPersonView.RotateView();

            Assert.Greater(cameraGameObject.transform.rotation.eulerAngles.x, thirdPersonView.MaxAngleOnRotatingXAxis - 1);
            Assert.Less(cameraGameObject.transform.rotation.eulerAngles.x, thirdPersonView.MaxAngleOnRotatingXAxis + 1);
        }


        [Test]
        public void SetCameraPos_Test()
        {
            SetUpTest();

            thirdPersonView.TargetOffSet = 1.0f;
            thirdPersonView.Distance = 0f;
            var testingOffSet = 1.0f;
            var testingDistance = 0f; // set the distance 0 

            var originPoint = (new Vector3(0f, testingOffSet, 0f));
            var camPosVect = -cameraGameObject.transform.forward;

            var testingCameraRig = camPosVect * testingDistance + originPoint;
            //calculate the expected value;

            thirdPersonView.SetCameraPos();

            Assert.AreEqual(testingCameraRig, cameraRigGameObject.transform.position);
        }

    }
}
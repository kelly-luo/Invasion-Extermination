using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


public class CameraControl : MonoBehaviour
{
    [Serializable]
    public class CameraMode
    {
        private List<ICameraView> cameraViewList = new List<ICameraView>();

        public ICameraView CurrentView
        {
            get { return cameraViewList[viewListIdx];}
        }
        private int viewListIdx = 0;
        public int ViewListIdx
        {
            get { return viewListIdx; }
            private set // This NEED TO REFACTOR 
            {
                //get HeadRot from old view
                var currentHeadRot = cameraViewList[viewListIdx].HeadRot;
                var currentCameraRigRot = cameraViewList[viewListIdx].CameraRigRot;
                var currentCameraRot = cameraViewList[viewListIdx].CameraRot;
                viewListIdx = ((value) % cameraViewList.Count);
                //set HeadRot to new wiew
                Debug.Log("float count" + cameraViewList.Count);
                Debug.Log("float " + viewListIdx);
                cameraViewList[viewListIdx].HeadRot = currentHeadRot;
                cameraViewList[viewListIdx].CameraRigRot = currentCameraRigRot;
                cameraViewList[viewListIdx].CameraRot = currentCameraRot;
            }
        }

        #region Camera Setting
        [Header("First Person View Camera Setting")]

        public float offSetX = 0f;
        public float offSetY = 1.65f;
        public float offSetZ = 0.2f;

        [Header("Thrid Person View Camera Setting")]
        //offSet (Y) of target
        public float targetOffSet = 1.65f;
        //height different between a target(player)
        public float height = 0.0f;
        //distance between a target(Player) 
        public float distance = 4.0f;
        #endregion
        public FirstPersonView firstPersonView;
        public ThirdPersonView thirdPersonView;



        public void InitView(Transform target, Transform targetNeckTr, Transform targetHeadTr, Transform cameraRigTr, Transform cameraTr, IUserInputManager userInput)
        {
            firstPersonView = new FirstPersonView(offSetX, offSetY, offSetZ);
            thirdPersonView = new ThirdPersonView(targetOffSet, height, distance);

            cameraViewList.Add(firstPersonView);
            cameraViewList.Add(thirdPersonView);

            foreach (var camView in cameraViewList)
            {
                camView.Init(target.transform, targetNeckTr, targetHeadTr, cameraRigTr, cameraTr, userInput);
            }
        }

        public ICameraView NextView()
        {
            ViewListIdx = ViewListIdx + 1;
            return CurrentView;
        }

    }


    public GameObject target;
    private Transform targetNeckTr;
    private Transform targetHeadTr;
    
    public CameraMode cameraMode;
    private Transform cameraRigTr;
    private Transform cameraTr;


    private IUserInputManager userInput;

    private bool isCursorLocked;

    #region properties

    private bool cursorLock = true;
    public bool CursorLock
    {
        get
        {
            return cursorLock;
        }
        set
        {
            if(!value)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            cursorLock = value;
        }
    }
    #endregion

    #region UnityBasicMethod

    void Start()
    {
        //get transform of camera Rig
        cameraRigTr = GetComponent<Transform>();
        //get transform of camera (child relationShip);
        cameraTr = transform.Find("PlayerCamera");

        targetNeckTr = GameObject.Find("Neck").transform;
        targetHeadTr = GameObject.Find("Head").transform;
        if (userInput == null)
            userInput = new UserInputManager();
        //targetNeckTr = target.GetComponent<Animator>().avatar.GetBone("Left Arm/Shoulder");
        //get the Instance in the cameraView Class and put it in the cameraViewList
        cameraMode.InitView(target.transform, targetNeckTr, targetHeadTr, cameraRigTr, cameraTr, userInput);

    }

    void Update()
    {
        cameraMode.CurrentView.RotateView();
        // this is just tesing key to change the view point.
        if(userInput.GetKeyDown(KeyCode.F5))
        {
            cameraMode.NextView();
        }
        UpdateCursorLock();
    }

    void LateUpdate()
    {
        cameraMode.CurrentView.SetCameraPos();

    }
    #endregion

    //private void GetCameraViewList()
    //{
    //    //reflection verion of method that getting view fields in the CameraView class.
    //    var bindingFlags = BindingFlags.Instance |
    //                       BindingFlags.Public;
    //    var fieldValues = cameraMode.GetType()
    //                                .GetFields(bindingFlags)
    //                                .Select(field => field.GetValue(cameraMode))
    //                                .Where(field => field is ICameraView)
    //                                .ToList();

    //    foreach (var viewField in fieldValues)
    //    {
    //        cameraViewList.Add(viewField as ICameraView);
    //    }
    //}

    #region CursorLockMechanism
    public void UpdateCursorLock()
    {
        if (cursorLock)
            LockUpdate();
    }

    private void LockUpdate()
    {
        if (userInput.GetKeyUp(KeyCode.Escape))
        {
            isCursorLocked = false;
        }
        else if (userInput.GetMouseButtonUp(0))
        {
            isCursorLocked = true;
        }

        if (isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    #endregion

}

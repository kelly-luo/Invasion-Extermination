using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


public class CameraControl : MonoBehaviour
{
    [Serializable]
    public class CameraViews
    {
        #region Camera Setting
        [Header("First Person View Camera Setting")]

        public float offSetX = 0f;
        public float offSetY = 1.65f;
        public float offSetZ = 0.2f;

        [Header("Thrid Person View Camera Setting")]
        //offSet (Y) of target
        public float targetOffSet = 2.0f;
        //height different between a target(player)
        public float height = 0.0f;
        //distance between a target(Player) 
        public float distance = 4.0f;
        #endregion
        public FirstPersonView firstPersonView;
        public ThirdPersonView thirdPersonView;

        public void initView()
        {
            firstPersonView = new FirstPersonView(offSetX, offSetY, offSetZ);
            thirdPersonView = new ThirdPersonView(targetOffSet, height, distance);
        }

    }


    public GameObject target;
    private Transform targetNeckTr;
    private Transform targetHeadTr;
    
    public CameraViews cameraView;
    private Transform cameraRigTr;
    private Transform cameraTr;


    private List<ICameraMove> cameraViewList = new List<ICameraMove>();
    private IUserInputManager userInput;

    private bool isCursorLocked;

    #region properties
    private int viewListIdx;
    public int ViewListIdx
    {
        get { return viewListIdx; }
        set { viewListIdx = ((viewListIdx + value) % cameraViewList.Count); }
    }

    private ICameraMove currentView;
    public ICameraMove CurrentView
    {
        get { return currentView; }
        //set
        //{
        //    //if currentView is same as setting value then do nothing.
        //    if (currentView != value)
        //    {
        //        currentView = value;
        //    }
        //}
    }

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
        //targetNeckTr = target.GetComponent<Animator>().avatar.GetBone("Left Arm/Shoulder");
        //get the Instance in the cameraView Class and put it in the cameraViewList
        cameraView.initView();
        this.GetCameraViewList();
        // Dependency injected class for unity Input API (For Unit Testing)
        if (userInput == null)
            userInput = new UserInputManager();
        foreach (var camView in cameraViewList)
        {
            camView.Init(target.transform, targetNeckTr, targetHeadTr,cameraRigTr, cameraTr, userInput);
        }

        currentView = cameraViewList[0];

    }

    void Update()
    {
        currentView.RotateView();
        UpdateCursorLock();
    }

    void LateUpdate()
    {
        currentView.SetCameraPos(target.transform, cameraRigTr);

    }
    #endregion

    //get next cameraView (currently there are only 2 views so this is for future) :)
    public void NextView()
    {
        currentView = cameraViewList[++ViewListIdx];
    }

    private void GetCameraViewList()
    {
        //reflection verion of method that getting view fields in the CameraView class.
        var bindingFlags = BindingFlags.Instance |
                           BindingFlags.Public;
        var fieldValues = cameraView.GetType()
                                    .GetFields(bindingFlags)
                                    .Select(field => field.GetValue(cameraView))
                                    .Where(field => field is ICameraMove)
                                    .ToList();

        foreach (var viewField in fieldValues)
        {
            cameraViewList.Add(viewField as ICameraMove);
        }
    }

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

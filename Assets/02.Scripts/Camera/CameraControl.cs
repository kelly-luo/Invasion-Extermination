using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour, ICameraControl
{


    [Serializable]
    public class CameraMode
    {
        private List<ICameraView> cameraViewList = new List<ICameraView>();

        public ICameraView CurrentView
        {
            get { return cameraViewList[viewListIdx]; }
        }
        private int viewListIdx = 0;
        public int ViewListIdx
        {
            get { return viewListIdx; }
            private set // This NEED TO REFACTOR 
            {
                viewListIdx = ((value) % cameraViewList.Count);
                //set  to new wiew
            }
        }

        #region Camera Sensitivity setting
        private bool smooth = false;
        public bool Smooth
        {
            get { return smooth; }
            set
            {
                if (value != smooth)
                {
                    smooth = value;
                    foreach (var view in cameraViewList)
                        view.IsSmooth = value;
                }
            }
        }

        private float smoothTime;
        public float SmoothTime
        {
            get { return smoothTime; }
            set
            {
                if (value != smoothTime)
                {
                    smoothTime = value;
                    foreach (var view in cameraViewList)
                    {
                        view.SmoothTime = value;
                    }
                }
            }
        }

        private float xSensitivity;
        public float XSensitivity
        {
            get { return xSensitivity; }
            set
            {
                if (value != xSensitivity)
                {
                    xSensitivity = value;
                    foreach (var view in cameraViewList)
                        view.XSensitivity = value;
                }
            }
        }

        private float ySensitivity;
        public float YSensitivity
        {
            get { return ySensitivity; }
            set
            {
                if (value != ySensitivity)
                {
                    ySensitivity = value;
                    foreach (var view in cameraViewList)
                        view.YSensitivity = value;
                }
            }
        }
        #endregion

        #region Camera Setting
        [Header("First Person View Camera Setting")]

        public float offSetX = 0f;
        public float offSetY = 1.65f;
        public float offSetZ = 0.2f;

        [Header("Thrid Person View Camera Setting")]
        //offSet (Y) of target
        public float targetOffSet = 1.65f;
        //distance between a target(Player) 
        public float distance = 4.0f;
        #endregion

        public FirstPersonView firstPersonView;
        public ThirdPersonView thirdPersonView;

        public void InitView(Transform target, Transform cameraRigTr, Transform cameraTr, IUnityServiceManager userInput)
        {
            firstPersonView = new FirstPersonView(offSetX, offSetY, offSetZ);
            thirdPersonView = new ThirdPersonView(targetOffSet, distance);

            var bindingFlags = BindingFlags.Instance | BindingFlags.Public;

            var fieldValues = this.GetType()
                                .GetFields(bindingFlags)
                                .Select(field => field.GetValue(this))
                                .Where(field => field is ICameraView)
                                .ToList();

            foreach (var viewField in fieldValues)
            {
                cameraViewList.Add(viewField as ICameraView);
            }

            foreach (var camView in cameraViewList)
            {
                camView.Init(target.transform, cameraRigTr, cameraTr, userInput);
            }
            this.SmoothTime = 10f;
            this.XSensitivity = 1.3f;
            this.YSensitivity = 1.3f;
            this.Smooth = true;
        }

        public void TransferRotationData()
        {
            var currentCameraRigRot = cameraViewList[viewListIdx].CameraRigRot;
            var currentCameraRot = cameraViewList[viewListIdx].CameraRot;
        }

        public ICameraView NextView()
        {
            //transfer Camera Rotation data to new view 
            var currentCameraRigRot = cameraViewList[viewListIdx].CameraRigRot;
            var currentCameraRot = cameraViewList[viewListIdx].CameraRot;

            ViewListIdx = ViewListIdx + 1;

            cameraViewList[viewListIdx].CameraRigRot = currentCameraRigRot;
            cameraViewList[viewListIdx].CameraRot = currentCameraRot;
            return CurrentView;
        }

    }


    public CameraViewType CurrentViewMode { get; set; } = CameraViewType.First;

    public GameObject target;

    public CameraMode cameraMode;
    private Transform cameraRigTr;
    private Transform cameraTr;

    private IUnityServiceManager UnityService;

    private bool isCursorLocked;

    #region properties
    public float XRot
    {
        get
        {
            return cameraMode.CurrentView.XRot;
        }
    }

    public float YRot
    {
        get
        {
            return cameraMode.CurrentView.YRot;
        }
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
            if (!value)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            cursorLock = value;
        }
    }

    public Action OnViewChange { get; set; }
    #endregion

    #region CameraEffectSetting
    public bool shakeRotation = false;
    #endregion

    #region Mono BasicMethod
    void Awake()
    {
        //get transform of camera Rig
        cameraRigTr = GetComponent<Transform>();
        //get transform of camera (child relationShip);
        cameraTr = transform.Find("PlayerCamera");

    }
    void Start()
    {
        if (UnityService == null)
            UnityService = new UnityServiceManager();
        //targetNeckTr = target.GetComponent<Animator>().avatar.GetBone("Left Arm/Shoulder");
        //get the Instance in the cameraView Class and put it in the cameraViewList
        cameraMode.InitView(target.transform, cameraRigTr, cameraTr, UnityService);

    }

    void Update()
    {
        UpdateViewMode();

        UpdateCursorLock();
    }


    void LateUpdate()
    {
        cameraMode.CurrentView.RotateView();
        // this is just tesing key to change the view point.
        cameraMode.CurrentView.SetCameraPos();

    }
    #endregion

    #region CursorLockMechanism
    public void UpdateCursorLock()
    {
        if (cursorLock)
            LockUpdate();
    }

    private void LockUpdate()
    {

        if (UnityService.GetKeyUp(KeyCode.Escape))
        {
            isCursorLocked = false;
        }
        else if (UnityService.GetMouseButtonUp(0))
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

    #region CameraRelatedMethod

    public Transform GetCameraTransform()
    {
        return cameraTr;
    }
    public Vector3 GetSightDirection()
    {
        return cameraMode.CurrentView.GetCameraDirection();
    }

    public IEnumerator ShakeCamera(float duration = 0.05f, float magnitudePos = 0.03f, float magnitudeRot = 0.1f)
    {
        float passTime = 0.0f;

        while (passTime < duration)
        {
            Vector3 shakePos = UnityService.InsideUnitSphere;

            cameraTr.localPosition = shakePos * magnitudePos;
      
            if (shakeRotation)
            {
                //get random rotation from PerlineNoise function math lib
                Vector3 shakeRot = new Vector3(0, 0, Mathf.PerlinNoise(UnityService.TimeAtFrame * magnitudeRot, 0.0f));

                cameraTr.localRotation = Quaternion.Euler(shakeRot);
            }
            passTime += UnityService.DeltaTime;
            Debug.Log("asdas");


            yield return null;
        }

    }
    private void UpdateViewMode()
    {
        if (UnityService.GetKeyDown(KeyCode.F5))
        {
            cameraMode.NextView();
            ViewModeChange();
        }
    }

    private void ViewModeChange()
    {
        if (CurrentViewMode == CameraViewType.First)
            CurrentViewMode = CameraViewType.Third;
        else
            CurrentViewMode = CameraViewType.First;

        this.OnViewChange?.Invoke();
    }
    #endregion
}
//view of the camera this need to be in the order of the camera view change 
//e.g. First person view shows first before the Third person View
public enum CameraViewType
{
    First, Third
}

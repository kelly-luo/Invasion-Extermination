using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{

    #region Input Values
    public float Vertical { get; set; }
    public float Horizontal { get; set; }
    #endregion

    #region Camera
    public GameObject cameraRig;
    private Transform cameraTr;
    private Transform cameraRigTr;

    private ICameraControl cameraCtrl;
    public ICameraControl CameraCtrl { get; set; }

    private Vector3 cameraDirection;

    #endregion

    #region Character Avatar Value
    private Quaternion headRot = Quaternion.Euler(0f,0,0f);
    private Quaternion actualHeadRot;

    public float MaxAngleOnHeadRotation { get; set; } = 90f;
    public float MinAngleOnHeadRotation { get; set; } = -90f;

    private Transform neckTr;
    private Transform headTr;

    #endregion

    #region Animator
    private Animator animator; // this need to be DI

    private readonly int hashXSpeed = Animator.StringToHash("XSpeed");
    private readonly int hashZSpeed = Animator.StringToHash("ZSpeed");
    #endregion

    private ICharacterTranslate playerTranslate;
    private Transform tr;
    private IUserInputManager userInput;


    void Awake()
    {
        neckTr = GameObject.Find("Neck").transform;
        headTr = GameObject.Find("Head").transform;
        tr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        playerTranslate = new PlayerTranslate(tr);
        actualHeadRot = headTr.localRotation;
    }

    void Start()
    {
        cameraCtrl = cameraRig.GetComponent<CameraControl>();
        cameraRigTr = cameraRig.GetComponent<Transform>();
        cameraTr = cameraCtrl.GetCameraTransform();

        if (userInput == null)
        {
            userInput = new UserInputManager();
        }
    }

    void Update()
    {
    }

    private void LateUpdate()
    {
        this.MoveCharacter();
        this.RotateNeck();
        this.RotateHeadAndAvatar(cameraCtrl.YRot);
    }

    private void MoveCharacter()
    {
        Vertical = userInput.GetAxis("Vertical");
        Horizontal = userInput.GetAxis("Horizontal");
        Vector3 moveDir = (cameraRigTr.forward * Vertical) + (cameraRigTr.right * Horizontal);
        Debug.Log("X: " + Vertical.ToString() + "Y: " + Horizontal.ToString());
        playerTranslate.TranslateCharacter(moveDir);
        var xSpeed = Horizontal;
        animator.SetFloat(hashXSpeed, xSpeed);
        var zSpeed = Vertical;
        animator.SetFloat(hashZSpeed, zSpeed);
        if ((xSpeed > 0)||( zSpeed > 0))
            this.RotateAvatarTowardSight();
    }

    #region CharacterBodyMove

    private void RotateNeck()
    {
        //you might be wondering why did i put the x on z axis and 
        //it is because the I found that local Transform of a Neck bone in model was reversed. 
        neckTr.localRotation = Quaternion.Euler(0f, 0f, cameraTr.localRotation.eulerAngles.x);

    }

    private void RotateHeadAndAvatar(float yRot)
    {
        //NextAngle is amount of angle change in head rotation
        var nextAngle = headRot.eulerAngles.y + yRot;
        if (nextAngle < 180)
        {
            //so it is necessary to seperate the amount of angle that go in to the head rotation 
            //and boby rotation(if the angle exceed maxAngle of Head Rotation)
            if (nextAngle > MaxAngleOnHeadRotation)
            {
                var leftoverAngle = (nextAngle - MaxAngleOnHeadRotation);
                var angleUpToLimit = yRot - leftoverAngle;

                headRot *= Quaternion.Euler(0f, angleUpToLimit, 0f);
                actualHeadRot *= Quaternion.Euler(0f, angleUpToLimit, 0f);
                headTr.localRotation = actualHeadRot;

                tr.localRotation *= Quaternion.Euler(0, leftoverAngle, 0f);

            }
            else
            {
                headRot *= Quaternion.Euler(0f, yRot, 0f);
                actualHeadRot *= Quaternion.Euler(0f, yRot, 0f);
                headTr.localRotation = actualHeadRot;

            }
        }
        else if (nextAngle > 180)
        {
            if (nextAngle < (360 + MinAngleOnHeadRotation))
            {
                var leftoverAngle = (nextAngle - (360 + MinAngleOnHeadRotation));
                var angleUpToLimit = yRot - leftoverAngle;
                headRot *= Quaternion.Euler(0f, angleUpToLimit, 0f);
                actualHeadRot *= Quaternion.Euler(0f, angleUpToLimit, 0f);
                headTr.localRotation = actualHeadRot;

                tr.localRotation *= Quaternion.Euler(0, leftoverAngle, 0f);

            }
            else
            {
                headRot *= Quaternion.Euler(0, yRot, 0);
                actualHeadRot *= Quaternion.Euler(0f, yRot, 0f);
                headTr.localRotation = actualHeadRot;
            }
        }
    }

    private void RotateAvatarTowardSight()
    {
        tr.localRotation= Quaternion.Slerp(tr.localRotation, Quaternion.LookRotation(cameraRigTr.forward), Time.deltaTime);
    }//tmr

    #endregion
}

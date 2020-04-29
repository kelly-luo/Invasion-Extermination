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
        if ((xSpeed > 0) || (zSpeed > 0))
            this.RotateAvatarTowardSight();// minus the amount of the value of angle to the head to lotate the body while moving the body . 
    }

    #region CharacterBodyMove

    private void RotateNeck()
    {
        //you might be wondering why did i put the x on z axis 
        //it is because the I found that local Transform of the Neck bone in model was reversed. 
        neckTr.localRotation = Quaternion.Euler(0f, 0f, cameraTr.localRotation.eulerAngles.x);

    }

    private void RotateHeadAndAvatar(float yRot)
    {
        // this part because animation changes the head position in Update so i have to
        // save the rotation of head on actualHeadRot value and use it as head rotation.
        headTr.localRotation = actualHeadRot;
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
                headTr.localRotation *= Quaternion.Euler(0f, angleUpToLimit, 0f);
                actualHeadRot = headTr.localRotation;

                tr.localRotation *= Quaternion.Euler(0, leftoverAngle, 0f);

            }
            else
            {
                headRot *= Quaternion.Euler(0f, yRot, 0f);
                headTr.localRotation *= Quaternion.Euler(0f, yRot, 0f);
                actualHeadRot = headTr.localRotation;

            }
        }
        else if (nextAngle > 180)
        {
            if (nextAngle < (360 + MinAngleOnHeadRotation))
            {
                var leftoverAngle = (nextAngle - (360 + MinAngleOnHeadRotation));
                var angleUpToLimit = yRot - leftoverAngle;
                headRot *= Quaternion.Euler(0f, angleUpToLimit, 0f);
                headTr.localRotation *= Quaternion.Euler(0f, angleUpToLimit, 0f);
                actualHeadRot = headTr.localRotation;

                tr.localRotation *= Quaternion.Euler(0, leftoverAngle, 0f);

            }
            else
            {
                headRot *= Quaternion.Euler(0, yRot, 0);
                headTr.localRotation *= Quaternion.Euler(0f, yRot, 0f);
                actualHeadRot = headTr.localRotation;
            }
        }
    }

    private void RotateAvatarTowardSight()
    {
        var bodyRot = tr.localRotation;

        tr.localRotation = Quaternion.Slerp(tr.localRotation, Quaternion.LookRotation(cameraRigTr.forward), Time.deltaTime);
        //take the amount of change in body rotation 
        var ChangeRotBody = tr.localRotation.eulerAngles.y - bodyRot.eulerAngles.y;
        //add to Head Rotation
        actualHeadRot *= Quaternion.Euler(0f, -ChangeRotBody, 0f);
    }

    #endregion
}

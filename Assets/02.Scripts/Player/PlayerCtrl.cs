using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{

    #region Movement Values
    public float MoveSpeed { get; set; } = 2.5f;
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

    public float MaxAngleOnHeadRotation { get; set; } = 90f;
    public float MinAngleOnHeadRotation { get; set; } = -90f;

    private Transform neckTr;
    private Transform headTr;

    #endregion



    private Transform tr;
    private IUserInputManager userInput;

    void Awake()
    {
        neckTr = GameObject.Find("Neck").transform;
        headTr = GameObject.Find("Head").transform;
    }

    void Start()
    {
        tr = GetComponent<Transform>();
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
        MoveCharacter();
        RotateNeck();
        RotateHeadAndAvatar(cameraCtrl.YRot);
    }

    private void MoveCharacter()
    {
        Vertical = userInput.GetAxis("Vertical");
        Horizontal = userInput.GetAxis("Horizontal");

        Vector3 moveDir = (cameraRigTr.forward * Vertical) + (cameraRigTr.right * Horizontal);

        tr.Translate(moveDir.normalized * MoveSpeed * Time.deltaTime,Space.World); 
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
        Debug.Log("headRot " + headRot.eulerAngles.y.ToString());
        if (nextAngle < 180)
        {
            //so it is necessary to seperate the amount of angle that go in to the head rotation 
            //and boby rotation(if the angle exceed maxAngle of Head Rotation)
            if (nextAngle > MaxAngleOnHeadRotation)
            {
                var leftoverAngle = (nextAngle - MaxAngleOnHeadRotation);
                var angleUpToLimit = yRot - leftoverAngle;

                headRot *= Quaternion.Euler(0f, angleUpToLimit, 0f);
                headTr.localRotation *= Quaternion.Euler(0, angleUpToLimit, 0);

                tr.localRotation *= Quaternion.Euler(0, leftoverAngle, 0f);

            }
            else
            {
                headRot *= Quaternion.Euler(0f, yRot, 0f);
                headTr.localRotation *= Quaternion.Euler(0, yRot, 0);

            }
        }
        else if (nextAngle > 180)
        {
            if (nextAngle < (360 + MinAngleOnHeadRotation))
            {
                var leftoverAngle = (nextAngle - (360 + MinAngleOnHeadRotation));
                var angleUpToLimit = yRot - leftoverAngle;
                headRot *= Quaternion.Euler(0f, angleUpToLimit, 0f);
                headTr.localRotation *= Quaternion.Euler(0, angleUpToLimit, 0);

                tr.localRotation *= Quaternion.Euler(0, leftoverAngle, 0f);

            }
            else
            {
                headRot *= Quaternion.Euler(0, yRot, 0);
                headTr.localRotation *= Quaternion.Euler(0, yRot, 0);
            }
        }
    }

    private void RotateAvatarTowardSight()
    {

    }//tmr

    #endregion
}

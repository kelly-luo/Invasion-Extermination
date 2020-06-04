using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTranslate : ICharacterTranslate
{
    public Transform Character { get; set; }
    public Rigidbody CharacterRigidbody { get; set; }
    public Vector3 DesiredPosition { get; set; }
    private CapsuleCollider collider;
    public float Speed { get; set; } = 1.5f;
    public float JumpForce { get; set; } = 7;

    public Vector3 MoveDirection { get; set; }

    public int GroundLayerMask { get; set; }

    public IUnityServiceManager UnityService { get; set; } = UnityServiceManager.Instance;

    private bool isRunning = false;
    public bool IsRunning
    {
        get
        {
            return isRunning;
        }
        set
        {
            if (value)
            {
                Speed = 4.5f;
            }
            else
            {
                Speed = 1.5f;
            }
        }
    }

    private bool isSitting = false;
    public bool IsSitting
    {
        get
        {
            return isSitting;
        }
        set
        {
            if (value)
            {
                Speed = 1.5f;
            }

        }
    }

    public bool IsJumping { get; set; } = false;
    private float speedDuringJump = 0.1f;

    public PlayerTranslate(Transform character)
    {
        this.Character = character;
        this.UnityService = UnityServiceManager.Instance;
        this.SetGroundLayer();
        if (character != null)
        {
            this.CharacterRigidbody = character.gameObject.GetComponent<Rigidbody>();
            this.collider = character.gameObject.GetComponent<CapsuleCollider>();
        }
    }

    public void TranslateCharacter(Vector3 moveDir)
    {
        if (CharacterRigidbody != null)
        {
            CheckCharacterIsOnGround();
            MoveDirection = moveDir;
            var actualSpeed = this.Speed;
            if (IsJumping)
                actualSpeed = speedDuringJump;
            DesiredPosition = Character.position + moveDir.normalized * actualSpeed * UnityService.DeltaTime;
            CharacterRigidbody.MovePosition(DesiredPosition);
        }
    }

    public bool JumpCharacter(bool IsCheckingGround)
    {
        bool jumpSuccess = false;
        if (IsCheckingGround)
            if (!CheckCharacterIsOnGround())
                return jumpSuccess;


        CharacterRigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        IsJumping = true;
        jumpSuccess = true;
        return jumpSuccess;
    }
    
    public bool CheckCharacterIsOnGround()
    {

        if (collider != null)
        {
            IsJumping = !Physics.CheckCapsule(collider.bounds.center, new Vector3(collider.bounds.center.x, collider.bounds.min.y, collider.bounds.center.z),
                collider.radius * 0.9f, GroundLayerMask);
            return !IsJumping;
        }
        else
            return false;
    }

    private void SetGroundLayer()
    {
        var enemyLayer = LayerMask.NameToLayer("Enemy");
        var obstacleLayer = LayerMask.NameToLayer("Obstacle");
        GroundLayerMask = (1 << enemyLayer) | (1 << obstacleLayer);
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : CharacterMovement
{




    private float horizontalMove = 0f;


    [Header("Jump")]
    [SerializeField] private float jumpCoyoteTime;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpBufferTime;
    [SerializeField] private float jumpCutMultiplier;
    [SerializeField] private float gravityScale;
    [SerializeField] private float fallGravityMultiplier;



    [HideInInspector]
    public float lastGroundedTime;
    private float lastJumpInputTime;
    private bool isJumping;
    private bool isJumpCut;

    public PlayerInputActions playerControls;
    private InputAction move;
    private InputAction jump;


    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }


    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
        jump = playerControls.Player.Jump;
        jump.Enable();
    }

    private void OnDisable()
    {
        jump.Disable();
        move.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = move.ReadValue<Vector2>().x;
        
        if (jump.WasPerformedThisFrame())
        {
            lastJumpInputTime = jumpBufferTime;
        }
        if (jump.WasReleasedThisFrame())
        {
            isJumpCut = true;
        }
    }

    private void FixedUpdate()
    {
        #region run


        if (!IsActing())
        {
            MoveInDirection(horizontalMove);
        }
        

        #endregion

        #region groundedcheck


       if (CheckPointIsGrounded(groundCheckPoint.position))
        {
            lastGroundedTime = jumpCoyoteTime;
            characterAnimator.SetGrounded(true);
        }


        #endregion


        #region Jump

        if (lastGroundedTime > 0 && lastJumpInputTime > 0 && !isJumping && !IsActing())
        {
            Jump();
        }

        #endregion

        #region Jump Cut

        if (isJumpCut)
        {
            OnJumpRelease();
        }

        #endregion


        #region friction

        if (lastGroundedTime > 0 && (Mathf.Abs(horizontalMove) < 0.01f | IsActing()))
        {
            ApplyFriction();
        }

        #endregion

        #region Jump Gravity

        if (rigidbody.velocity.y < 0)
        {
            rigidbody.gravityScale = gravityScale * fallGravityMultiplier;
        }
        else
        {
            rigidbody.gravityScale = gravityScale;
        }

        #endregion

        #region timer

        lastGroundedTime -= Time.deltaTime;
        lastJumpInputTime -= Time.deltaTime;
        if (isJumping && rigidbody.velocity.y <= 0)
        {
            isJumping = false;
        }


        #endregion


    }


    private void Jump()
    {
        rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        lastGroundedTime = 0;
        lastJumpInputTime = 0;
        isJumping = true;
        characterAnimator.SetGrounded(false);
        characterAnimator.Jump();
    }

    private void OnJumpRelease()
    {
        if (rigidbody.velocity.y > 0 && isJumping)
        {
            rigidbody.AddForce(Vector2.down * rigidbody.velocity.y * (1 * jumpCutMultiplier), ForceMode2D.Impulse);
        }
        lastJumpInputTime = 0;
        isJumpCut = false;
    }







}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("References")]
    public CharacterAnimator characterAnimator;
    [SerializeField] protected List<CharacterAction> characterActions;
    public SpriteRenderer spriteRenderer;
    public new Rigidbody2D rigidbody;




    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float decceleration;
    [SerializeField] private float frictionAmount;
    [SerializeField] private float velPower;


    [Header("Ground")]
    public Transform groundCheckPoint;
    [SerializeField] private Vector2 groundCheckSize;
    public LayerMask groundLayerMask;

    public void MoveInDirection(float horizontalMove)
    {
        if (Math.Abs(horizontalMove) != 0f)
        {
            spriteRenderer.flipX = horizontalMove < 0f;
        }

        characterAnimator.SetIsWalking(horizontalMove != 0f);
        float targetSpeed = horizontalMove * moveSpeed;

        float speedDiff = targetSpeed - rigidbody.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, velPower) * Mathf.Sign(speedDiff);

        rigidbody.AddForce(movement * Vector2.right);
    }

    public void ApplyFriction()
    {
        float amount = Mathf.Min(Mathf.Abs(rigidbody.velocity.x), frictionAmount);
        amount *= Mathf.Sign(rigidbody.velocity.x);
        rigidbody.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
    }

    public bool IsActing()
    {
        foreach (CharacterAction characterAction in characterActions)
        {
            if (characterAction.isActing)
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckPointIsGrounded(Vector3 position)
    {
        return Physics2D.OverlapBox(position, groundCheckSize, 0, groundLayerMask) != null;
    }


}

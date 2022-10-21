using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : CharacterMovement
{
    [Header("Enemy")]
    [SerializeField] private List<BehaviorPattern> behaviors;
    public Transform playerTransform;

    private void Awake()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
    }


    private void FixedUpdate()
    {

        if (!IsActing())
        {
            if (!CheckGrounded())
            {
                return;
            }

            foreach (BehaviorPattern behavior in behaviors)
            {
                behavior.InactiveBehavior();
            }

            foreach (BehaviorPattern behavior in behaviors)
            {
                if (behavior.isActive)
                {
                    behavior.ActiveBehavior();
                    return;
                }
            }
        }
        else
        {
            ApplyFriction();
        }

    }

    private bool CheckGrounded()
    {
        if (CheckPointIsGrounded(groundCheckPoint.position))
        {
            return true;
        }
        return false;
    }
}

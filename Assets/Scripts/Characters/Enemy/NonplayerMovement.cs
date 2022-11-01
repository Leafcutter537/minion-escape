using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonplayerMovement : CharacterMovement
{
    [Header("Behaviors")]
    [SerializeField] private List<BehaviorPattern> behaviors;

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

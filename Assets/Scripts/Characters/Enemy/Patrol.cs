using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Patrol : BehaviorPattern
{

    [Header("Patrol")]
    [SerializeField] private float groundCheckOffset;
    [SerializeField] private float bodyCheckOffset;
    [SerializeField] private Vector2 bodyCheckBoxSize;
    [SerializeField] private Transform bodyCheckCenter;
    [SerializeField] private LayerMask obstacleLayer;
    private Collider2D myCollider;

    private bool isFacingRight;

    private void Awake()
    {
        isActive = true;
        isFacingRight = true;
        myCollider = GetComponent<Collider2D>();
    }

    public override void ActiveBehavior()
    {
        bool patrolBlocked = CheckPatrolBlocked(isFacingRight);
        float moveDirection = isFacingRight ? 1 : -1;
        if (patrolBlocked & enemyMovement.CheckPointIsGrounded(enemyMovement.groundCheckPoint.position))
        {
            if (!CheckPatrolBlocked(!isFacingRight))
            {
                isFacingRight = !isFacingRight;
                moveDirection *= -1;
            }
            else
            {
                moveDirection = 0;
            }
        }
        enemyMovement.spriteRenderer.flipX = !isFacingRight;
        enemyMovement.MoveInDirection(moveDirection);

    }

    private bool CheckPatrolBlocked(bool checkToRight)
    {
        Vector3 groundCheckPoint = enemyMovement.groundCheckPoint.position;
        groundCheckPoint.x += checkToRight ? groundCheckOffset : -groundCheckOffset;
        Vector3 bodyCheckPoint = bodyCheckCenter.position;
        bodyCheckPoint.x += checkToRight ? bodyCheckOffset : -bodyCheckOffset;
        Collider2D obstacleCollision = Physics2D.OverlapBox(bodyCheckPoint, bodyCheckBoxSize, 0f, obstacleLayer);
        bool isObstacle = false;
        if (obstacleCollision != null)
        {
            if (obstacleCollision != myCollider)
            {
                isObstacle = true;
            }
        }
        return !enemyMovement.CheckPointIsGrounded(groundCheckPoint) | isObstacle;
    }

    public override void InactiveBehavior()
    {
    }

}

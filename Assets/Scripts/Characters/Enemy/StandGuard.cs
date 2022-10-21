using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandGuard : BehaviorPattern, iMetamorphosisTransfer
{

    public Transform guardPoint;
    public bool faceLeftDefault;
    private bool isInGuardPoint;

    private void Awake()
    {
        isActive = true;
    }

    public override void ActiveBehavior()
    {
        if (isInGuardPoint)
        {
            enemyMovement.spriteRenderer.flipX = faceLeftDefault;
            enemyMovement.MoveInDirection(0);
            enemyMovement.ApplyFriction();
        }
        else
        {
            float direction = guardPoint.position.x > transform.position.x ? 1 : -1;
            enemyMovement.MoveInDirection(direction);
        }
    }

    public override void InactiveBehavior()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == guardPoint)
        {
            isInGuardPoint = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform == guardPoint)
        {
            isInGuardPoint = false;
        }
    }

    public void Transfer(GameObject newObject)
    {
        StandGuard newStandGuard = newObject.GetComponent<StandGuard>();
        newStandGuard.guardPoint = this.guardPoint;
        newStandGuard.faceLeftDefault = this.faceLeftDefault;
    }
}

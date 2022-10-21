using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : AggroBehavior
{


    [SerializeField] private CharacterAction attackAction;
    private List<Transform> targets;

    private void Awake()
    {
        targets = new List<Transform>();
    }

    public override void ActiveBehavior()
    {
        bool targetIsToLeft = transform.position.x - targets[0].position.x > 0;
        enemyMovement.spriteRenderer.flipX = targetIsToLeft;
        enemyMovement.rigidbody.velocity = Vector3.zero;
        attackAction.StartAction();
    }

    public override void InactiveBehavior()
    {
    }

    public override void Aggro(Transform target)
    {
        isActive = true;
        targets.Add(target);
    }

    public override void Deaggro(Transform target)
    {
        targets.Remove(target);
        if (targets.Count == 0)
        {
            isActive = false;
        }
    }
}

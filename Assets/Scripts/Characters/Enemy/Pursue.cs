using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : AggroBehavior, iMetamorphosisTransfer
{



    private List<Transform> targets;
    [SerializeField] private AggroField _pursueField;
    public AggroField pursueField { 
        get
        {
            return _pursueField;
        }
        set
        {
            value.SubscribeAggroBehavior(this);
            _pursueField = value;
        }
    }

    private void Awake()
    {
        targets = new List<Transform>();
    }

    public override void ActiveBehavior()
    {
        bool targetIsToLeft = transform.position.x - targets[0].position.x > 0;
        enemyMovement.spriteRenderer.flipX = targetIsToLeft;
        float direction = targetIsToLeft ? -1 : 1;
        enemyMovement.MoveInDirection(direction);
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

    public void Transfer(GameObject newObject)
    {
        Pursue newPursue = newObject.GetComponent<Pursue>();
        newPursue.pursueField = this.pursueField;
    }
}

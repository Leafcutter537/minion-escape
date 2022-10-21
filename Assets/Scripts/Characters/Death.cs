using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : CharacterAction
{

    protected override void EndAction()
    {
        base.EndAction();
        Destroy(gameObject);
    }

    public override void StartAction()
    {
        GetComponent<Collider2D>().enabled = false;
        rigidbody.gravityScale = 0;
        base.StartAction();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroField : MonoBehaviour
{

    public AggroBehavior aggroBehavior;



    public void SubscribeAggroBehavior(AggroBehavior aggroBehavior)
    {
        this.aggroBehavior = aggroBehavior;
        
        ContactFilter2D contactFilter = new ContactFilter2D();
        
        Collider2D[] results = new Collider2D[10];
        GetComponent<Collider2D>().OverlapCollider(contactFilter, results);
        
        
        foreach (Collider2D c in results)
        {
            if (c != null)
                if (((aggroBehavior.aggroTargets.value & 1 << c.gameObject.layer) == 1 << c.gameObject.layer))
                    if (c.gameObject != aggroBehavior.gameObject)
                        aggroBehavior.Aggro(c.transform);
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((aggroBehavior.aggroTargets.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            if (aggroBehavior != null)
            {
                if (collision.gameObject != aggroBehavior.gameObject)
                    aggroBehavior.Aggro(collision.transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((aggroBehavior.aggroTargets.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            if (aggroBehavior != null)
                aggroBehavior.Deaggro(collision.transform);
        }
    }

}

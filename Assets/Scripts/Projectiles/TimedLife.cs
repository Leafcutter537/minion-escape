using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedLife : MonoBehaviour
{

    [SerializeField] private GameObject replacementObject;

    public void EndOfAnimation()
    {
        if (replacementObject != null)
        {
            Instantiate(replacementObject, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

}

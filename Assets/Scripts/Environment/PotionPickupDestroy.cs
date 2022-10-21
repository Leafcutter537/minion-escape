using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionPickupDestroy : MonoBehaviour
{
    [SerializeField] private PotionPickup potionPickup;

    public void NextStepAnimation()
    {
        Destroy(potionPickup.gameObject);
    }
}

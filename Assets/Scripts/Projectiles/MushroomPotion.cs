using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomPotion : PotionProjectile
{
    [SerializeField] private GameObject seedPrefab;

    protected override void PotionEffect()
    {
        Instantiate(seedPrefab, transform.position, Quaternion.identity);
    }
}

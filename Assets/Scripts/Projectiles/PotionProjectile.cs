
using System.Collections.Generic;
using UnityEngine;

public class PotionProjectile : MonoBehaviour
{


    [SerializeField] private Vector3 explosionOffset;
    [SerializeField] private GameObject exposionPrefab;
    [SerializeField] private float explosionRadius;
    public PotionPickup.PotionType potionType;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(exposionPrefab, transform.position + explosionOffset, Quaternion.identity);
        PotionEffect();
        Destroy(gameObject);
    }

    protected virtual void PotionEffect()
    {
        Collider2D[] colliders;
        switch (potionType)
        {
            case (PotionPickup.PotionType.Acidic):
                colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
                foreach (Collider2D collider in colliders)
                {
                    Meltable melt = collider.GetComponent<Meltable>();
                    if (melt != null)
                    {
                        melt.Melt();
                    }
                }
                break;
            case (PotionPickup.PotionType.Rage):
                colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
                foreach (Collider2D collider in colliders)
                {
                    EnemyEnrage enrage = collider.GetComponent<EnemyEnrage>();
                    if (enrage != null)
                    {
                        enrage.Enrage();
                        return;
                    }
                }
                break;
        }
    }


}

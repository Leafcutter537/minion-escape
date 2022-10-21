using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MushroomSeed : MonoBehaviour
{

    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private Vector2 initialVelocity;
    [SerializeField] private float lifeTime;
    [SerializeField] private GameObject growingMushroomPrefab;
    [SerializeField] private Vector3 growOffset;
    private float timeLeft;

    private void Awake()
    {
        timeLeft = lifeTime;
        rigidbody.velocity = initialVelocity;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(growingMushroomPrefab, transform.position + growOffset, Quaternion.identity);
        Destroy (this.gameObject);
    }

}

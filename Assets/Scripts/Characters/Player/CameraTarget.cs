using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float lookDistance;
    [SerializeField] private float panSpeed;
    public Vector2 lookDirection;

    private void Update()
    {
        Vector3 targetPosition = player.transform.position;
        targetPosition.x += lookDirection.x * lookDistance;
        targetPosition.y += lookDirection.y * lookDistance;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * panSpeed);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryIndicator : MonoBehaviour
{
    public Vector2 startingVelocity;
    public float gravity;

    [Header("Prefabs")]
    [SerializeField] private GameObject dotPrefab;

    [Header("Dots")]
    [SerializeField] private float timeBetweenDots;
    [SerializeField] private float numDots;

    private List<GameObject> dots;


    public void CreateDots()
    {
        dots = new List<GameObject>();
        for (int i = 0; i < numDots; i++)
        {
            dots.Add(Instantiate(dotPrefab, transform.position, Quaternion.identity));
        }
    }

    public void SetDotPosition()
    {
        for (int i = 0; i < numDots; i++)
        {
            float time = i * timeBetweenDots;
            Vector2 position = GetPosition(time);
            dots[i].transform.position = position;
        }
    }

    private Vector2 GetPosition(float time)
    {
        float x = transform.position.x + startingVelocity.x * time;
        float y = transform.position.y + startingVelocity.y * time + 0.5f * gravity * Mathf.Pow(time, 2);
        return new Vector2(x, y);
    }

    private void OnDestroy()
    {
        dots.ForEach(dot => Destroy(dot));
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedLife : MonoBehaviour
{

    [SerializeField] private GameObject replacementObject;
    [SerializeField] private float durationAfterAnimationEnd;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public float timeLeft;

    private void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void EndOfAnimation()
    {
        if (durationAfterAnimationEnd == 0)
            Destroy(gameObject);
        else
        {
            spriteRenderer.color = new Color(1, 1, 1, 0);
            timeLeft = durationAfterAnimationEnd;
        }
    }

    private void OnDestroy()
    {
        if (replacementObject != null)
        {
            Instantiate(replacementObject, transform.position, Quaternion.identity);
        }
    }

}

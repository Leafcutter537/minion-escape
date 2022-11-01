using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appear : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void FinishAppearing()
    {
        Color tempColor = spriteRenderer.color;
        tempColor.a = 1;
        spriteRenderer.color = tempColor;
    }
}

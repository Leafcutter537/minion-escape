using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{

    [SerializeField] private GameObject victoryPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            victoryPanel.gameObject.SetActive(true);
        }
    }
}

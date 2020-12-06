using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScreenBox : MonoBehaviour
{
    private MasterControl gameControl;

    private void Start()
    {
        gameControl = FindObjectOfType<MasterControl>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameControl.NextScreen();
        }
    }
}

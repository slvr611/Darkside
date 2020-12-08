using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxScript : MonoBehaviour
{
    public bool isMoving;
    private SoundManager boxSound;

    private void Start()
    {
        boxSound = FindObjectOfType<SoundManager>();
        isMoving = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            boxSound.PlayBox();
            isMoving = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            boxSound.StopBox();
            isMoving = false;
        }
    }
}

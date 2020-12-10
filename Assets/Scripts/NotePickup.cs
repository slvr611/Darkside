using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePickup : MonoBehaviour
{
    public GameObject noteToShow;
    public bool isNear;

    private void Start()
    {
        isNear = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            if (isNear)
            {
                if (noteToShow != null)
                {
                    noteToShow.SetActive(true);
                    Destroy(gameObject);
                }
                else
                {
                    GetComponent<AudioSource>().Play();
                    GetComponent<ParticleSystem>().Stop();
                }
                
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isNear = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isNear = false;
    }
}

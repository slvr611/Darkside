using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamChildOfElevator : MonoBehaviour
{
    public GameObject elevator;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cam.transform.parent = elevator.transform;
            cam.transform.position = elevator.transform.position;
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + 10, -10);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvelatorButton : MonoBehaviour
{
    public GameObject elevator;
    public GameObject door;

    private void Start()
    {
        elevator = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        elevator.SendMessage("goUp");
        door.SetActive(true);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvelatorButton : MonoBehaviour
{
    public GameObject elevator;

    private void Start()
    {
        elevator = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        elevator.SendMessage("goUp");
    }
}

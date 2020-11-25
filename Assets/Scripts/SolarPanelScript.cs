﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarPanelScript : MonoBehaviour
{
    public bool isPowered;
    public float powerDownTimer;
    public float maxTimer = .5f;

    // Start is called before the first frame update
    void Start()
    {
        isPowered = false;
        powerDownTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (powerDownTimer <= 0)
        {
            isPowered = false;
        }
        else
        {
            powerDownTimer -= Time.deltaTime;
        }
    }

    public void givePower()
    {
        powerDownTimer = maxTimer;
        isPowered = true;
    }
}

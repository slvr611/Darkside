﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningBarScrift : MonoBehaviour
{
    public float spinSpeed = 5;

    public void Spin()
    {
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
    }
}

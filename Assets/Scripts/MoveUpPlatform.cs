using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpPlatform : MonoBehaviour
{
    public float speed = 5; 

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }
}

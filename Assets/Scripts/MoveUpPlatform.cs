using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpPlatform : MonoBehaviour
{
    public float speed = 5;
    public float maxYPos = 4;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < maxYPos)
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
        
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDownPlatform : MonoBehaviour
{
    public float speed = 3;
    public float minimumYPos = -15.8f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > minimumYPos)
        {
            transform.position += -transform.up * speed * Time.deltaTime;
        }
        
    }

    public void stopPlatform()
    {
        speed = 0;
    }

    public void resumePlatform()
    {
        speed = 1;
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        print("whatchit: " + collision.gameObject.name);
        //-14.5 and -13

        if (transform.position.y > -14.5f && transform.position.y < -13 && collision.gameObject.CompareTag("Ground"))
        {
            speed = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        print("seeya: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Ground"))
        {
            speed = 5;
        }
    }*/
}

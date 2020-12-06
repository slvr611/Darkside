using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomStopBox : MonoBehaviour
{
    private MoveDownPlatform platform;

    private void Start()
    {
        platform = transform.parent.gameObject.GetComponent<MoveDownPlatform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //print("whatchit: " + collision.gameObject.name);
        //-14.5 and -13

        if (collision.gameObject.CompareTag("Ground"))
        {
            platform.stopPlatform();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //print("seeya: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Ground"))
        {
            platform.resumePlatform();
        }
    }
}

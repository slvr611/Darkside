using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftPlatform : MonoBehaviour
{
    public float speed = 3;
    public float minXPos = -15.8f;
    private SoundManager platSound;

    void Start()
    {
        platSound = FindObjectOfType<SoundManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > minXPos)
        {
            transform.position += -transform.right * speed * Time.deltaTime;

        }

    }

    public void stopPlatform()
    {
        speed = 0;
        platSound.StopPlatform();

    }

    public void resumePlatform()
    {
        speed = 1;
        if (platSound != null)
        {
            platSound.PlayPlatform();
        }
    }
}

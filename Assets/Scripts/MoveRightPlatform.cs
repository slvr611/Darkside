using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightPlatform : MonoBehaviour
{
    public float speed = 3;
    public float maxXPos = -15.8f;
    private SoundManager platSound;

    void Start()
    {
        platSound = FindObjectOfType<SoundManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < maxXPos)
        {
            transform.position += transform.right * speed * Time.deltaTime;

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

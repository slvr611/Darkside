using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningBarScrift : MonoBehaviour
{
    public float spinSpeed = 5;
     private SoundManager barSound;

      void Start()
    {
        barSound = FindObjectOfType<SoundManager>();
        barSound.PlayPlatform();
    }

    private void Update()
    {
        Spin();    
    }

    public void Spin()
    {
        transform.Rotate(0, 0, -spinSpeed * Time.deltaTime);
        
    }

    private void OnDisable()
    {
        barSound.StopPlatform();
    }

    private void OnEnable()
    {
        barSound.PlayPlatform();
    }
}
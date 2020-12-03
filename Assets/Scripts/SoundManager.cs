using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   public AudioSource[] mySounds;
   private AudioSource elevator;
    private AudioSource platform;
    private AudioSource reflector;
    private AudioSource laser;

    // Start is called before the first frame update
    void Start()
    {

        mySounds = GetComponents<AudioSource>();
        elevator = mySounds[0];
        platform = mySounds[1];
        reflector = mySounds[2];
        laser = mySounds[3];
    }

    public void PlayElevator() {
        elevator.Play();
        print("played");
    }

    public void PlayPlatform() {
        platform.Play();
    }

     public void PlayReflector() {
        reflector.Play();
    }

    public void PlayLaser() {
        laser.Play();
    }

    

    }

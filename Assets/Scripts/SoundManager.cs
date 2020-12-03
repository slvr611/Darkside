using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   public AudioSource[] mySounds;
   public AudioSource[] mySounds2;
   public AudioSource[] mySounds3;
   private AudioSource elevator;
    private AudioSource platform;
    private AudioSource laser;
    private AudioSource reflector;
    private AudioSource death;

    // Start is called before the first frame update
    void Start()
    {

        mySounds = GetComponents<AudioSource>();
        mySounds2 = GetComponents<AudioSource>();
        mySounds3 = GetComponents<AudioSource>();
        elevator = mySounds[0];
        platform = mySounds2[0];
        laser = mySounds3[0];
        reflector = mySounds3[1];
        death = mySounds3[2];
        

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
    public void PlayDeath() {
        death.Play();
    }

    

    }

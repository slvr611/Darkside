using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   public AudioSource[] mySounds;

   private AudioSource elevator;
    private AudioSource platform;
    private AudioSource laser;
    private AudioSource reflector;
    private AudioSource death;
    private AudioSource mirrorPlacement;
    private AudioSource setMirror;
    private AudioSource box;

    // Start is called before the first frame update
    void Start()
    {
        //elevator = mySounds[0];
        //platform = mySounds[1];
        //laser = mySounds[2];
        //reflector = mySounds[2];
        //death = mySounds[4];
        //mirrorPlacement = mySounds[5];
        //setMirror = mySounds[6];
        // box = mySounds[7];

        mySounds = FindObjectsOfType<AudioSource>();
        

    }

    public void PlayElevator() {
        elevator.Play();
        //print("played");
    }

    public void PlaySetMirror() {
        setMirror.Play();
    }

    public void PlayPlatform() {
       // print("platform sound");
        platform.Play();
    }

       public void PlayMirrorPlacement() {
       // print("platform sound");
        mirrorPlacement.Play();
    }

    public void StopPlatform()
    {
        //print("platform stop");
        platform.Stop();
    }

     public void StopBox()
    {
        box.Stop();
    }
    public void PlayBox() {
       // print("platform sound");
        box.Play();
    }

     public void PlayReflector() {
        reflector.Play();
    }

    public void StopReflector()
    {
        reflector.Stop();
    }

    public void PlayLaser() {
        //laser.Play();
    }
    public void PlayDeath() {
        //death.Play();
    }

    public void refresh()
    {
        Start();
    }

    }

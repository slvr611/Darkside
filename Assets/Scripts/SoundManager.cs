using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   public AudioSource[] mySounds;
    public AudioSource elevator;

    // Start is called before the first frame update
    void Start()
    {

        mySounds = GetComponents<AudioSource>();
        elevator = mySounds[0];
    }

    public void PlayElevator() {
        elevator.Play();
        print("played");
    }

    }

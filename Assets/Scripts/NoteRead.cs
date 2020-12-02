using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteRead : MonoBehaviour
{
    public AudioClip narration;
    
    // Start is called before the first frame update
    void Start()
    {
        //Play picking up page sound effect (or use Audio Source - play on awakw)
        //start playing audio

        //maybe add note to inventory
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Destroy(gameObject);
        }
    }
}

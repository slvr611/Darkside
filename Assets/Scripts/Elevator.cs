using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float elevatorSpeed = 5;
    public bool isMoving;
    private SoundManager soundObj;
    public float maxYPos = 100;
    public bool AutoStart = false;
    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        soundObj = FindObjectOfType<SoundManager>();

        if (AutoStart)
        {
            goUp();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving && transform.position.y<maxYPos)
        {
            transform.position += transform.up * elevatorSpeed * Time.deltaTime;
            
        }
    }

    public void goUp()
    {
        isMoving = true;
        soundObj.PlayElevator();
    }
}

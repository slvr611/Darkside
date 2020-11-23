using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour
{
    public PlayerScript player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerScript>();   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.isOnLadder = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //print("exit");
        player.isOnLadder = false;
        player.rb.gravityScale = 1;
    }
}

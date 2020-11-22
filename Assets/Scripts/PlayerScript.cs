using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float xMovement;
    private float yMovement;
    private float speed = 10;
    private bool isGrounded;
    public bool isAiming;
    public float jumpForce = 10;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position - new Vector3(0f, 1.5f, 0), Vector2.down);
        //print(hit2D.distance);
        xMovement = Input.GetAxisRaw("Horizontal");

        //yMovement = Input.GetAxisRaw("Vertical");

        if (xMovement > 0)
        {
            //face right	
        }
        else if (xMovement < 0)
        {
            //face left
        }


        if (Input.GetKeyDown("w"))
        {
            rb.velocity = (Vector2.up * jumpForce); 
        }

        if (hit2D.distance <= .2){
            isGrounded = true;
        }
        else{
            isGrounded = false;
        }

    transform.position += new Vector3(xMovement, 0, 0) * speed * Time.deltaTime;

    if(Input.GetKey("space")){
            //print();
	    isAiming = true;
    }
    else{
	    isAiming = false;
    }	
        }
}

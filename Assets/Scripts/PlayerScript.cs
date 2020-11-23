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

    public bool isOnLadder;
    public bool isOnMovablePlatform;
    public Rigidbody2D rb;

    public GameObject currentPlatform;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        rb.freezeRotation = true;
        sr = GetComponentInChildren<SpriteRenderer>();

        xMovement = 0;
        yMovement = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position - new Vector3(0f, 1.5f, 0), Vector2.down);
        //print(hit2D.distance);
        xMovement = Input.GetAxisRaw("Horizontal");

        yMovement = Input.GetAxisRaw("Vertical");

        if (xMovement > 0)
        {
            //face right
            sr.flipX = false;
            
        }
        else if (xMovement < 0)
        {
            //face left
            sr.flipX = true;
        }

        if (!isOnLadder)
        {
            yMovement = 0;
            rb.gravityScale = 1;
        }

        if (Input.GetKeyDown("w"))
        {
            if (isOnLadder)
            {
                yMovement = 1;
                rb.gravityScale = 0;
            }
            else
            {
                rb.velocity = (Vector2.up * jumpForce);
                yMovement = 0;

            }
            
        }

        if (hit2D.distance <= .2){
            isGrounded = true;
        }
        else{
            isGrounded = false;
        }

    transform.position += new Vector3(xMovement, yMovement, 0) * speed * Time.deltaTime;

    if(Input.GetKey("space")){
            //print();
            if (isOnMovablePlatform)
            {
                transform.position += transform.right * (speed / 2) * Time.deltaTime;
                currentPlatform.transform.position += transform.right * (speed / 2) * Time.deltaTime;
            }
            else
            {
                isAiming = true;
            }
	    
    }
    else{
	    isAiming = false;
    }

        
 }

    public void killPlayer()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovablePlat"))
        {
            print("is on plat");
            isOnMovablePlatform = true;
            currentPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovablePlat"))
        {
            //print("is on plat");
            isOnMovablePlatform = false;
        }
    }
}

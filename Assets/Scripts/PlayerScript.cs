using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float xMovement;
    public float yMovement;
    private float speed = 10;

    public float yMouseSensitivity;
    public float maxFireRange;

    public Transform topLeft;
    public Transform bottomRight;
    public bool isGrounded;

    public bool isAiming;
    public float jumpForce = 10;

    public bool isOnLadder;
    public bool isOnMovablePlatform;
    public Rigidbody2D rb;

    public bool isMirrorOut = false;
    public GameObject mirror;

    public GameObject currentPlatform;
    public GameObject deathsplosion;

    private SpriteRenderer sr;
    public LineRenderer fireLine;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        rb.freezeRotation = true;
        sr = GetComponentInChildren<SpriteRenderer>();

        xMovement = 0;
        yMovement = 0;

        isMirrorOut = false;
        mirror.SetActive(false);

        fireLine = FindObjectOfType<LineRenderer>();
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
            //rb.simulated = true;
        }
        else
        {
            rb.gravityScale = 0;
            //rb.simulated = false;
        }
        

        Collider2D objectUnder = Physics2D.OverlapArea(topLeft.position, bottomRight.position);

        if (objectUnder != null) {
            if (objectUnder.CompareTag("Ground"))
            {
                isGrounded = true;
                isOnMovablePlatform = false;
            }
            else if (objectUnder.CompareTag("MovablePlat"))
            {
                isGrounded = true;
                isOnMovablePlatform = true;
                currentPlatform = objectUnder.gameObject;
            }
            else
            {
                isGrounded = false;
                isOnMovablePlatform = false;
            }
        }
        else
        {
            isGrounded = false;
            isOnMovablePlatform = false;

        }


        if (Input.GetKeyDown("w") || Input.GetKeyDown("up"))
        {
            if (isOnLadder)
            {
                //rb.simulated = false;
                yMovement = 1;

            }
            else if(isGrounded)
            {
                rb.velocity += Vector2.up * jumpForce;
                //yMovement = 1;
                //yMovement = 0;
            }
            else
            {
                yMovement = 0;
            }
            
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

        if (Input.GetKeyDown("t"))
        {
            if (isMirrorOut)
            {
                isMirrorOut = false;
                mirror.SetActive(false);
            }
            else
            {
                isMirrorOut = true;
                mirror.SetActive(true);
            }
        }

        if (isAiming)
        {
            //fireLine.SetPosition(0, transform.position);
            fireLine.SetPositions(new Vector3[] {transform.position, transform.position + transform.right * 5});
        }

        
 }

    public void killPlayer()
    {
        Instantiate(deathsplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovablePlat"))
        {
            //print("is on plat");
            //isOnMovablePlatform = true;
            //currentPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovablePlat"))
        {
            //print("is on plat");
            //isOnMovablePlatform = false;
        }
    }
}

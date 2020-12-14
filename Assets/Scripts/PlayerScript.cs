using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerScript : MonoBehaviour
{
    public Collider2D objectUnder;
    private Animator animator;

    private float xMovement;
    public float yMovement;
    private float speed = 10;

    public float mouseSensitivity;
    public float MouseX;
    public float MouseY;

    public float angle;
    public float linePointx;
    public float linePointy;
    public float maxFireRange = 5;
    public float mirrorPlaceRange = 5;

    public Transform topLeft;
    public Transform bottomRight;
    public bool isGrounded;

    public bool isAiming;
    public bool isDead;
    public float jumpForce = 10;

    public bool isOnLadder;
    public bool isOnMovablePlatform;
    public Rigidbody2D rb;

    public bool isMirrorOut = false;
    public GameObject mirror;

    public GameObject currentPlatform;
    public GameObject deathsplosion;
    public GameObject jumpPS;
    public GameObject projectile;
    public GameObject powerTriangle;

    private SpriteRenderer sr;
    public LineRenderer fireLine;

    private SoundManager sm;

    public event Action OnPlayerDeath;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        rb.freezeRotation = true;
        sr = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();

        xMovement = 0;
        yMovement = 0;

        isMirrorOut = false;
        mirror.SetActive(false);

        jumpForce = 8;

        maxFireRange = 10;
        
        fireLine = FindObjectOfType<LineRenderer>();
        sm = FindObjectOfType<SoundManager>();

        isDead = false;
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
            animator.SetBool("IsWalking", true);
        }
        else if (xMovement < 0)
        {
            //face left
            sr.flipX = true;
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
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
        

        objectUnder = Physics2D.OverlapArea(topLeft.position, bottomRight.position);

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
                Instantiate(jumpPS, transform.position + (Vector3.down * 1.5f), transform.rotation);
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
            if (!mirror.activeSelf)
            {
                powerTriangle.SetActive(true);
            }
                mirror.SetActive(true);
                Destroy(GameObject.FindGameObjectWithTag("Mirror"));

            
        }

        //AIMING AND FIRING MAGIC BEAM TO KNOCK OUT LIGHT
        if (isAiming)
        {
            fireLine.enabled = true;
            //fireLine.SetPosition(0, transform.position);
            MouseY += Input.GetAxis("Mouse Y") * mouseSensitivity;
            MouseX += Input.GetAxis("Mouse X") * mouseSensitivity;

            
            angle = Mathf.Atan2(MouseY, MouseX);
            
            



            linePointx = Mathf.Cos(angle) * maxFireRange;
            linePointy = Mathf.Sin(angle) * maxFireRange;
            fireLine.SetPositions(new Vector3[] {transform.position, transform.position + new Vector3(linePointx, linePointy, 0)});


            if (Input.GetMouseButtonDown(0)) {
                
                Shoot();
            }
        }
        else
        {
            fireLine.enabled = false;
        }

        
 }

    public void killPlayer()
    {
        Instantiate(deathsplosion, transform.position, transform.rotation);

        if (OnPlayerDeath != null && !isDead)
        {
            print("deeeth");
            isDead = true;
            OnPlayerDeath();
        }

        Destroy(gameObject);
    }

    private void Shoot()
    {
        print("pew");
        GameObject pew = Instantiate(projectile, transform.position, transform.rotation);
        pew.SendMessage("setTarget", transform.position + new Vector3(linePointx, linePointy, 0));
    }

    public void mirrorPlaced()
    {
        powerTriangle.GetComponent<Animator>().SetTrigger("Close");
    }

    public void setPosition(Vector3 spot)
    {
        print("setting position");
        transform.position = spot;
    }

    
}

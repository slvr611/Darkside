using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Collider2D objectUnder;

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
    public float jumpForce = 10;

    public bool isOnLadder;
    public bool isOnMovablePlatform;
    public Rigidbody2D rb;

    public bool isMirrorOut = false;
    public GameObject mirror;

    public GameObject currentPlatform;
    public GameObject deathsplosion;
    public GameObject projectile;
    public GameObject powerTriangle;

    private SpriteRenderer sr;
    public LineRenderer fireLine;

    private SoundManager soundPlatform;
    private SoundManager laserSound;
    private SoundManager deathSound;
    private SoundManager mirrorSound;


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
        soundPlatform = FindObjectOfType<SoundManager>();
        laserSound = FindObjectOfType<SoundManager>();
        deathSound = FindObjectOfType<SoundManager>();
         mirrorSound = FindObjectOfType<SoundManager>();
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
                soundPlatform.PlayPlatform();
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
                mirrorSound.PlayMirrorPlacement();

            
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
        deathSound.PlayDeath();
        Destroy(gameObject);
    }

    private void Shoot()
    {
        print("pew");
        GameObject pew = Instantiate(projectile, transform.position, transform.rotation);
        pew.SendMessage("setTarget", transform.position + new Vector3(linePointx, linePointy, 0));
        laserSound.PlayLaser();
    }

    public void mirrorPlaced()
    {
        powerTriangle.GetComponent<Animator>().SetTrigger("Close");
    }
}

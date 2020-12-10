using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableMirror : MonoBehaviour
{
    public int state;
    public Transform player;
    public float placementRangeMax = 5;
    public float placementRange;
    public float mouseSensitivity = 5;

    public float MouseX;
    public float MouseY;
    public float angle;
    public GameObject mirror;
    public GameObject line;
    public GameObject linePrefab;
    public SoundManager setMirrorSound;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        MouseX = 0;
        MouseY = 0;
        placementRange = placementRangeMax;
        setMirrorSound = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            state = 0;
            gameObject.SetActive(false);
            player.gameObject.GetComponentInChildren<PlayerScript>().mirrorPlaced();
        }

        
        MouseY += Input.GetAxis("Mouse Y") * mouseSensitivity;
        MouseX += Input.GetAxis("Mouse X") * mouseSensitivity;
        angle = Mathf.Atan2(MouseY, MouseX);

        if (state == 0)
        {
            //Placing mode
            placementRange += Input.mouseScrollDelta.y;

            if (placementRange < 0)
            {
                placementRange = 0;
            }
            else if(placementRange > placementRangeMax){
                placementRange = placementRangeMax;
            }

            transform.position = player.position + new Vector3(Mathf.Cos(angle) * placementRange, Mathf.Sin(angle) * placementRange, 0);
            transform.rotation = new Quaternion(0,0,0,0);
            if (Input.GetMouseButtonDown(0))
            {
                state += 1;
            }
        }
        else if (state == 1)
        {
            //Rotoating Mode
            RaycastHit hit;

            Physics.Raycast(transform.position, transform.up, out hit, 100);
            Debug.DrawLine(transform.position, hit.point, Color.cyan);

            

            transform.rotation = Quaternion.LookRotation(new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0));
            transform.Rotate(0, 90, 0);

            if (line != null)
            {
                //Destroy(reflectionCube);
                //reflectionCube = GameObject.CreatePrimitive(PrimitiveType.Cube);


                line.transform.position = transform.position + (transform.up * (hit.distance / 2));
                line.transform.rotation = Quaternion.LookRotation(transform.up);
                line.transform.localScale = new Vector3(1, 1, hit.distance);
            }
            else
            {
                line = Instantiate(linePrefab);
                line.GetComponentInChildren<Collider>().enabled = false;

                line.transform.position = transform.position + (transform.up * (hit.distance / 2));
                line.transform.rotation = Quaternion.LookRotation(transform.up);
                line.transform.localScale = new Vector3(1, 1, hit.distance);
            }
            if (Input.GetMouseButtonDown(0))
            {
                //setMirrorSound.PlaySetMirror();
                state += 1;
                Instantiate(mirror, transform.position, transform.rotation).transform.Rotate(0,0,-90);
                player.gameObject.GetComponentInChildren<PlayerScript>().mirrorPlaced();
                
            }
        }
        else if (state == 2)
        {
            
            
            state = 0;
            gameObject.SetActive(false);
            
        }
    }

    private void OnDisable()
    {
        Destroy(line);
    }

    private void OnEnable()
    {
        MouseX = 0;
        MouseY = 0;
    }
}

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

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        MouseX = 0;
        MouseY = 0;
        placementRange = placementRangeMax;
    }

    // Update is called once per frame
    void Update()
    {
        float angle;
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
            if (Input.GetMouseButtonDown(0))
            {
                state += 1;
            }
        }
        else if (state == 1)
        {
            //Rotoating Mode

            transform.rotation = Quaternion.LookRotation(new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0));
            transform.Rotate(0, 90, 0);
            if (Input.GetMouseButtonDown(0))
            {
                state += 1;
            }
        }
        else if (state == 2)
        {
            //reflecting mode

            if (Input.GetMouseButtonDown(1))
            {
                Destroy(gameObject);
            }
        }
    }
}

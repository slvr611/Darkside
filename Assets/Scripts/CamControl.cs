using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public Vector3[] camPositions;
    public int currentCamPosition;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < camPositions.Length; i++)
        {
            camPositions[i].z = transform.position.z;
        }

        if (camPositions.Length <= 0)
        {
            camPositions = new Vector3[1] { transform.position };
        }

        //transform.position = camPositions[0];
        //currentCamPosition = 0;
    }

    public void NextScreen()
    {
        currentCamPosition += 1;
        transform.position = camPositions[currentCamPosition];


    }

    public void setCam()
    {
        transform.position = camPositions[currentCamPosition];
    }

}

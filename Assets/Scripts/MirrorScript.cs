using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorScript : MonoBehaviour
{
    public bool isReflecting;
    public float powerDownTimer;
    public float maxTimer = .5f;

    public Vector2 direction;
    public GameObject reflectionCubePrefab;
    public GameObject reflectionCube;

    // Start is called before the first frame update
    void Start()
    {
        isReflecting = false;
        powerDownTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (powerDownTimer <= 0)
        {
            isReflecting = false;
        }
        else
        {
            powerDownTimer -= Time.deltaTime;
        }

        if (isReflecting)
        {
            RaycastHit hit;

            Physics.Raycast(transform.position, direction, out hit, 100);
            Debug.DrawLine(transform.position, hit.point, Color.cyan);

            Vector3 reflectDirection = new Vector3(direction.x, direction.y, 0);
            if (reflectionCube != null)
            {
                //Destroy(reflectionCube);
                //reflectionCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                

                reflectionCube.transform.position = transform.position + (reflectDirection * (hit.distance/2));
                reflectionCube.transform.rotation = Quaternion.LookRotation(reflectDirection);
                reflectionCube.transform.localScale = new Vector3 (1, 1, hit.distance);
            }
            else
            {
                reflectionCube = Instantiate(reflectionCubePrefab);
                reflectionCube.GetComponentInChildren<Collider>().enabled = false;

                reflectionCube.transform.position = transform.position + (reflectDirection * (hit.distance/2));
                reflectionCube.transform.rotation = Quaternion.LookRotation(reflectDirection);
                reflectionCube.transform.localScale = new Vector3(1, 1, hit.distance);
            }
            

            if (hit.collider.gameObject.CompareTag("SP"))
            {
                hit.collider.gameObject.SendMessage("givePower");
            }
        }
        else
        {
            Destroy(reflectionCube);
        }
    }

    public void reflect(Vector2 fromDirection)
    {
        powerDownTimer = maxTimer;
        isReflecting = true;
        direction = -fromDirection;
        direction = reflectAngle(direction);
    }

    public Vector2 reflectAngle(Vector2 vector)
    {   
        float angleBetween = Vector2.Angle(vector,new Vector2(1,0));
        angleBetween *= Mathf.Deg2Rad;
        Vector2 addAngle = new Vector2(Mathf.Cos(angleBetween/2), Mathf.Sin(angleBetween/2));
        return addAngle;
    }

    private void OnDestroy()
    {
        Destroy(reflectionCube);
    }
}

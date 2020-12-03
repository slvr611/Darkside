using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed;
    public bool start;
    public Vector3 targetPos;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
        speed = 5;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            transform.position += (targetPos - transform.position) * speed * Time.deltaTime;
        }

        if (Vector3.Distance(transform.position, targetPos) < .1)
        {
            timer += Time.deltaTime;

            if (timer >= .5)
            {
                Destroy(gameObject);
            }
        }
    }

    public void setTarget(Vector3 target)
    {
        //print("message received");
        targetPos = target;
        start = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("entered: " + collision.gameObject.name);
        //if you hit a breakable light
        //then break it
        if (collision.gameObject.CompareTag("BLight"))
        {
            print("entered BL");
            collision.gameObject.SendMessage("breakLight");
        }
    }
}

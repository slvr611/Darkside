using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public MasterControl master;
    public GameObject particles;

    private void Start()
    {
        master = FindObjectOfType<MasterControl>();
        particles = transform.GetChild(0).gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            master.setCheckpoint(new Vector3(transform.position.x, transform.position.y, collision.transform.position.z));
            particles.SetActive(true);
            GetComponent<Animator>().SetTrigger("LightUp");
           
        }
    }
}

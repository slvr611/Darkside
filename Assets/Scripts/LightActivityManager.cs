using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightActivityManager : MonoBehaviour
{
    public GameObject[] activateLights;
    public GameObject[] deactivateLights;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
            for (int i = 0; i < activateLights.Length; i++)
            {
                activateLights[i].SetActive(true);
            }

            for (int h = 0; h < deactivateLights.Length; h++)
            {
                deactivateLights[h].SetActive(false);
            }
        //}
    }
    
}

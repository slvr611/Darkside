using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableLight : MonoBehaviour
{
    public GameObject onBulb;
    public GameObject offBulb;
    public GameObject smashPrefab;

    public void breakLight()
    {
        print("bl");
        transform.GetChild(0).gameObject.SetActive(false);
        onBulb.SetActive(false);
        offBulb.SetActive(true);

        Instantiate(smashPrefab, transform.position, transform.rotation);
    }

    public void refreshColliders()
    {
        if (transform.GetChild(0).gameObject.activeSelf)
        {
            transform.GetChild(0).SendMessage("refreshColliders");
        }
        
    }
}

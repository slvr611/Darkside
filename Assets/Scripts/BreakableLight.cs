using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableLight : MonoBehaviour
{
    public GameObject onBulb;
    public GameObject offBulb;

    public void breakLight()
    {
        print("bl");
        transform.GetChild(0).gameObject.SetActive(false);
        onBulb.SetActive(false);
        offBulb.SetActive(true);
    }

    public void refreshColliders()
    {
        transform.GetChild(0).SendMessage("refreshColliders");
    }
}

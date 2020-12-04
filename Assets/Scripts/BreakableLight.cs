using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableLight : MonoBehaviour
{
    public void breakLight()
    {
        print("bl");
        gameObject.SetActive(false);
    }

    public void refreshColliders()
    {
        transform.GetChild(0).SendMessage("refreshColliders");
    }
}

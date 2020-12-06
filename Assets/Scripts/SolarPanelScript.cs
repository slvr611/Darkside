using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarPanelScript : MonoBehaviour
{
    public bool isPowered;
    public float powerDownTimer;
    public float maxTimer = .5f;
    public MonoBehaviour[] scriptsToActivate;
    public MonoBehaviour[] notPoweredScripts;

    // Start is called before the first frame update
    void Start()
    {
        isPowered = false;
        powerDownTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (powerDownTimer <= 0)
        {
            isPowered = false;
            for (int i = 0; i < scriptsToActivate.Length; i++)
            {
                scriptsToActivate[i].enabled = false;
            }
            for (int h = 0; h < notPoweredScripts.Length; h++)
            {
                notPoweredScripts[h].enabled = true;
            }

        }
        else
        {
            powerDownTimer -= Time.deltaTime;
        }

    }

    public void givePower()
    {
        powerDownTimer = maxTimer;
        isPowered = true;

        for (int i = 0; i < scriptsToActivate.Length; i++)
        {
            scriptsToActivate[i].enabled = true;
        }
        for (int h = 0; h < notPoweredScripts.Length; h++)
        {
            notPoweredScripts[h].enabled = false;
        }
    }
}

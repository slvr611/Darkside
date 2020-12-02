using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathsplosion : MonoBehaviour
{
    private float timer;
    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        ps = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 1)
        {
            ps.Stop();
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                Destroy(gameObject);
            }

        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}

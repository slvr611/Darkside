using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopParticlesAfterTime : MonoBehaviour
{
    public float timeStop = 1;
    private float timer;
    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeStop)
        {
            ps.Stop();

        }

        if (timer >= timeStop + .5f)
        {
            Destroy(gameObject);
        }
    }
}

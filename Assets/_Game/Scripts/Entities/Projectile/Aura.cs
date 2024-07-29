using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour
{
    public ParticleSystem[] fxs;

    bool todestroy;
    private void Update()
    {
        if (transform.parent == null)
        {
            if (!todestroy)
            {
                foreach (var fx in fxs)
                {
                    ParticleSystem.EmissionModule emission = fx.emission;
                    emission.enabled = false;
                }
                todestroy = true;
                Invoke("SelfDestroy", .5f);
            }
        }
    }

    void SelfDestroy()
    {
        Destroy(gameObject);
    }
}

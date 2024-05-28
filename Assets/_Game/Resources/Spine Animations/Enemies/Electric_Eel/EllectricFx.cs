using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class EllectricFx : MonoBehaviour
{
    ParticleSystem ParticleSystem;
    public Transform target;
    public ParticleSystem lightning;
    private ParticleSystem.MainModule lightningMain;
    private ParticleSystem.Particle[] m_particles;
    private void Awake()
    {
        ParticleSystem = GetComponent<ParticleSystem>();
        m_particles = new ParticleSystem.Particle[10];
        lightningMain = lightning.main;
    }


    private void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.up = direction.normalized;
        transform.up = target.position - transform.position;

        int numParticlesAlive = lightning.GetParticles(m_particles);
        for (int i = 0; i < numParticlesAlive; i++)
        {
            m_particles[i].startSize3D = new Vector3(3, direction.magnitude, 1);
        }
        lightning.SetParticles(m_particles, numParticlesAlive);



    }
}

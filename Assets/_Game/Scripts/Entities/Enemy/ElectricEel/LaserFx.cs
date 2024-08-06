using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFx : MonoBehaviour
{
    public Transform laserGuide;
    public ParticleSystem laser;
    Dictionary<ParticleSystem, ParticleSystem.Particle[]> particlesDic;
    public ParticleSystem[] followPositionParticles;
    public ParticleSystem[] scaleSizeParticles;
    public bool isPlay;
    UnityEngine.Material material;
    public ParticleSystem[] alls;
    private void Awake()
    {
        particlesDic = new Dictionary<ParticleSystem, ParticleSystem.Particle[]>();
        foreach (ParticleSystem particleSystem in scaleSizeParticles)
        {
            particlesDic.Add(particleSystem, new ParticleSystem.Particle[particleSystem.main.maxParticles]);
        }
    }
    [ContextMenu("Play")]
    public void Play()
    {
        isPlay = true;
        foreach (ParticleSystem ps in alls)
        {
            ParticleSystem.EmissionModule emissionModule = ps.emission;
            emissionModule.enabled = true;
        }
        laser.Play();
    }
    [ContextMenu("Stop")]
    public void Stop()
    {
        foreach (ParticleSystem ps in alls)
        {
            ParticleSystem.EmissionModule emissionModule = ps.emission;
            emissionModule.enabled = false;
        }
    }

    public void LateUpdate()
    {
        if (isPlay)
        {
            foreach (ParticleSystem ps in scaleSizeParticles)
            {
                ScaleSize(ps);
            }

            foreach (ParticleSystem ps in followPositionParticles)
            {
                ps.transform.position = laserGuide.transform.position;
            }
        }
    }

    public float scale = 0.067f;
    void ScaleSize(ParticleSystem ps)
    {
        ParticleSystem.Particle[] particles = particlesDic[ps];

        Vector3 direction = laserGuide.position - transform.position;
        float distance = direction.magnitude;
        ps.transform.forward = -direction;
        int currentAliveParticles = ps.GetParticles(particles);
        for (int i = 0; i < currentAliveParticles; i++)
        {
            particles[i].startSize3D = new Vector3(.5f, distance * scale, 1);
        }
        ps.SetParticles(particles, currentAliveParticles);
    }
}

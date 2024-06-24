using _Game.Scripts.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricFx : MonoBehaviour
{
    public Transform startTransform;
    public Transform targetTransform;
    public ParticleSystem lighting;
    public bool isPlay;
    public ParticleSystem.Particle[] particles;
    public bool IsNotDestrouy;
    UnityEngine.Material material;
    private void Awake()
    {
        particles = new ParticleSystem.Particle[lighting.main.maxParticles];
        material = lighting.GetComponent<ParticleSystemRenderer>().material;
    }
    public void Play()
    {
        isPlay = true;
        lighting.Play();
    }

    public void Update()
    {
        if (isPlay)
        {
            transform.position = startTransform.position;
            Vector3 direction = targetTransform.position - startTransform.position;
            float distance = direction.magnitude;
            transform.up = direction;
            float tillingX = Mathf.Lerp(0, 1, distance / 14);
            float scaleX = Mathf.Lerp(2, 3, distance / 14);
            material.mainTextureScale = new Vector2(tillingX, 1);
            int currentAliveParticles = lighting.GetParticles(particles);
            for (int i = 0; i < currentAliveParticles; i++)
            {
                particles[i].startSize3D = new Vector3(scaleX, distance, 1);
            }
            lighting.SetParticles(particles, currentAliveParticles);
            if (!lighting.IsAlive(true) && !IsNotDestrouy)
            {
                Destroy(gameObject);
                isPlay = false;
            }
        }
    }
}

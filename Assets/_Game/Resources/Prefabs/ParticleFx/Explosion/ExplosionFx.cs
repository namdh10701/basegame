using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionFx : MonoBehaviour
{

    public ParticleSystem root;
    public ParticleSystem ring;
    ParticleSystem.MainModule ringMain;
    public float size;

    public float orgScale = 2.5f;
    private void Start()
    {
        ringMain = ring.main;
    }
    public void SetSize(float size)
    {
        root.transform.localScale = new Vector3(size / orgScale, size / orgScale, size / orgScale);
        ring.transform.localScale = new Vector3(1 / (size / orgScale), 1 / (size / orgScale), 1 / (size / orgScale));
        ringMain.startSize = size;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SetSize(size);
            root.Play();
        }
    }
}

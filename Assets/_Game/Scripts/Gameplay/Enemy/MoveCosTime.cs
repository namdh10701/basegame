using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCosTime : MonoBehaviour
{
    [SerializeField] Rigidbody2D body;
    [SerializeField] private Vector2 destination;
    [SerializeField] private float frequency;
    [SerializeField] private float amplitude;
    [SerializeField] private float modifier;
    [SerializeField] public Vector2 Direction;
    [SerializeField] bool isActive;
    public void Active()
    {
        isActive = true;
    }

    public void Deactive()
    {
        isActive = false;
    }
    private void Update()
    {
        if (isActive)
        {
            body.velocity = Mathf.Clamp((Mathf.Cos(Time.time * frequency) * amplitude + modifier), 0, 10000) * Direction;
        }
    }

}

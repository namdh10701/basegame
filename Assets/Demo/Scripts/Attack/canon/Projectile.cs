using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D body;
    public float Speed;
    private void Start()
    {
        body.velocity = transform.up * Speed;
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void OnHit()
    {
        Destroy(gameObject);
    }
}
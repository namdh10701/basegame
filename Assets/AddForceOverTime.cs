using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceOverTime : MonoBehaviour
{
    Rigidbody2D body;
    public float force;
    public float time;
    float elapsedTime;
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (elapsedTime < time)
        {
            elapsedTime += Time.fixedDeltaTime;
            body.AddForce(force * Vector2.down, ForceMode2D.Force);
        }
        else
        {
            body.velocity = Vector3.zero;
        }
    }
}

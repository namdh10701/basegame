using _Game.Scripts.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public CircleCollider2D circleCollider;
    public CircleCollider2D circleColliderB;
    public Area area;
    public Vector2 targetPoint;
    public Rigidbody2D body;

    public float force;
    public bool isMoving;
    public float speedClamp;
    Vector2 direction;
    private void FixedUpdate()
    {
        Move();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SelectTargetPoint();
            isMoving = true;
        }
    }

    void SelectTargetPoint()
    {
        circleCollider.transform.position = area.SampleCircle(circleCollider);
        targetPoint = circleCollider.SamplePoint(circleColliderB);
    }

    void Move()
    {
        if (isMoving)
        {
            direction = (targetPoint - (Vector2)circleColliderB.transform.position).normalized;
            if (Vector2.Distance(targetPoint, (Vector2)circleColliderB.transform.position) > 1f)
            {
                body.AddForce(direction * force);
            }
            else
            {
                isMoving = false;
            }
            ClampVel();
        }
    }


    void ClampVel()
    {
        if (body.velocity.magnitude > speedClamp)
        {
            body.velocity = speedClamp * direction;
        }
    }
}



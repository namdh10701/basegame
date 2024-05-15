using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierMovement : MonoBehaviour
{
    public Transform startPoint; // Start point of the curve
    public Transform controlPoint; // Control point of the curve
    public Transform endPoint; // End point of the curve
    public Rigidbody2D rb; // Rigidbody2D to move
    public float duration = 2f; // Duration of the movement

    private float t = 0f; // Parameter for interpolation

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartMovement();
        }

        if (t < 1f)
        {
            MoveAlongBezierCurve();
        }
    }

    private void StartMovement()
    {
        t = 0f; // Reset parameter
    }

    private void MoveAlongBezierCurve()
    {
        t += Time.deltaTime / duration; // Increment parameter based on time

        // Calculate position along the Bezier curve
        Vector3 position = CalculateBezierPoint(startPoint.position, controlPoint.position, endPoint.position, t);

        // Move the Rigidbody2D to the calculated position
        rb.MovePosition(position);
    }

    // Calculate a point along a Bezier curve given start, control, and end points
    private Vector3 CalculateBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;
    }
}
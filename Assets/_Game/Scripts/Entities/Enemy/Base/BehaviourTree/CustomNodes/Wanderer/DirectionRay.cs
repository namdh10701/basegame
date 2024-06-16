using _Game.Scripts.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DirectionRay : MonoBehaviour
{
    public Area area;

    public float maxDistance = 10f;
    public Vector2 rayDirection = Vector2.right;
    public bool IsDrawGizmos;
    public Color color;
    public IRayListener Listener;

    public DirectionRay Reverse;
    public UnityEvent onCollide;
    public UnityEvent onStopCollide;

    bool isCollide;
    private void Awake()
    {
        IsDrawGizmos = false;
    }
    void Update()
    {
        if (area == null)
        {
            return;
        }
        // The origin point of the raycast (can be the position of the GameObject)
        Vector2 origin = transform.position;

        // Ensure the origin is within the bounds
        if (area.bounds.Contains(origin) && maxDistance > 0)
        {
            // Calculate the end point of the ray
            Vector2 endPoint = origin + rayDirection.normalized * maxDistance;

            // Check if the line segment intersects with the bounds
            if (LineSegmentIntersectsBounds(origin, endPoint, area.bounds, out float distance))
            {
                if (!isCollide)
                {
                    isCollide = true;
                    onCollide?.Invoke();
                    // Log that the ray hit the bounds from inside
                    Listener?.OnIntersectBounds(area, this, distance);
                }
                // Optional: Draw a debug line in the scene view
                Debug.DrawLine(origin, endPoint, Color.red);
            }
            else
            {
                if (isCollide)
                {
                    isCollide = false;
                    Listener?.OnIntersectStop(area, this);
                    onStopCollide?.Invoke();
                }
                // Optional: Draw a debug line in the scene view to indicate no hit
                Debug.DrawLine(origin, endPoint, Color.green);
            }
            Listener.OnInsideBounds(area, this);
        }
        else
        {
            Listener?.OnOutsideBounds(area, this);
        }
    }

    // Check if a line segment intersects the bounds
    private bool LineSegmentIntersectsBounds(Vector2 p1, Vector2 p2, Bounds bounds, out float distance)
    {
        // Get the bounds corners
        Vector2 min = bounds.min;
        Vector2 max = bounds.max;


        // Check intersection with each side of the bounds
        if (LineIntersectsLine(p1, p2, new Vector2(min.x, min.y), new Vector2(max.x, min.y), out distance)) // Bottom
            return true;
        if (LineIntersectsLine(p1, p2, new Vector2(min.x, max.y), new Vector2(max.x, max.y), out distance)) // Top
            return true;
        if (LineIntersectsLine(p1, p2, new Vector2(min.x, min.y), new Vector2(min.x, max.y), out distance)) // Left
            return true;
        if (LineIntersectsLine(p1, p2, new Vector2(max.x, min.y), new Vector2(max.x, max.y), out distance)) // Right
            return true;
        return false;
    }

    // Check if two line segments intersect
    private bool LineIntersectsLine(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, out float distance)
    {
        distance = 0f;

        float denominator = (p4.y - p3.y) * (p2.x - p1.x) - (p4.x - p3.x) * (p2.y - p1.y);

        // Lines are parallel
        if (denominator == 0)
            return false;

        float ua = ((p4.x - p3.x) * (p1.y - p3.y) - (p4.y - p3.y) * (p1.x - p3.x)) / denominator;
        float ub = ((p2.x - p1.x) * (p1.y - p3.y) - (p2.y - p1.y) * (p1.x - p3.x)) / denominator;

        // Check if intersection point is within both line segments
        if (ua >= 0 && ua <= 1 && ub >= 0 && ub <= 1)
        {
            // Calculate the distance to the intersection point
            distance = Vector2.Distance(p1, new Vector2(p1.x + ua * (p2.x - p1.x), p1.y + ua * (p2.y - p1.y)));
            return true;
        }

        return false;
    }
    private void OnDrawGizmos()
    {
        if (IsDrawGizmos)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)rayDirection.normalized * maxDistance);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Base.Scripts.Utils
{
    public static class CircleCollider2DExtension
    {
        public static Vector2 SamplePoint(this CircleCollider2D circleCollider)
        {
            float radius = circleCollider.radius;
            Vector2 circleCenter = circleCollider.transform.position;

            float angle = Random.Range(0f, 2f * Mathf.PI);
            float distance = Mathf.Sqrt(Random.Range(0f, 1f)) * radius;

            float x = circleCenter.x + distance * Mathf.Cos(angle);
            float y = circleCenter.y + distance * Mathf.Sin(angle);

            return new Vector2(x, y);
        }

        public static Vector2 SamplePoint(this CircleCollider2D circleCollider, CircleCollider2D colliderB)
        {
            float radiusA = circleCollider.radius;
            float radiusB = colliderB.radius;
            // Ensure B can fit within A
            if (radiusA < radiusB)
            {
                throw new System.ArgumentException("Circle B cannot fit within Circle A");
            }
            // Calculate effective radius
            float effectiveRadius = radiusA - radiusB;

            // Generate a random angle between 0 and 2π
            float theta = Random.Range(0f, 2f * Mathf.PI);

            // Generate a random distance with uniform distribution within the effective radius
            float distance = Mathf.Sqrt(Random.Range(0f, 1f)) * effectiveRadius;

            // Convert polar coordinates to Cartesian coordinates
            float x = distance * Mathf.Cos(theta);
            float y = distance * Mathf.Sin(theta);

            // Adjust by the center position of A
            float B_x = circleCollider.transform.position.x + x;
            float B_y = circleCollider.transform.position.y + y;

            return new Vector2(B_x, B_y);
        }

        public static Vector2 SamplePoint(this CircleCollider2D circleCollider, BoxCollider2D boxCollider)
        {
            float radiusA = circleCollider.radius;
            float halfWidthB = boxCollider.size.x / 2f;
            float halfHeightB = boxCollider.size.y / 2f;

            // Calculate the effective radius
            float effectiveRadius = radiusA - Mathf.Sqrt(halfWidthB * halfWidthB + halfHeightB * halfHeightB);

            // Ensure B can fit within A
            if (effectiveRadius < 0)
            {
                throw new System.ArgumentException("BoxCollider2D cannot fit within CircleCollider2D");
            }

            // Generate a random angle between 0 and 2π
            float theta = Random.Range(0f, 2f * Mathf.PI);

            // Generate a random distance with uniform distribution within the effective radius
            float distance = Mathf.Sqrt(Random.Range(0f, 1f)) * effectiveRadius;

            // Convert polar coordinates to Cartesian coordinates
            float x = distance * Mathf.Cos(theta);
            float y = distance * Mathf.Sin(theta);

            // Adjust by the center position of A, considering the offset
            Vector2 circleCenter = (Vector2)circleCollider.transform.position + circleCollider.offset;
            float B_x = circleCenter.x + x;
            float B_y = circleCenter.y + y;

            return new Vector2(B_x, B_y);
        }

        public static Vector2 SamplePoint(this CircleCollider2D circleCollider, CapsuleCollider2D capsuleCollider)
        {
            float radiusA = circleCollider.radius;

            // Determine the maximum extent of the CapsuleCollider2D
            float halfWidthB = capsuleCollider.size.x / 2f;
            float halfHeightB = capsuleCollider.size.y / 2f;

            float effectiveRadius;

            if (capsuleCollider.direction == CapsuleDirection2D.Horizontal)
            {
                // Horizontal capsule
                effectiveRadius = radiusA - halfWidthB;
            }
            else
            {
                // Vertical capsule
                effectiveRadius = radiusA - halfHeightB;
            }

            // Ensure B can fit within A
            if (effectiveRadius < 0)
            {
                throw new System.ArgumentException("CapsuleCollider2D cannot fit within CircleCollider2D");
            }

            // Generate a random angle between 0 and 2π
            float theta = Random.Range(0f, 2f * Mathf.PI);

            // Generate a random distance with uniform distribution within the effective radius
            float distance = Mathf.Sqrt(Random.Range(0f, 1f)) * effectiveRadius;

            // Convert polar coordinates to Cartesian coordinates
            float x = distance * Mathf.Cos(theta);
            float y = distance * Mathf.Sin(theta);

            // Adjust by the center position of A, considering the offset
            Vector2 circleCenter = (Vector2)circleCollider.transform.position + circleCollider.offset;
            float B_x = circleCenter.x + x;
            float B_y = circleCenter.y + y;

            return new Vector2(B_x, B_y);
        }

        public static Vector2 SamplePoint(this CircleCollider2D circleCollider, PolygonCollider2D polygonCollider)
        {
            float radiusA = circleCollider.radius;

            // Calculate the bounding radius of the polygon collider
            float maxDistance = 0f;
            foreach (Vector2 point in polygonCollider.points)
            {
                float distance = point.magnitude;
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                }
            }

            // Ensure B can fit within A
            if (radiusA < maxDistance)
            {
                throw new System.ArgumentException("PolygonCollider2D cannot fit within CircleCollider2D");
            }

            // Calculate effective radius
            float effectiveRadius = radiusA - maxDistance;

            // Generate a random angle between 0 and 2π
            float theta = Random.Range(0f, 2f * Mathf.PI);

            // Generate a random distance with uniform distribution within the effective radius
            float distanceFromCenter = Mathf.Sqrt(Random.Range(0f, 1f)) * effectiveRadius;

            // Convert polar coordinates to Cartesian coordinates
            float x = distanceFromCenter * Mathf.Cos(theta);
            float y = distanceFromCenter * Mathf.Sin(theta);

            // Adjust by the center position of A, considering the offset
            Vector2 circleCenter = (Vector2)circleCollider.transform.position + circleCollider.offset;
            float B_x = circleCenter.x + x;
            float B_y = circleCenter.y + y;

            return new Vector2(B_x, B_y);
        }
    }
}
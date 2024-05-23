using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Battle
{
    public class Area : MonoBehaviour
    {
        [SerializeField] Bounds bounds;
        [SerializeField] Color color;
        [SerializeField] Vector2 offset;
        [SerializeField] bool isDrawGizmos;
        private void OnDrawGizmos()
        {
            if (!isDrawGizmos)
            {
                return;
            }
            Gizmos.color = color; // Set the color of the bounds
            bounds.center = transform.position;
            Gizmos.DrawWireCube(bounds.center + (Vector3)offset, bounds.size);
        }

        public Vector2 SampleCircle(CircleCollider2D circleCollider)
        {
            Vector2 sampledPoint = SamplePoint(circleCollider.radius, circleCollider.radius);
            return sampledPoint;
        }

        public Vector2 SamplePoint()
        {
            Vector2 point = bounds.center + new Vector3(Random.Range(-bounds.extents.x, bounds.extents.x), Random.Range(-bounds.extents.y, bounds.extents.y));
            point += offset;
            return point;
        }

        public Vector2 SamplePoint(float offsetX, float offsetY)
        {
            Vector2 point = bounds.center + new Vector3(Random.Range(-bounds.extents.x + offsetX, bounds.extents.x - offsetX), Random.Range(-bounds.extents.y + offsetY, bounds.extents.y - offsetY));
            point += offset;
            return point;
        }
    }
}
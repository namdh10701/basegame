using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnArea : MonoBehaviour
{
    [SerializeField] Bounds bounds;
    [SerializeField] Color color;
    [SerializeField] Vector2 offset;

    private void OnDrawGizmos()
    {
        Gizmos.color = color; // Set the color of the bounds
        bounds.center = transform.position;
        Gizmos.DrawWireCube(bounds.center + (Vector3)offset, bounds.size);
    }

    public Vector2 SamplePoint()
    {
        Vector2 point = bounds.center + new Vector3(Random.Range(-bounds.extents.x, bounds.extents.x), Random.Range(-bounds.extents.y, bounds.extents.y));
        point += offset;
        return point;
    }
}

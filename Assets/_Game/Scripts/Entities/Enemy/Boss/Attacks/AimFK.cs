using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class AimFK : MonoBehaviour
{
    public bool laserIsFiring;
    public Transform laserGuideTransform;
    public Transform root;
    public float boundXMin;
    public float boundXMax;
    public float Y;
    Vector2 aimDirection;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(root.transform.position, root.transform.position + (Vector3)aimDirection * 10);
    }
    void Update()
    {
        aimDirection = laserGuideTransform.position - root.position;
        if (laserIsFiring)
        {
            Vector3 targetPosition = root.position + (Vector3)aimDirection;
            //float d = (Y - root.position.y) / aimDirection.y;
            targetPosition = root.position + (Vector3)aimDirection.normalized * 3.7f;
            targetPosition.x = Mathf.Clamp(targetPosition.x, +boundXMin, +boundXMax);
            transform.position = targetPosition;
        }
    }
}

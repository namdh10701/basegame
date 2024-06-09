using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Collections;
using UnityEngine;

[ExecuteAlways]
public class CannonShadow : MonoBehaviour
{
    public Transform rotateTransform;
    public float minOffset;
    public float maxOffset;
    public float maxAngle;
    public float minAngle;

    public float currentAngle;
    public Vector3 currentRotation;
    public Quaternion currentQuater;

    private void Update()
    {
        if (rotateTransform == null)
            return;
        Vector3 forwardDirection = rotateTransform.up;
        float currentAngle = Vector3.SignedAngle(Vector2.up, forwardDirection, Vector3.forward);
        float angleRatio = Mathf.InverseLerp(minAngle, maxAngle, currentAngle);
        angleRatio = Mathf.Clamp01(angleRatio);
        float currentOffset = Mathf.Lerp(maxOffset, minOffset, angleRatio);
        transform.localPosition = new Vector3(currentOffset, 0, 0);
    }
}
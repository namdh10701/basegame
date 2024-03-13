using System;
using UnityEngine;

public class RotateBrain : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    Quaternion restRotation;

    [HideInInspector] public bool IsLookingAtTarget;

    private void Start()
    {
        restRotation = transform.rotation;
    }
    public void Rotate(Transform target)
    {
        Vector3 targetDirection = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        float angle = Quaternion.Angle(transform.rotation, targetRotation);
        IsLookingAtTarget = angle < 1f;
    }

    internal void ResetRotate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, restRotation, rotateSpeed * Time.deltaTime);
        IsLookingAtTarget = false;
    }
}
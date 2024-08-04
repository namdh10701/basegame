using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    private readonly float[] angles = { 0f, 90f, 180f, 270f };

    private void OnEnable()
    {
        int randomIndex = Random.Range(0, angles.Length);
        float selectedAngle = angles[randomIndex];
        transform.rotation = Quaternion.Euler(0, 0, selectedAngle);
    }
}

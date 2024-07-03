using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceCamera : MonoBehaviour
{
    public Canvas canvas;

    private void OnEnable()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
    }
}

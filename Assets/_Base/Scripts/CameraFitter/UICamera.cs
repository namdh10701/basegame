using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICamera : MonoBehaviour
{
    private void Start()
    {
        CameraFitter.Instance?.RegisterUICam(GetComponent<Camera>());
    }

    private void OnDestroy()
    {
        CameraFitter.Instance?.UnregisterUICam(GetComponent<Camera>());
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private void OnEnable()
    {
        CameraFitter.Instance.RegisterMainCam(GetComponent<Camera>());
    }
    private void OnDestroy()
    {
        CameraFitter.Instance?.UnregisterMainCam(GetComponent<Camera>());
    }
}

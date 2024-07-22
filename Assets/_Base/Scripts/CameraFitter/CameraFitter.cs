using System;
using UnityEngine;

public enum FitType
{
    FitX, FitY, Both
}
public class CameraFitter : MonoBehaviour
{
    private static CameraFitter instance;
    [SerializeField] private SpriteRenderer cameraRefRatio;
    [SerializeField] private FitType fitType;
    private Camera mainCam;
    private Camera uiCam;
    public Camera cam;

    private void Awake()
    {
        instance = this;
        //ResizeCamera(cam);
    }

    public static CameraFitter Instance => instance;

    public void RegisterMainCam(Camera camera)
    {
        mainCam = camera;
        //ResizeCamera(mainCam);
    }

    public void RegisterUICam(Camera camera)
    {
        uiCam = camera;
        //ResizeCamera(uiCam);
    }


    public float GetOrthograpicSize(Camera mainCam, SpriteRenderer ratioRef)
    {
        float bgWidth = ratioRef.bounds.size.x;
        float bgHeight = ratioRef.bounds.size.y;
        float targetOrthoSize = 5;

        switch (fitType)
        {
            case FitType.Both:
                targetOrthoSize = Mathf.Max(bgWidth * 0.5f / GetComponent<Camera>().aspect, bgHeight * 0.5f);
                break;
            case FitType.FitX:
                targetOrthoSize = bgWidth * 0.5f / GetComponent<Camera>().aspect;
                break;
            case FitType.FitY:
                targetOrthoSize = bgHeight * .5f;
                break;
        }

        return targetOrthoSize;
    }
}
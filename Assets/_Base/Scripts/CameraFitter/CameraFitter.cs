using UnityEngine;
using static _Base.Scripts.UI.FlexibleGridLayout;

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

    private void Awake()
    {
        instance = this;
    }

    public static CameraFitter Instance => instance;

    public void RegisterMainCam(Camera camera)
    {
        mainCam = camera;
        ResizeCamera(mainCam);
    }

    public void RegisterUICam(Camera camera)
    {
        uiCam = camera;
        ResizeCamera(uiCam);
    }

    private void ResizeCamera(Camera camera)
    {
        if (cameraRefRatio == null)
            return;

        float bgWidth = cameraRefRatio.bounds.size.x;
        float bgHeight = cameraRefRatio.bounds.size.y;
        float targetOrthoSize = 5;

        switch (fitType)
        {
            case FitType.Both:
                targetOrthoSize = Mathf.Max(bgWidth * 0.5f / camera.aspect, bgHeight * 0.5f);
                break;
            case FitType.FitX:
                targetOrthoSize = bgWidth * 0.5f / camera.aspect;
                break;
            case FitType.FitY:
                targetOrthoSize = bgHeight * .5f;
                break;
        }

        camera.orthographicSize = targetOrthoSize;
    }

    public void UnregisterMainCam(Camera camera)
    {
        if (mainCam == camera)
            mainCam = null;
    }

    public void UnregisterUICam(Camera camera)
    {
        if (uiCam == camera)
            uiCam = null;
    }
}
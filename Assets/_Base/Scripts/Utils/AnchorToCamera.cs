using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorToCamera : MonoBehaviour
{
    public enum Side
    {
        BottomLeft,
        Left,
        TopLeft,
        Top,
        TopRight,
        Right,
        BottomRight,
        Bottom,
        Center,
    }

    public Camera mainCamera = null;
    public Side side = Side.Center;
    public Vector3 relativeOffset = Vector3.zero;

    private void Start()
    {
        mainCamera = Camera.main;
        switch (side)
        {
            case Side.Top:
                transform.position = new Vector3(transform.position.x, mainCamera.orthographicSize + relativeOffset.y, transform.position.z);
                break;
            case Side.Bottom:
                transform.position = new Vector3(transform.position.x, -mainCamera.orthographicSize + relativeOffset.y, transform.position.z);
                break;
            case Side.Left:
                transform.position = new Vector3(-mainCamera.orthographicSize * mainCamera.aspect + relativeOffset.x, transform.position.y, transform.position.z);
                break;
            case Side.Right:
                transform.position = new Vector3(mainCamera.orthographicSize * mainCamera.aspect + relativeOffset.x, transform.position.y, transform.position.z);
                break;
            case Side.BottomLeft:
                transform.position = new Vector3(-mainCamera.orthographicSize * mainCamera.aspect + relativeOffset.x, -mainCamera.orthographicSize + relativeOffset.y, transform.position.z);
                break;
            case Side.BottomRight:
                transform.position = new Vector3(mainCamera.orthographicSize * mainCamera.aspect + relativeOffset.x, -mainCamera.orthographicSize + relativeOffset.y, transform.position.z);
                break;
            case Side.TopLeft:
                transform.position = new Vector3(-mainCamera.orthographicSize * mainCamera.aspect + relativeOffset.x, mainCamera.orthographicSize + relativeOffset.y, transform.position.z);
                break;
            case Side.TopRight:
                transform.position = new Vector3(mainCamera.orthographicSize * mainCamera.aspect + relativeOffset.x, mainCamera.orthographicSize + relativeOffset.y, transform.position.z);
                break;
        }
    }

}

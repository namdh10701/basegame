using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static _Base.Scripts.UI.FlexibleGridLayout;

namespace _Game.Features.Gameplay
{
    public enum CameraSize
    {
        Normal, GiantOctopusBoss
    }
    public class CameraController : MonoBehaviour
    {
        private static CameraController instance;
        public static CameraController Instance => instance;

        public SpriteRenderer NormalCameraRef;
        public SpriteRenderer GiantOctopusCameraRef;
        public Camera mainCam;
        public Dictionary<CameraSize, SpriteRenderer> refDic;
        private void Awake()
        {
            instance = this;

            refDic = new Dictionary<CameraSize, SpriteRenderer>() { { CameraSize.Normal, NormalCameraRef }, { CameraSize.GiantOctopusBoss, GiantOctopusCameraRef } };
            FitCameraToRatio(NormalCameraRef);
        }

        public void FitCameraToRatio(SpriteRenderer ratioRef)
        {
            float size = GetOrthograpicSize(mainCam, ratioRef);
            mainCam.orthographicSize = size;
        }

        public void LerpSize(CameraSize cameraSize)
        {
            float size = GetOrthograpicSize(mainCam, refDic[cameraSize]);
            StartCoroutine(LerpSizeCoroutine(size));
        }
        public float GetOrthograpicSize(Camera mainCam, SpriteRenderer ratioRef)
        {
            float bgWidth = ratioRef.bounds.size.x;
            float bgHeight = ratioRef.bounds.size.y;
            float targetOrthoSize = 5;
            targetOrthoSize = Mathf.Max(bgWidth * 0.5f / GetComponent<Camera>().aspect, bgHeight * 0.5f);
            return targetOrthoSize;
        }
        IEnumerator LerpSizeCoroutine(float targetSize)
        {
            float startSize = mainCam.orthographicSize;
            float elapsedTime = 0;
            float progress = 0;
            float duration = 1;

            while (progress < 1)
            {
                elapsedTime += Time.deltaTime;
                progress = elapsedTime / duration;
                mainCam.orthographicSize = Mathf.Lerp(startSize, targetSize, progress);
                yield return null;
            }
        }
    }
}
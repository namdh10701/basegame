using System;
using UnityEngine;

namespace _Base.Scripts.Utils
{
    /// <summary>
    /// Contains UI helper methods
    /// </summary>
    public static class UIUtils
    {
        /// <summary>
        /// Resizes a RectTransform to match the screen safe area.
        /// The resize algorithm only works for full screen canvas/parents.
        /// </summary>
        public static void ResizeToSafeArea(this RectTransform rectTransform, Canvas canvas)
        {
            if (rectTransform == null)
                throw new ArgumentNullException(nameof(rectTransform));

            Rect safeArea = Screen.safeArea;
            Rect canvasRect = canvas.pixelRect;

            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;
            anchorMin.x /= canvasRect.width;
            anchorMin.y /= canvasRect.height;
            anchorMax.x /= canvasRect.width;
            anchorMax.y /= canvasRect.height;

#if UNITY_EDITOR
            rectTransform.anchorMin = new Vector2(0f, 0f);
            rectTransform.anchorMax = new Vector2(1f, 1f);
#else
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
#endif
        }
    }
}

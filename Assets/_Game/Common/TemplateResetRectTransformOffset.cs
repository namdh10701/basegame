using UnityEngine;

namespace _Game.Common
{
    public class TemplateResetRectTransformOffset: MonoBehaviour
    {
        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                var rect = child.GetComponent<RectTransform>();
                if (!rect) continue;
                
                rect.offsetMax = Vector2.zero;
                rect.offsetMin = Vector2.zero;
            }
        }
    }
}

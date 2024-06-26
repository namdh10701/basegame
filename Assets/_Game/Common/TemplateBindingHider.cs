using UnityEngine;

namespace _Game.Common
{
    [DefaultExecutionOrder(-500)]
    public class TemplateBindingHider: MonoBehaviour
    {
        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                child.gameObject.SetActive(false);
            }
        }
    }
}

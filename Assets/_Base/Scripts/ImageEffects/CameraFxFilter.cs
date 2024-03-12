using UnityEngine;

namespace _Base.Scripts.ImageEffects
{
    public class CameraFxFilter : MonoBehaviour
    {
        public Material material;
        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (material == null)
            {
                Graphics.Blit(source, destination);
                return;
            }
            Graphics.Blit(source, destination, material);
        }

    }
}

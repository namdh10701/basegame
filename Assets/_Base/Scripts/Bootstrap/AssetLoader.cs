using UnityEngine;

namespace _Base.Scripts.Bootstrap
{
    public class AssetLoader: MonoBehaviour
    {
        [SerializeField] GameObject[] assets;
        [SerializeField] Transform persistentRoot;

        public void Load()
        {
            foreach (GameObject preloadedAsset in assets)
            {
                Instantiate(preloadedAsset, persistentRoot);
            }
        }
    }
}
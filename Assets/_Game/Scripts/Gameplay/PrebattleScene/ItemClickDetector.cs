using UnityEngine;

namespace _Game.Scripts
{
    public class ItemClickDetector : MonoBehaviour
    {
        [SerializeField] Collider2D collider;
        public GameObject Item;
        public void Toggle(bool isOn)
        {
            collider.enabled = isOn;
        }
    }
}

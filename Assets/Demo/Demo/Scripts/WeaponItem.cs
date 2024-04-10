using Demo.Scripts.Data;
using UnityEngine;

namespace Demo.Scripts
{
    public class WeaponItem : MonoBehaviour
    {
        private Color _color;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        public WeaponData WeaponData;
        public GameObject Parent;

        public void Setup(WeaponData data, GameObject parent)
        {
            WeaponData = data;
            Parent = parent;
            _spriteRenderer.sprite = data.Sprite;
            _spriteRenderer.color = data.Color;
            var color = _spriteRenderer.color;
            color.a = 1;
            _spriteRenderer.color = color;
        }

        public WeaponItem GetDataWeaponItem()
        {
            return this;
        }
    }
}
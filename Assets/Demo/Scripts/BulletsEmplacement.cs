using Demo.Scripts.Data;
using UnityEngine;

namespace Demo.Scripts
{
    public class BulletsEmplacement : MonoBehaviour
    {
        [SerializeField] SpriteRenderer _bullet;
        public BulletData BulletData;
        public void Setup(BulletData bulletData)
        {
            BulletData = bulletData;
            _bullet.sprite = bulletData.Sprite;
        }

        public void EnableItem(bool enable)
        {
            _bullet.gameObject.SetActive(enable);
        }
    }
}

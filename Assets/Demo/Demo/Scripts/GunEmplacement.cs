using Demo.Scripts.Data;
using UnityEngine;

namespace Demo.Scripts
{
    public class GunEmplacement : MonoBehaviour
    {
        [SerializeField] SpriteRenderer _spriteRenderer;
        public int Id;
        public WeaponData _weaponData;
        public Canon.Canon Canon;

        public void Setup(int id)
        {
            Id = id;
        }

        public void SetWeaponData(WeaponData weaponData)
        {
            _weaponData = weaponData;
        }

        public void AddCanon(Canon.Canon canon)
        {
            Canon = canon;
        }

        public void RemoveCanon()
        {
            if (Canon != null)
                Destroy(Canon.gameObject);
            Canon = null;
        }



    }
}

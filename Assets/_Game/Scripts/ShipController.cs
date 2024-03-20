using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] List<Transform> _posGunEmplacements;
    [SerializeField] List<Transform> _posBulletsEmplacements;
    [SerializeField] GunEmplacement _prefabGunEmplacement;
    [SerializeField] BulletsEmplacement _prefabBulletsEmplacement;
    [SerializeField] BulletItem _prefabBulletItem;


    List<GunEmplacement> _gunEmplacements = new List<GunEmplacement>();
    List<BulletsEmplacement> _bulletEmplacements = new List<BulletsEmplacement>();
    BulletItem _bulletItem;

    public void Setup(BulletsConfig bulletsConfig)
    {
        SpawnGunEmplacement();
        SpawnBulletsEmplacement(bulletsConfig);
    }

    private void SpawnGunEmplacement()
    {
        for (int i = 0; i < _posGunEmplacements.Count; i++)
        {
            var temp = Instantiate(_prefabGunEmplacement, _posGunEmplacements[i]);
            temp.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            temp.gameObject.transform.localScale = new Vector3(0.15f, 0.15f, 0);
            temp.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            temp.Setup(i);
            _gunEmplacements.Add(temp);
        }
    }

    private void SpawnBulletsEmplacement(BulletsConfig bulletsConfig)
    {
        for (int i = 0; i < _posBulletsEmplacements.Count; i++)
        {
            var temp = Instantiate(_prefabBulletsEmplacement, _posBulletsEmplacements[i]);
            temp.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            temp.gameObject.transform.localScale = new Vector3(0.15f, 0.15f, 0);
            temp.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            temp.Setup(bulletsConfig.BulletsData[i]);
            _bulletEmplacements.Add(temp);
        }
    }

    public void SetWeaponDataToEmplacement(int gunEmplacementId, WeaponData weaponData)
    {
        foreach (var gunEmplacement in _gunEmplacements)
        {
            if (gunEmplacement.Id == gunEmplacementId)
            {
                gunEmplacement.SetWeaponData(weaponData);
            }
        }
    }

    public void RemoveCanon(int gunEmplacementId)
    {
        foreach (var gunEmplacement in _gunEmplacements)
        {
            if (gunEmplacement.Id == gunEmplacementId)
            {
                gunEmplacement.RemoveCanon();
            }
        }
    }

    public GameObject SpawnBulletItem(GameObject parent)
    {
        var bulletsEmplacement = parent.GetComponent<BulletsEmplacement>();
        if (_bulletItem != null)
        {
            Destroy(_bulletItem.gameObject);
        }
        _bulletItem = Instantiate(_prefabBulletItem, parent.transform);
        _bulletItem.Setup(bulletsEmplacement.BulletData);
        _bulletItem.transform.localScale = new Vector3(0.1f, 0.1f, 0);
        return _bulletItem.gameObject;
    }

    public void EnableBulletItem(int id, bool enable)
    {
        foreach (var item in _bulletEmplacements)
        {
            if (item.BulletData.Id == id)
            {
                item.EnableItem(enable);
            }
        }
    }

    public void ReloadBullet(Canon canon)
    {
        foreach (var item in _gunEmplacements)
        {
            if (item.Canon != null)
            {
                if (item.Canon == canon)
                {
                    item.Canon.Reload();
                }
            }
        }
    }
}

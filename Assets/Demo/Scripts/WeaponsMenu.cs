using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsMenu : MonoBehaviour
{
    [SerializeField] List<Transform> _posWeapons;
    [SerializeField] WeaponItem _prefabWeaponItem;

    List<WeaponItem> weaponItems = new List<WeaponItem>();

    public void SetUp()
    {
        foreach (var pos in _posWeapons)
        {
            var weaponItem = Instantiate(_prefabWeaponItem, pos);
            weaponItem.transform.localPosition = new Vector3(0, 0, 0);
            weaponItems.Add(weaponItem);
        }
    }
}

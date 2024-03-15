using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsMenu : MonoBehaviour
{
    [SerializeField] List<Transform> _posWeapons;
    [SerializeField] WeaponItem _prefabWeaponItem;

    List<WeaponItem> weaponItems = new List<WeaponItem>();

    public void SetUp(WeaponsConfig weaponsConfig)
    {
        for (int i = 0; i < _posWeapons.Count; i++)
        {
            var weaponItem = Instantiate(_prefabWeaponItem, _posWeapons[i]);
            weaponItem.transform.localPosition = new Vector3(0, 0, 0);
            weaponItem.Setup(weaponsConfig.WeaponsData[i].Color);
            weaponItems.Add(weaponItem);
        }
    }
}
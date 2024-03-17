using System;
using System.Collections;
using System.Collections.Generic;
using _Base.Scripts.Utils;
using DG.Tweening;
using UnityEngine;

public class GameController : SingletonMonoBehaviour<GameController>
{
    [SerializeField] ShipController _ship;
    [SerializeField] WeaponsMenu _prefabWeaponsMenu;
    [SerializeField] WeaponsMennuConfig _config;
    [SerializeField] List<Canon> canons;
    [SerializeField] EnemyController _enemyController;
    

    WeaponsMenu _weaponsMenu;

    void Awake()
    {
        Initializa();
    }

    private void Initializa()
    {
        _ship.Setup();
    }

    public void ShowWeaponsMenu(GameObject go)
    {
        _ship.RemoveCanon(go.GetComponent<GunEmplacement>().Id);
        _weaponsMenu = Instantiate(_prefabWeaponsMenu, go.transform);
        _weaponsMenu.transform.localPosition = new Vector3(0, 0, 0);
        _weaponsMenu.transform.DOScale(new Vector3(2.0f, 2.0f, 0f), 0.2f);
        _weaponsMenu.SetUp(_config, go);
    }

    public void OnSelectedWeaponItem(GameObject go)
    {
        var weaponItem = go.gameObject.GetComponent<WeaponItem>();
        SpawnCanon(weaponItem.GetDataWeaponItem());


        _weaponsMenu.transform.DOScale(new Vector3(1.0f, 1.0f, 0f), 0.2f).OnComplete(() =>
        {
            Destroy(_weaponsMenu.gameObject);
        });
    }

    public void SpawnCanon(WeaponItem item)
    {
        foreach (var canon in canons)
        {
            var gunEmplacement = item.Parent.gameObject.GetComponent<GunEmplacement>();

            if (canon.CanonData.Id == item.WeaponData.Id)
            {

                var temp = Instantiate(canon, item.Parent.transform);
                if (gunEmplacement.Id > 2)
                {
                    temp.transform.localEulerAngles = new Vector3(180, 0, 0);
                }
                gunEmplacement.AddCanon(temp);
                gunEmplacement.SetWeaponData(item.WeaponData);

            }
        }
    }
}
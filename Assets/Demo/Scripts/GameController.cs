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
    [SerializeField] private WeaponsConfig _config;

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
        _weaponsMenu = Instantiate(_prefabWeaponsMenu, go.transform);
        _weaponsMenu.transform.localPosition = new Vector3(0, 0, 0);
        _weaponsMenu.transform.DOScale(new Vector3(2.0f, 2.0f, 0f), 0.2f);
        _weaponsMenu.SetUp(_config);
    }

    public void OnSelectedWeaponItem(GameObject go)
    {
        _weaponsMenu.transform.DOScale(new Vector3(1.0f, 1.0f, 0f), 0.2f).OnComplete(() =>
        {
            Destroy(_weaponsMenu.gameObject);
        });
        // var weaponItem = go.GetComponent<WeaponItem>();
    }
}
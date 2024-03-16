using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunEmplacement : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    public int Id;
    public WeaponData _weaponData;
    public Canon Canon;

    public void Setup(int id)
    {
        Id = id;
    }

    public void SetWeaponData(WeaponData weaponData)
    {
        _weaponData = weaponData;
    }

    public void AddCanon(Canon canon)
    {
        Canon = canon;
    }

    public void RemoveCanon()
    {
        if (Canon != null)
            Destroy(Canon.gameObject);
    }



}

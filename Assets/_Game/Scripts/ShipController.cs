using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] List<Transform> _posGunEmplacements;
    [SerializeField] GunEmplacement _prefabGunEmplacement;

    public List<GunEmplacement> GunEmplacements = new List<GunEmplacement>();

    public void Setup()
    {
        for (int i = 0; i < _posGunEmplacements.Count; i++)
        {
            var temp = Instantiate(_prefabGunEmplacement, _posGunEmplacements[i]);
            temp.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            temp.gameObject.transform.localScale = new Vector3(0.15f, 0.15f, 0);
            temp.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            temp.Setup(i);
            GunEmplacements.Add(temp);
        }
    }

    public void SetWeaponDataToEmplacement(int gunEmplacementId, WeaponData weaponData)
    {
        foreach (var gunEmplacement in GunEmplacements)
        {
            if (gunEmplacement.Id == gunEmplacementId)
            {
                gunEmplacement.SetWeaponData(weaponData);
            }
        }
    }

    public void RemoveCanon(int gunEmplacementId)
    {
        foreach (var gunEmplacement in GunEmplacements)
        {
            if (gunEmplacement.Id == gunEmplacementId)
            {
                gunEmplacement.RemoveCanon();
            }
        }
    }
}

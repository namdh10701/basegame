using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] List<Transform> _posGunEmplacements;
    [SerializeField] GunEmplacement _prefabGunEmplacement;

    List<GunEmplacement> _gunEmplacement = new List<GunEmplacement>();

    public void Setup()
    {
        foreach (var pos in _posGunEmplacements)
        {
            var temp = Instantiate(_prefabGunEmplacement, pos);
            temp.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            temp.gameObject.transform.localScale = new Vector3(0.15f, 0.15f, 0);
            temp.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            _gunEmplacement.Add(temp);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointClickDetector : MonoBehaviour, IPointClickDetector
{
    [SerializeField] Camera mainCamera;
    public System.Action<GameObject> OnClickCallback;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                // if (hitObject.tag == Helper.TAG_GUN_EMPLACEMENT)
                // {
                //     GameController.Instance.ShowWeaponsMenu(hitObject);
                // }
                // else if (hitObject.tag == Helper.TAG_WEAPON_ITEM)
                // {
                //     GameController.Instance.OnSelectedWeaponItem(hitObject);
                // }
                switch (hitObject.tag)
                {
                    case Helper.TAG_GUN_EMPLACEMENT:
                        GameController.Instance.ShowWeaponsMenu(hitObject);
                        break;
                    case Helper.TAG_WEAPON_ITEM:
                        GameController.Instance.OnSelectedWeaponItem(hitObject);
                        break;
                }
            }
        }
    }
}

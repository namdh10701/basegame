using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointClickDetector : MonoBehaviour, IPointClickDetector
{
    [SerializeField] Camera mainCamera;
    public System.Action<GameObject> OnClickCallback;
    private GameObject selectedObject;
    GameObject hitObject;
    private bool isDragging = false;

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
                hitObject = hit.collider.gameObject;
                switch (hitObject.tag)
                {
                    case Helper.TAG_GUN_EMPLACEMENT:
                        GameController.Instance.ShowWeaponsMenu(hitObject);
                        break;
                    case Helper.TAG_WEAPON_ITEM:
                        GameController.Instance.OnSelectedWeaponItem(hitObject);
                        break;
                    case Helper.TAG_BULLET:
                        GameController.Instance.EnableBulletItem(hitObject, false);
                        selectedObject = GameController.Instance.SpawnBulletItem(hitObject);
                        isDragging = (selectedObject != null) ? true : false;
                        break;
                }
            }
        }
        if (isDragging)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            selectedObject.transform.position = pos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            GameController.Instance.EnableBulletItem(hitObject, true);
            Destroy(selectedObject.gameObject);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("GetMouseButtonUp" + hit.collider.gameObject.tag);
            }
        }

    }
}

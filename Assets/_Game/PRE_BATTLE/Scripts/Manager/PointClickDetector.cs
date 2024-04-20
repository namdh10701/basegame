using System;
using System.Collections.Generic;
using UnityEngine;

public class PointClickDetector : MonoBehaviour
{
    [SerializeField] Camera _camera;
    private bool _isDragActive = false;
    private Vector2 _screenPosition;
    private Vector3 _worldPosition;
    private GameObject _gameObjectSlected;


    public void Update()
    {
        if (_isDragActive)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Drop();
                return;

            }
        }
        if (Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;
            _screenPosition = new Vector2(mousePosition.x, mousePosition.y);
        }
        else if (Input.touchCount > 0)
        {
            _screenPosition = Input.GetTouch(0).position;
        }
        else
            return;

        _worldPosition = _camera.ScreenToWorldPoint(_screenPosition);
        if (_isDragActive)
        {
            Drag();
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(_worldPosition, Vector2.zero);
            HashSet<string> tagsToCheck = new HashSet<string> { "DragObject", "WeaponItem", "Gun" };

            // Check if the hit.collider is not null and its tag is in the HashSet
            if (hit.collider != null && tagsToCheck.Contains(hit.collider.gameObject.tag))
            {
                _gameObjectSlected = hit.collider.gameObject;
                InitDrag();
            }
        }

    }
    private void InitDrag()
    {
        _isDragActive = true;
    }

    private void Drag()
    {
        if (_gameObjectSlected.tag == "Gun")
        {
            return;
        }
        _gameObjectSlected.transform.position = new Vector2(_worldPosition.x, _worldPosition.y);
    }

    private void Drop()
    {
        if (_gameObjectSlected == null)
            return;

        // Define a dictionary to map tags to actions
        Dictionary<string, Action> tagActions = new Dictionary<string, Action>
        {
            { "DragObject", HandleDragObject },
            { "WeaponItem", HandleWeaponItem },
            { "Gun", HandleGun }
        };

        // Check if the tag exists in the dictionary and invoke the corresponding action
        if (tagActions.TryGetValue(_gameObjectSlected.gameObject.tag, out var action))
        {
            action.Invoke();
        }

        _isDragActive = false;
    }

    // Define methods to handle each tag
    void HandleDragObject()
    {
        var dragItem = _gameObjectSlected.GetComponent<DragItem>();
        dragItem.GetCellSelectFromDragItem(dragItem.GetItemMenuData());
        Destroy(_gameObjectSlected);
    }

    void HandleWeaponItem()
    {
        var weaponItem = _gameObjectSlected.GetComponent<WeaponItem>();
        weaponItem.GetCellSelectFromWeaponItem(weaponItem.GetItemMenuData());
    }

    void HandleGun()
    {
        Debug.Log("Tap Gun item");
    }


}


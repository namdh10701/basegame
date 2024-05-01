using System;
using System.Collections.Generic;
using _Game.Scripts;
using _Game.Scripts.Entities;
using _Game.Scripts.GameContext;
using _Game.Scripts.Input;
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

        _worldPosition = new Vector2(_camera.ScreenToWorldPoint(_screenPosition).x, _camera.ScreenToWorldPoint(_screenPosition).y);
        if (_isDragActive)
        {
            Drag();
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(_worldPosition, Vector2.zero);
            HashSet<string> tagsToCheck = new HashSet<string> { "DragObject", "WeaponItem", "Gun", "BulletsMenu" };

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
        HashSet<string> tagsToCheck = new HashSet<string> { "Gun", "BulletsMenu" };
        if (tagsToCheck.Contains(_gameObjectSlected.tag))
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
            { "Gun", HandleGun },
            { "BulletsMenu", HandleBulletsMenu },
        };

        // Check if the tag exists in the dictionary and invoke the corresponding action
        if (tagActions.TryGetValue(_gameObjectSlected.gameObject.tag, out var action))
        {
            action.Invoke();
        }

        _isDragActive = false;
    }

    public readonly static Dictionary<int, string> bulletDic = new Dictionary<int, string>()
    {
        {1,"CannonProjectile"},
        {2,"CannonProjectile1"}
    };

    private void HandleBulletsMenu()
    {
        if (GlobalContext.PlayerContext.ManaPoint.Value > 30)
        {
            GlobalContext.PlayerContext.ManaPoint.Value -= 30;
            var bulletItem = _gameObjectSlected.GetComponent<BulletItem>();
            var bulletPrefab = Resources.Load<_Base.Scripts.RPGCommon.Entities.Projectile>("Prefabs/Projectiles/" + bulletDic[bulletItem._id]);
            selectedCannon.Reloader.Reload(bulletPrefab);
        }
        if (_bulletsMenu != null)
        {
            Destroy(_bulletsMenu.gameObject);
        }
    }

    void HandleDragObject()
    {
        //var dragItem = _gameObjectSlected.GetComponent<DragItem>();
        //dragItem.GetCellSelectFromDragItem(dragItem.GetItemMenuData());
        Destroy(_gameObjectSlected);
    }

    void HandleWeaponItem()
    {
    }

    void HandleGun()
    {
        selectedCannon = _gameObjectSlected.GetComponent<ItemClickDetector>().Item.GetComponent<Cannon>();
        CreateBulletsMenu();
    }

    BulletsMenu _bulletsMenu;
    [SerializeField] BulletsMenu _prefabBulletsMenu;
    public ShipSetup ShipSetup;
    Cannon selectedCannon;
    public void CreateBulletsMenu()
    {
        if (_bulletsMenu == null)
        {
            _bulletsMenu = Instantiate(_prefabBulletsMenu, this.transform);
            //_bulletsMenu.Setup(ShipSetup._bulletItemData);
        }
    }

}


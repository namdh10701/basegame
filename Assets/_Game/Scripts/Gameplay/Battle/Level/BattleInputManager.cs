
using _Game.Scripts.Entities;
using _Game.Scripts.GameContext;
using _Game.Scripts.Gameplay.Ship;
using _Game.Scripts.Input;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class BattleInputManager : MonoBehaviour
    {
        [SerializeField] Camera _camera;
        [SerializeField] LayerMask layerMask;
        [SerializeField] ShipSetup shipSetup;
        [SerializeField] Ship Ship;
        public BulletsMenu bulletMenuPrefab;

        Cannon selectingCannon;
        BulletsMenu bulletMenu;
        private void Update()
        {
#if UNITY_EDITOR
            HandleMouse();
#else
            HandleTouch();
#endif
        }

        void HandleMouse()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, layerMask);
                if (hit.collider != null)
                {
                    if (hit.collider.TryGetComponent(out ItemClickDetector icd))
                    {
                        IGridItem gridItem = icd.Item.gameObject.GetComponent<IGridItem>();
                        if (gridItem is Cannon)
                        {
                            selectingCannon = gridItem as Cannon;
                            CreateBulletsMenu();
                        }
                        else if (gridItem is Bullet bullet)
                        {
                            OnSelectBullet(bullet);
                        }
                    }
                }
            }
        }

        bool isClick = false;
        void HandleTouch()
        {
            if (UnityEngine.Input.touchCount > 0)
            {
                Touch touch = UnityEngine.Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        isClick = true;
                        break;
                    case TouchPhase.Ended:
                        if (isClick)
                        {
                            Vector3 mousePosition = _camera.ScreenToWorldPoint(touch.position);
                            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, layerMask);
                            if (hit.collider != null)
                            {
                                if (hit.collider.TryGetComponent(out ItemClickDetector icd))
                                {
                                    IGridItem gridItem = icd.Item.gameObject.GetComponent<IGridItem>();
                                    if (gridItem is Cannon)
                                    {
                                        selectingCannon = gridItem as Cannon;
                                        CreateBulletsMenu();
                                    }
                                    else if (gridItem is Bullet bullet)
                                    {
                                        OnSelectBullet(bullet);
                                    }
                                }
                            }
                            isClick = false;
                        }
                        break;
                }
            }
        }

        void CreateBulletsMenu()
        {
            if (bulletMenu != null)
                return;
            bulletMenu = Instantiate(bulletMenuPrefab, this.transform);
            bulletMenu.Setup(ShipSetup.GridItemDatas);
        }

        public void OnSelectBullet(Bullet bullet)
        {
            if (selectingCannon == null)
                return;
            /*if (((ShipStats)Ship.Instance.Stats).ManaPoint.StatValue.BaseValue > 30)
            {
                ((ShipStats)Ship.Instance.Stats).ManaPoint.StatValue.BaseValue -= 30;
                var bulletPrefab = bullet.Projectile;
                selectingCannon.Reloader.Reload(bulletPrefab);
            }*/
            if (bulletMenu != null)
            {
                Destroy(bulletMenu.gameObject);
            }
        }
    }
}
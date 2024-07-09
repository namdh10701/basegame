
using _Game.Scripts.Entities;
using UnityEngine;
using UnityEngine.UI;
using _Game.Features.Inventory;
using _Game.Scripts.DB;
using _Game.Scripts;
using Unity.VisualScripting;

namespace _Game.Features.Gameplay
{
    public class BattleInputManager : MonoBehaviour
    {
        [SerializeField] Camera _camera;
        [SerializeField] LayerMask layerMask;
        public ShipHUD bulletMenu;

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
                RaycastHit2D[] hits = Physics2D.CircleCastAll(mousePosition, 1, Vector2.zero, Mathf.Infinity, layerMask);

                float closestDistance = Mathf.Infinity;
                RaycastHit2D closestHit = new RaycastHit2D();

                foreach (RaycastHit2D hit in hits)
                {
                    float distance = Vector2.Distance(hit.point, mousePosition);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestHit = hit;
                    }
                }

                if (closestHit.collider != null)
                {
                    OnClickDown(closestHit.collider);
                }

            }
        }
        void OnClickDown(Collider2D collider)
        {
            if (collider.TryGetComponent(out ItemClickDetector icd))
            {
                IWorkLocation workLocation = icd.Item.GetComponent<IWorkLocation>();
                workLocation.OnClick();
                IGridItem gridItem = icd.Item.GetComponent<IGridItem>();
                if (gridItem != null)
                {
                    if (gridItem.Def.Type == ItemType.CANNON)
                    {
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
                            RaycastHit2D[] hits = Physics2D.CircleCastAll(mousePosition, 1, Vector2.zero, Mathf.Infinity, layerMask);

                            float closestDistance = Mathf.Infinity;
                            RaycastHit2D closestHit = new RaycastHit2D();

                            foreach (RaycastHit2D hit in hits)
                            {
                                float distance = Vector2.Distance(hit.point, mousePosition);
                                if (distance < closestDistance)
                                {
                                    closestDistance = distance;
                                    closestHit = hit;
                                }
                            }

                            if (closestHit.collider != null)
                            {
                                OnClickDown(closestHit.collider);
                            }
                            isClick = false;
                        }
                        break;
                }
            }
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts
{
    public class ItemMenu : MonoBehaviour
    {
        [SerializeField] Image _backGroup;
        [SerializeField] Image _icon;
        [SerializeField] PointClickDetectorUI pointClickDetectorUI;

        private ItemMenuData _itemMenuData;
        private Color _oldColor;

        public GridItemReference GridItemReference;
        public Action<GridItemReference> onPointerDown;
        public Action<GridItemReference> onPointerUp;
        private void Awake()
        {
            pointClickDetectorUI.onPointerDown = OnPointerDown;
            pointClickDetectorUI.onPointerUp = OnPointerUp;
        }
        public void Setup(GridItemReference gridItemReference, Action<GridItemReference> onPointerDown, Action<GridItemReference> onPointerUp, bool isDisabled)
        {
            this.GridItemReference = gridItemReference;
            _icon.sprite = gridItemReference.Image;
            this.onPointerDown = onPointerDown;
            this.onPointerUp = onPointerUp;

            if (isDisabled)
            {
                Disable();
            }
            else
            {
                Enable();
            }
        }
        public ItemMenuData GetItemMenuData()
        {
            return _itemMenuData;
        }

        public void EnableItemMenu(bool enable)
        {
            if (!enable)
                _backGroup.color = Color.grey;
            else
                _backGroup.color = _oldColor;


            this.gameObject.GetComponent<PointClickDetectorUI>().enabled = enable;
            _itemMenuData.isSelected = !enable;
        }
        public void Disable()
        {
            _backGroup.color = Color.grey;
            pointClickDetectorUI.enabled = false;
        }
        public void Enable()
        {
            _backGroup.color = Color.white;
            pointClickDetectorUI.enabled = true;
        }

        public void OnPointerDown()
        {
            onPointerDown.Invoke(GridItemReference);
        }

        public void OnPointerUp()
        {
            onPointerUp.Invoke(GridItemReference);
        }
    }
}
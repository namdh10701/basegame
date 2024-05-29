
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts
{
    public class ItemMenu : MonoBehaviour
    {
        [SerializeField] Image _backGroup;
        [SerializeField] Image _icon;
        [SerializeField] TextMeshProUGUI _id;
        [SerializeField] PointClickDetectorUI pointClickDetectorUI;

        public Action<GridItemDef> onPointerDown;
        public Action<GridItemDef> onPointerUp;

        public GridItemDef GridItemDef;
        private void Awake()
        {
            pointClickDetectorUI.onPointerDown += OnPointerDown;
            pointClickDetectorUI.onPointerUp += OnPointerUp;
        }
        private void OnDestroy()
        {
            pointClickDetectorUI.onPointerDown -= OnPointerDown;
            pointClickDetectorUI.onPointerUp -= OnPointerUp;
        }
        public void Setup(GridItemDef gridItemDef, Action<GridItemDef> onPointerDown, Action<GridItemDef> onPointerUp, bool isDisabled)
        {
            this.GridItemDef = gridItemDef;
            Sprite image = ResourceLoader.LoadGridItemImage(gridItemDef);
            _icon.sprite = image;
            _id.text = $"{gridItemDef.Name} {gridItemDef.Id}";
            Debug.Log("Setup Item" + _id.text);
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
            onPointerDown.Invoke(GridItemDef);
        }

        public void OnPointerUp()
        {
            onPointerUp.Invoke(GridItemDef);
        }
    }
}
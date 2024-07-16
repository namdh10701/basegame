using System.Collections;
using _Game.Features.Inventory;
using _Game.Scripts.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;

namespace _Game.Features.MyShip.GridSystem
{
    [Binding]
    [RequireComponent(typeof(Toggle))]
    public class SlotCell : RootViewModel
    {
        public static readonly int WIDTH = 83;
        public static readonly int HEIGHT = 75;

        private GridLayoutGroup _gridLayoutGroup;
        private Toggle _toggle;
        private TMP_Text _debugText;
        public Image SignalImg;
        // private SlotGrid _grid;
        public Color BgColorHighLight = Color.white;
        public Color BgColorNormal = new Color(1, 1, 1, 0);

        #region Binding Prop: IsHighLight

        /// <summary>
        /// IsHighLight
        /// </summary>
        [Binding]
        public bool IsHighLight
        {
            get => _isHighLight;
            set
            {
                if (Equals(_isHighLight, value))
                {
                    return;
                }

                _isHighLight = value;
                OnPropertyChanged(nameof(IsHighLight));
                OnPropertyChanged(nameof(CellBgColor));
            }
        }

        private bool _isHighLight;

        #endregion

        [Binding]
        public Color CellBgColor
        {
            get
            {
                if (!IsAvailable) return new Color(1, 1, 1, 0);

                return IsHighLight ? BgColorHighLight : BgColorNormal;
            }
        }

        public Vector2Int Position { get; private set; }

        public bool IsAvailable => _toggle.isOn;

        // public void SetSignal(bool show)
        // {
        //     var color = SignalImg.color;
        //     color.a = show ? 1 : 0;
        //     SignalImg.color = color;
        // }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator Start()
        {
            yield return null;
            Debug.Log(GetComponent<RectTransform>().rect.width);

            var pos = GridLayoutGroupUtils.GetCellPosition(_gridLayoutGroup, transform.GetSiblingIndex());
            Position = pos;
            gameObject.name = $"Cell {pos.x},{pos.y}";
            _debugText.text = $"{pos.x},{pos.y}";

            // _grid.OnCellReady(this);
        }

        /// <summary>
        /// 
        /// </summary>
        private void Awake()
        {
            _gridLayoutGroup = GetComponentInParent<GridLayoutGroup>();
            _toggle = GetComponent<Toggle>();
            _debugText = GetComponentInChildren<TMP_Text>();
            // _grid = GetComponentInParent<SlotGrid>();
        }
        
        public InventoryItem Data { get; set; }
        
        // public Vector2Int Position => GridLayoutGroupUtils.GetCellPosition(_gridLayoutGroup, transform.GetSiblingIndex());
    }
}

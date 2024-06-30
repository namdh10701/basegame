using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.GD;
using _Game.Scripts.UI;
using Unity.VisualScripting;
using UnityWeld.Binding;

namespace _Game.Features.Shop
{
    [Binding]
    public class PirateViewModel : RootViewModel
    {
        #region Binding Prop: ActiveNavIndex

        private int _activeNavIndex = 0;

        [Binding]
        public int ActiveNavIndex
        {
            get => _activeNavIndex;
            set
            {
                if (_activeNavIndex == value)
                {
                    return;
                }

                _activeNavIndex = value;

                OnPropertyChanged(nameof(ActiveNavIndex));

                // NavTo((Nav)value);
            }
        }

        #endregion

        private List<PirateItem> itemSource = new List<PirateItem>();

        #region Binding: Items

        private ObservableList<PirateItem> items = new ObservableList<PirateItem>();

        [Binding]
        public ObservableList<PirateItem> Items => items;

        #endregion

        #region Binding Prop: IsMultiSelect

        /// <summary>
        /// IsMultiSelect
        /// </summary>
        [Binding]
        public bool IsMultiSelect
        {
            get => _isMultiSelect;
            set
            {
                if (Equals(_isMultiSelect, value))
                {
                    return;
                }

                _isMultiSelect = value;
                OnPropertyChanged(nameof(IsMultiSelect));
            }
        }

        private bool _isMultiSelect;

        #endregion

        public PirateItem HighlightItem
        {
            get => _highlightItem;
            set
            {
                if (_highlightItem != null)
                {
                    _highlightItem.IsHighLight = false;
                }

                _highlightItem = value;
                _highlightItem.IsHighLight = true;
            }
        }
        private PirateItem _highlightItem;

        // #region Binding Prop: FilterItemType
        //
        // [Binding]
        // public ShopItem.ItemType FilterItemType => (ShopItem.ItemType)_filterItemTypeIndex;
        //
        // #endregion

        #region Binding Prop: FilterItemTypeIndex

        private int _filterItemTypeIndex = 0;

        [Binding]
        public int FilterItemTypeIndex
        {
            get => _filterItemTypeIndex;
            set
            {
                if (_filterItemTypeIndex == value)
                {
                    return;
                }

                _filterItemTypeIndex = value;

                OnPropertyChanged(nameof(FilterItemTypeIndex));
                // OnPropertyChanged(nameof(FilterItemType));

                DoFilter();
            }
        }

        #endregion

        private void DoFilter()
        {
            var itemType = (PirateItemType)_filterItemTypeIndex;
            Items.Clear();
            Items.AddRange(itemSource.Where(v => v.Type == itemType));
        }

        void Awake()
        {
            InitializeInternal();
        }

        protected void InitializeInternal()
        {
            for (int i = 0; i < 3; i++)
            {
                itemSource.Add(new PirateItem { Id = "1", PirateViewModel = this, Type = PirateItemType.GOLD, Price = 100 * i,Name = "Rocket ammo" });
            }



            for (int i = 0; i < 30; i++)
            {
                itemSource.Add(new PirateItem { Id = "1", PirateViewModel = this, Type = PirateItemType.GEM, Price = 100 * i, Name = "Rocket ammo" });
            }

            DoFilter();
        }



        [Binding]
        public void OnEquipSelectedItems()
        {
            foreach (var item in Items.Where(v => v.IsSelected))
            {
                item.IsEquipped = true;
            }
        }

        [Binding]
        public void ToggleMultiSelect()
        {
            IsMultiSelect = !IsMultiSelect;
        }
    }
}
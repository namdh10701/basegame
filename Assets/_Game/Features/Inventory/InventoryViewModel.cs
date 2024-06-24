using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.GD;
using _Game.Scripts.UI;
using Unity.VisualScripting;
using UnityWeld.Binding;

namespace _Game.Features.Inventory
{
    [Binding]
    public class InventoryViewModel : RootViewModel
    {
        private List<InventoryItem> itemSource = new List<InventoryItem>();
        
        #region Binding: Items

        private ObservableList<InventoryItem> items = new ObservableList<InventoryItem>();

        [Binding]
        public ObservableList<InventoryItem> Items => items;

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

        public InventoryItem HighlightItem
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
        private InventoryItem _highlightItem;

        // #region Binding Prop: FilterItemType
        //
        // [Binding]
        // public InventoryItem.ItemType FilterItemType => (InventoryItem.ItemType)_filterItemTypeIndex;
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
            var itemType = (ItemType)_filterItemTypeIndex;
            Items.Clear();
            Items.AddRange(itemSource.Where(v => v.Type == itemType));
        }
        //
        // private void Awake()
        // {
        //     GDConfigLoader.Instance.OnLoaded += Init;
        //     GDConfigLoader.Instance.Load();
        // }
        //
        // private void Init()
        // {
        //     
        //     for (int i = 0; i < 3; i++)
        //     {
        //         itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CREW });
        //     }
        //     
        //     // for (int i = 0; i < 5; i++)
        //     // {
        //     //     itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0001"});
        //     // }
        //     // GDConfigLoader.Instance.Cannons["0001"];
        //     itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0001"});
        //     itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
        //     
        //     for (int i = 0; i < 30; i++)
        //     {
        //         itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.AMMO });
        //     }
        //
        // }

        protected void InitializeInternal()
        {
            for (int i = 0; i < 3; i++)
            {
                itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CREW });
            }
            
            // for (int i = 0; i < 5; i++)
            // {
            //     itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0001"});
            // }
            // GDConfigLoader.Instance.Cannons["0001"];
            itemSource.Add(new InventoryItem { InventoryViewModel = this, Type = ItemType.CANNON, Id = "0001"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
            
            for (int i = 0; i < 30; i++)
            {
                itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.AMMO });
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
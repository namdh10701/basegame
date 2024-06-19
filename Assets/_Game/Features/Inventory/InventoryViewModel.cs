using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.GD;
using _Game.Scripts.UI;
using Unity.VisualScripting;
using UnityWeld.Binding;

namespace _Game.Features.Inventory
{
    [Binding]
    public class InventoryViewModel : SubViewModel
    {
        private List<InventoryItem> itemSource = new List<InventoryItem>();
        #region Binding: Items

        private ObservableList<InventoryItem> items = new ObservableList<InventoryItem>();

        [Binding]
        public ObservableList<InventoryItem> Items => items;

        #endregion

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

        private void Awake()
        {
            GDConfigLoader.Instance.OnLoaded += Init;
            GDConfigLoader.Instance.Load();
        }

        private void Init()
        {
            
            for (int i = 0; i < 3; i++)
            {
                itemSource.Add(new InventoryItem { Type = ItemType.CREW });
            }
            
            // for (int i = 0; i < 5; i++)
            // {
            //     itemSource.Add(new InventoryItem { Type = ItemType.CANNON, Id = "0001"});
            // }
            // GDConfigLoader.Instance.Cannons["0001"];
            itemSource.Add(new InventoryItem { Type = ItemType.CANNON, Id = "0001"});
            itemSource.Add(new InventoryItem { Type = ItemType.CANNON, Id = "0012"});
            
            for (int i = 0; i < 30; i++)
            {
                itemSource.Add(new InventoryItem { Type = ItemType.AMMO });
            }

        }
    }
}
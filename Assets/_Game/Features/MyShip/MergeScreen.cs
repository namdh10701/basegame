using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Features.Inventory;
using _Game.Scripts.GD;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityWeld.Binding;
namespace _Game.Features.MergeScreen
{
    [Binding]
    public class MergeScreen : RootViewModel
    {
        #region Binding Prop: IsActiveSuccesFul
        /// <summary>
        /// IsActiveSuccesFul
        /// </summary>
        [Binding]
        public bool IsActiveSuccesFul
        {
            get => _isActiveSuccesFul
            ;
            set
            {
                if (Equals(_isActiveSuccesFul, value))
                {
                    return;
                }
                _isActiveSuccesFul = value;
                OnPropertyChanged(nameof(IsActiveSuccesFul));
            }
        }

        private bool _isActiveSuccesFul;
        #endregion

        #region Binding Prop: CanMerge
        /// <summary>
        /// CanMerge
        /// </summary>
        [Binding]
        public bool CanMerge
        {
            get => _canMerge
            ;
            set
            {
                if (Equals(_canMerge, value))
                {
                    return;
                }
                _canMerge = value;
                OnPropertyChanged(nameof(CanMerge));
            }
        }
        private bool _canMerge;
        #endregion

        #region Binding Prop: NumberItems
        /// <summary>
        /// NumberItems
        /// </summary>
        [Binding]
        public int NumberItems
        {
            get => _numberItems
            ;
            set
            {
                if (Equals(_numberItems, value))
                {
                    return;
                }
                _numberItems = value;
                OnPropertyChanged(nameof(NumberItems));
            }
        }
        private int _numberItems;
        #endregion

        #region Binding Prop: NumberItemRequired
        /// <summary>
        /// NumberItem
        /// </summary>
        [Binding]
        public int NumberItemsRequired
        {
            get => _numberItemsRequired
            ;
            set
            {
                if (Equals(_numberItemsRequired, value))
                {
                    return;
                }
                _numberItemsRequired = value;
                OnPropertyChanged(nameof(NumberItemsRequired));
            }
        }

        private int _numberItemsRequired = 3;
        #endregion

        [Binding]
        public string IdItemMerge { get; set; }

        [Binding]
        public ItemType TypeItemMerge { get; set; }

        #region Binding Prop: SpriteItemMerge
        /// <summary>
        /// Thumbnail
        /// </summary>
        [Binding]
        public Sprite SpriteItemMerge
        {
            get
            {
                switch (TypeItemMerge)
                {
                    case ItemType.CANNON:
                        return _Game.Scripts.DB.Database.GetCannonImage(IdItemMerge);
                    case ItemType.CREW:
                        return _Game.Scripts.DB.Database.GetCrewImage(IdItemMerge);
                    case ItemType.AMMO:
                        return _Game.Scripts.DB.Database.GetAmmoImage(IdItemMerge);
                    default:
                        return null;
                }
            }
        }
        #endregion

        [Binding]
        public string IdItemTarget { get; set; }

        [Binding]
        public ItemType TypeItemTarget { get; set; }

        #region Binding Prop: SpriteItemMerge
        /// <summary>
        /// Thumbnail
        /// </summary>
        [Binding]
        public Sprite SpriteItemTarget
        {
            get
            {
                switch (TypeItemTarget)
                {
                    case ItemType.CANNON:
                        return _Game.Scripts.DB.Database.GetCannonImage(IdItemTarget);
                    case ItemType.CREW:
                        return _Game.Scripts.DB.Database.GetCrewImage(IdItemTarget);
                    case ItemType.AMMO:
                        return _Game.Scripts.DB.Database.GetAmmoImage(IdItemTarget);
                    default:
                        return null;
                }
            }
        }
        #endregion

        #region Binding: InventoryItems
        private ObservableList<InventoryItem> inventoryItems = new ObservableList<InventoryItem>();

        [Binding]
        public ObservableList<InventoryItem> InventoryItems => inventoryItems;
        #endregion

        #region Binding Prop: TypeFiler
        /// <summary>
        /// TypeFiler
        /// </summary>
        [Binding]
        public ItemType TypeFiler
        {
            get => _typeFilter
            ;
            set
            {
                if (Equals(_typeFilter, value))
                {
                    return;
                }
                _typeFilter = value;
                OnPropertyChanged(nameof(TypeFiler));
                DoFilter();
            }
        }

        private ItemType _typeFilter;
        #endregion

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

        public void DoFilter(bool clearSelection = false)
        {
            if (clearSelection)
            {
                foreach (var inventoryItem in dataSource)
                {
                    inventoryItem.IsSelected = false;
                }
            }

            InventoryItems.Clear();
            InventoryItems.AddRange(dataSource.Where(v => v.Type == TypeFiler));
        }


        private List<InventoryItem> dataSource = new List<InventoryItem>();
        public static MergeScreen Instance;

        private void Awake()
        {
            Instance = this;
            InitializeInternal();
        }

        protected void InitializeInternal()
        {
            foreach (var conf in GameData.CannonTable.Records)
            {
                dataSource.Add(new InventoryItem { Type = ItemType.CANNON, Id = conf.Id, Rarity = conf.Rarity, RarityLevel = conf.RarityLevel.ToString(), OperationType = conf.OperationType });
            }

            foreach (var conf in GameData.AmmoTable.Records)
            {
                dataSource.Add(new InventoryItem { Type = ItemType.AMMO, Id = conf.Id, Rarity = conf.Rarity, RarityLevel = conf.RarityLevel.ToString(), OperationType = conf.OperationType });
            }

            var crewNo = 1;
            for (int i = 1; i <= 2; i++)
            {
                var rarities = Enum.GetValues(typeof(Rarity)).Cast<Rarity>();
                foreach (var rarity in rarities)
                {
                    dataSource.Add(new InventoryItem { Type = ItemType.CREW, Id = $"{(crewNo++).ToString().PadLeft(4, '0')}", Rarity = rarity, OperationType = $"{i}" });
                }
            }

            foreach (var inventoryItem in dataSource)
            {
                inventoryItem.SelectionStateChanged += OnSelectionStateChanged;
            }

            dataSource = dataSource.Where(v => v.Thumbnail != null).ToList();
            DoFilter();
        }

        private void OnSelectionStateChanged(InventoryItem item)
        {
            if (item.Type != TypeItemMerge)
            {
                NumberItems = 0;
            }
            TypeFiler = item.Type;
            TypeItemMerge = item.Type;
            OnPropertyChanged(nameof(SpriteItemMerge));
            FilterItemTypeIndex = TypeFiler == ItemType.CANNON ? 0 : 1;
            NumberItems++;
            CanMerge = NumberItems == NumberItemsRequired ? true : false;
        }
    }
}

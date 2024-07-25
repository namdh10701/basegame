using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Features.Inventory;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.SaveLoad;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;

namespace _Game.Features.InventoryItemInfo
{
    [Binding]
    public class EnhanceItemInventoryModal : ModalWithViewModel
    {
        [Binding]
        public InventoryItem InventoryItem { get; set; }

        [Binding]
        public string Id { get; set; }

        [Binding]
        public string IdIngredients { get; set; }


        #region Binding Prop: ItemType
        /// <summary>
        /// ItemType
        /// </summary>
        [Binding]
        public ItemType Type
        {
            get => m_type;
            set
            {
                if (Equals(m_type, value))
                {
                    return;
                }

                m_type = value;
                OnPropertyChanged(nameof(Type));
            }
        }
        private ItemType m_type;
        #endregion

        #region Binding Prop: ItemOperationType
        /// <summary>
        /// OperationType
        /// </summary>
        [Binding]
        public string OperationType
        {
            get => _operationType;
            set
            {
                if (Equals(_operationType, value))
                {
                    return;
                }

                _operationType = value;
                OnPropertyChanged(nameof(OperationType));
            }
        }
        private string _operationType;
        #endregion

        #region Binding Prop: Rarity
        /// <summary>
        /// Rarity
        /// </summary>
        [Binding]
        public Rarity Rarity
        {
            get => _rarity;
            set
            {
                if (Equals(_rarity, value))
                {
                    return;
                }

                _rarity = value;
                OnPropertyChanged(nameof(Rarity));
            }
        }
        private Rarity _rarity;
        #endregion

        #region Binding Prop: RarityLevel
        /// <summary>
        /// RarityLevel
        /// </summary>
        [Binding]
        public string RarityLevel
        {
            get => _rarityLevel;
            set
            {
                if (Equals(_rarityLevel, value))
                {
                    return;
                }

                _rarityLevel = value;
                OnPropertyChanged(nameof(RarityLevel));
            }
        }
        private string _rarityLevel;
        #endregion

        #region Binding Prop: Slot
        /// <summary>
        /// Slot
        /// </summary>
        [Binding]
        public string Slot
        {
            get => _slot;
            set
            {
                if (Equals(_slot, value))
                {
                    return;
                }

                _slot = value;
                OnPropertyChanged(nameof(Slot));
            }
        }
        private string _slot;
        #endregion

        #region Binding: Stars
        private ObservableList<GameObject> stars = new ObservableList<GameObject>();

        [Binding]
        public ObservableList<GameObject> Stars => stars;
        #endregion

        #region Binding Prop: SpriteMainItem
        /// <summary>
        /// Thumbnail
        /// </summary>
        [Binding]
        public Sprite SpriteMainItem
        {
            get
            {
                var path = Type == null || OperationType == null || Rarity == null ? $"Items/item_misc_ship" :
                 $"Items/item_{Type.ToString().ToLower()}_{OperationType.ToLower()}_{Rarity.ToString().ToLower()}";
                return Resources.Load<Sprite>(path);
            }
        }
        #endregion

        #region Binding Prop: Ingredients
        /// <summary>
        /// Thumbnail
        /// </summary>
        [Binding]
        public Sprite Ingredients
        {
            get
            {
                var path = Type == null ? $"Items/item_ammo_arrow_common" : $"Items/item_misc_{Type.ToString().ToLower()}";
                return Resources.Load<Sprite>(path);
            }
        }
        #endregion

        #region Binding Prop: PreviousLevel
        /// <summary>
        /// PreviousLevel
        /// </summary>
        [Binding]
        public int CurrentLevel
        {
            get => _currentLevel;
            set
            {
                if (Equals(_currentLevel, value))
                {
                    return;
                }

                _currentLevel = value;
                OnPropertyChanged(nameof(CurrentLevel));
            }
        }
        private int _currentLevel;
        #endregion

        #region Binding Prop: PreviousLevel
        /// <summary>
        /// PreviousLevel
        /// </summary>
        [Binding]
        public int NextLevel
        {
            get => _nextLevel;
            set
            {
                if (Equals(_nextLevel, value))
                {
                    return;
                }

                _nextLevel = value;
                OnPropertyChanged(nameof(NextLevel));
            }
        }
        private int _nextLevel;
        #endregion

        #region Binding Prop: NumbMiscItemOwner
        /// <summary>
        /// NumbMiscItemOwner
        /// </summary>
        [Binding]
        public int NumbMiscItemOwner
        {
            get => _numbMiscItemOwner;
            set
            {
                if (Equals(_numbMiscItemOwner, value))
                {
                    return;
                }

                _numbMiscItemOwner = value;
                OnPropertyChanged(nameof(NumbMiscItemOwner));
            }
        }
        private int _numbMiscItemOwner;
        #endregion

        #region Binding Prop: NumbGoldOwner
        /// <summary>
        /// NumbGoldOwner
        /// </summary>
        [Binding]
        public int NumbGoldOwner
        {
            get => _numbGoldOwner;
            set
            {
                if (Equals(_numbGoldOwner, value))
                {
                    return;
                }

                _numbGoldOwner = value;
                OnPropertyChanged(nameof(NumbGoldOwner));
            }
        }
        private int _numbGoldOwner;
        #endregion

        #region Binding Prop: NumbGoldRequired
        /// <summary>
        /// NumbGoldRequired
        /// </summary>
        [Binding]
        public int NumbGoldRequired
        {
            get => _numbGoldRequired;
            set
            {
                if (Equals(_numbGoldRequired, value))
                {
                    return;
                }

                _numbGoldRequired = value;
                OnPropertyChanged(nameof(NumbGoldRequired));
            }
        }
        private int _numbGoldRequired;
        #endregion

        #region Binding Prop: NumbMiscItemRequired
        /// <summary>
        /// NumbMiscItemRequired
        /// </summary>
        [Binding]
        public int NumbMiscItemRequired
        {
            get => _numbMiscItemRequired;
            set
            {
                if (Equals(_numbMiscItemRequired, value))
                {
                    return;
                }

                _numbMiscItemRequired = value;
                OnPropertyChanged(nameof(NumbMiscItemRequired));
            }
        }
        private int _numbMiscItemRequired;
        #endregion

        #region Binding Prop: EligibleGold
        /// <summary>
        /// EligibleGold
        /// </summary>
        [Binding]
        public bool EligibleGold
        {
            get => _eligibleGold;
            set
            {
                if (Equals(_eligibleGold, value))
                {
                    return;
                }

                _eligibleGold = value;
                OnPropertyChanged(nameof(EligibleGold));
            }
        }
        private bool _eligibleGold;
        #endregion

        #region Binding Prop: EligibleMiscItem
        /// <summary>
        /// EligibleMiscItem
        /// </summary>
        [Binding]
        public bool EligibleMiscItem
        {
            get => _eligibleMiscItem;
            set
            {
                if (Equals(_eligibleMiscItem, value))
                {
                    return;
                }

                _eligibleMiscItem = value;
                OnPropertyChanged(nameof(EligibleMiscItem));
            }
        }
        private bool _eligibleMiscItem;
        #endregion

        #region Binding Prop: InteractableButtonConfirm
        /// <summary>
        /// InteractableButtonConfirm
        /// </summary>
        [Binding]
        public bool InteractableButtonConfirm
        {
            get => _interactableButtonConfirm;
            set
            {
                if (Equals(_interactableButtonConfirm, value))
                {
                    return;
                }

                _interactableButtonConfirm = value;
                OnPropertyChanged(nameof(InteractableButtonConfirm));
            }
        }
        private bool _interactableButtonConfirm;
        #endregion

        #region Binding Prop: IsActivePopupSuccess
        /// <summary>
        /// IsActivePopupSuccess
        /// </summary>
        [Binding]
        public bool IsActivePopupSuccess
        {
            get => _isActivePopupSuccess;
            set
            {
                if (Equals(_isActivePopupSuccess, value))
                {
                    return;
                }

                _isActivePopupSuccess = value;
                OnPropertyChanged(nameof(IsActivePopupSuccess));
            }
        }
        private bool _isActivePopupSuccess;
        #endregion

        #region Binding Prop: ValuePropertyUpgrade
        /// <summary>
        /// ValuePropertyUpgrade
        /// </summary>
        [Binding]
        public string ValueExtra
        {
            get => _valueExtra;
            set
            {
                if (Equals(_valueExtra, value))
                {
                    return;
                }

                _valueExtra = value;
                OnPropertyChanged(nameof(ValueExtra));
            }
        }
        private string _valueExtra;
        #endregion

        List<ItemData> _miscs = new List<ItemData>();
        InventoryItemUpgradeTableRecord _inventoryItemUpgradeTableRecord = new InventoryItemUpgradeTableRecord();
        public override async UniTask Initialize(Memory<object> args)
        {
            InventoryItem = args.ToArray().FirstOrDefault() as InventoryItem;
            LoadData();
        }

        private void LoadData()
        {
            SetDataInventoryItem(InventoryItem);
            GetResourcesOwner(Type);
        }

        protected void LoadConfigUpgrade()
        {
            switch (Type)
            {
                case ItemType.CANNON:
                    _inventoryItemUpgradeTableRecord = GameData.CannonUpgradeTable.GetGoldAndBlueprintByLevel(CurrentLevel);
                    break;
                case ItemType.AMMO:
                    _inventoryItemUpgradeTableRecord = GameData.AmmoUpgradeTable.GetGoldAndBlueprintByLevel(CurrentLevel);
                    break;
                case ItemType.SHIP:
                    _inventoryItemUpgradeTableRecord = GameData.ShipUpgradeTable.GetGoldAndBlueprintByLevel(CurrentLevel);
                    break;
            }

            NumbGoldRequired = _inventoryItemUpgradeTableRecord.Gold;
            NumbMiscItemRequired = _inventoryItemUpgradeTableRecord.Blueprint;
        }

        protected void SetDataInventoryItem(InventoryItem inventoryItem)
        {
            Id = inventoryItem.Id;
            Type = inventoryItem.Type;
            OperationType = inventoryItem.OperationType;
            Rarity = inventoryItem.Rarity;
            RarityLevel = inventoryItem.RarityLevel;
            Slot = inventoryItem.Slot;
            CurrentLevel = inventoryItem.Level;
            NextLevel = CurrentLevel + 1;
            OnPropertyChanged(nameof(SpriteMainItem));
            OnPropertyChanged(nameof(Ingredients));
            LoadStarsItem();
        }

        protected void GetResourcesOwner(ItemType itemType)
        {
            NumbMiscItemOwner = 0;
            foreach (var item in SaveSystem.GameSave.OwnedItems)
            {
                if (item.ItemId == itemType.ToString().ToLower())
                {
                    _miscs.Add(item);
                    NumbMiscItemOwner++;
                }
            }

            NumbGoldOwner = SaveSystem.GameSave.gold;
            NumbGoldRequired = 0;
            NumbMiscItemRequired = 2;

            EligibleGold = NumbGoldOwner >= NumbGoldRequired;
            EligibleMiscItem = NumbMiscItemOwner >= NumbMiscItemRequired;
            InteractableButtonConfirm = EligibleMiscItem && EligibleGold;
        }

        protected void LoadStarsItem()
        {
            if (Type == ItemType.CREW || Type == ItemType.AMMO) return;

            for (int i = 0; i < int.Parse(RarityLevel); i++)
            {
                Stars.Add(new Star());
            }
        }

        [Binding]
        public async void OnUpgradeItem()
        {
            IsActivePopupSuccess = true;
            RemoveItemMisc(Type);
            GetValuePropertyUpgrade();
            await UniTask.Delay(2000);
            IsActivePopupSuccess = false;
            LoadData();
        }

        protected void RemoveItemMisc(ItemType itemType)
        {
            int itemsRemoved = 0;
            string targetItemId = itemType.ToString().ToLower();
            for (int i = SaveSystem.GameSave.OwnedItems.Count - 1; i >= 0; i--)
            {
                foreach (var item in SaveSystem.GameSave.OwnedItems)
                {
                    if (item.ItemId == Id)
                    {
                        item.Level = NextLevel;
                        InventoryItem.Level = NextLevel;
                        break;
                    }
                }

                if (SaveSystem.GameSave.OwnedItems[i].ItemId == targetItemId)
                {
                    SaveSystem.GameSave.OwnedItems.RemoveAt(i);
                    itemsRemoved++;

                    if (itemsRemoved >= NumbMiscItemRequired)
                    {
                        break;
                    }
                }
            }
            SaveSystem.SaveGame();
        }

        protected void GetValuePropertyUpgrade()
        {
            var dataTableRecord = GameData.CannonTable.GetDataTableRecord(OperationType, Rarity.ToString()) as CannonTableRecord;
            if (Type == ItemType.AMMO || Type == ItemType.CANNON)
            {
                var damage = dataTableRecord.Attack * _inventoryItemUpgradeTableRecord.Effect;
                ValueExtra = $"Attack (+{damage})";
            }
            else
            {
                var hp = dataTableRecord.Hp * _inventoryItemUpgradeTableRecord.Effect;
                ValueExtra = $"HP (+{hp})";

            }


        }

        [Binding]
        public async void Close()
        {
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
        }
    }
}
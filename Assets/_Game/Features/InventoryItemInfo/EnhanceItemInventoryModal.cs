using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Features.Inventory;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.SaveLoad;
using Cysharp.Threading.Tasks;
using Online;
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
        public string OnwItemId { get; set; }

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
                var path = Type == null || OperationType == null || Rarity == null ? $"Images/Items/item_misc_ship" :
                 $"Images/Items/item_{Type.ToString().ToLower()}_{OperationType.ToLower()}_{Rarity.ToString().ToLower()}";
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
                var path = Type == null ? $"Images/Items/item_res_blueprint_ammo" : $"Images/Items/item_res_blueprint_{Type.ToString().ToLower()}";
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

        List<ItemData> _blueSprints = new List<ItemData>();
        InventoryItemUpgradeTableRecord _inventoryItemUpgradeTableRecord = new InventoryItemUpgradeTableRecord();
        public override async UniTask Initialize(Memory<object> args)
        {
            InventoryItem = args.Span[0] as InventoryItem;
            LoadData();
        }

        private void LoadData()
        {
            SetDataInventoryItem(InventoryItem);
            _inventoryItemUpgradeTableRecord = LoadConfigUpgrade(InventoryItem);
            GetResourcesOwner(ItemType.BLUEPRINT, Type.ToString().ToLower());
        }

        protected void SetDataInventoryItem(InventoryItem inventoryItem)
        {
            Id = inventoryItem.Id;
            OnwItemId = inventoryItem.OwnItemId;
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

        protected void GetResourcesOwner(ItemType itemType, string id)
        {
            foreach (var item in SaveSystem.GameSave.OwnedItems)
            {
                if (item.ItemId == id && item.ItemType == itemType)
                {
                    _blueSprints.Add(item);
                }
            }
            NumbMiscItemOwner = _blueSprints.Count;
            LoadConfigUpgrade();

        }
        protected void LoadConfigUpgrade()
        {
            NumbGoldRequired = _inventoryItemUpgradeTableRecord.Gold;
            NumbMiscItemRequired = _inventoryItemUpgradeTableRecord.Blueprint;
            NumbGoldOwner = PlayfabManager.Instance.Gold;

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
            var resUpgrade = await PlayfabManager.Instance.EnhanceItem(OnwItemId);

            if (resUpgrade.Result)
            {
                var itemData = resUpgrade.ItemUpgrade.GetItemData();
                Debug.Log("OnUpgradeItem" + itemData);

                IsActivePopupSuccess = true;
                ChangeValuePropertyUpgrade();

                await UniTask.Delay(2000);
                IsActivePopupSuccess = false;
                UpdataDataItemOwner(Type);
                LoadData();
            }
            else
                Debug.Log("[OnUpgradeItem]" + resUpgrade.Error);

        }

        protected void UpdataDataItemOwner(ItemType itemType)
        {
            string targetItemId = itemType.ToString().ToLower();

            foreach (var item in SaveSystem.GameSave.OwnedItems)
            {
                if (item.ItemId == Id && item.OwnItemId == OnwItemId)
                {
                    item.Level = NextLevel;
                    InventoryItem.Level = NextLevel;
                    CurrentLevel = NextLevel;
                    NextLevel = CurrentLevel + 1;
                    break;
                }
            }
            // RemoveItemsBlueSprint();

        }

        private void RemoveItemsBlueSprint()
        {
            if (NumbMiscItemRequired == 0) return;
            var blueprintsRemove = new List<ItemData>();
            var amountsBlueprintsRemove = 0;
            foreach (var item in SaveSystem.GameSave.OwnedItems)
            {
                if (amountsBlueprintsRemove == NumbMiscItemRequired) return;
                if (item.ItemType == ItemType.BLUEPRINT && item.ItemId == Type.ToString().ToLower())
                {
                    blueprintsRemove.Add(item);
                    amountsBlueprintsRemove++;
                }
            }

            if (blueprintsRemove.Count > 0)
            {
                foreach (var item in blueprintsRemove)
                {
                    SaveSystem.GameSave.OwnedItems.Remove(item);
                }
            }
        }

        protected void ChangeValuePropertyUpgrade()
        {
            switch (Type)
            {
                case ItemType.CANNON:
                    var cannonTable = GameData.CannonTable.FindById(Id);
                    var damageCannonExtra = Math.Ceiling(cannonTable.Attack * _inventoryItemUpgradeTableRecord.Effect);
                    ValueExtra = $"Attack (+{damageCannonExtra})";
                    break;
                case ItemType.AMMO:
                    var amoTable = GameData.AmmoTable.FindById(Id);
                    var damageAmmoExtra = Math.Ceiling(amoTable.AmmoAttack * _inventoryItemUpgradeTableRecord.Effect);
                    ValueExtra = $"Attack (+{damageAmmoExtra})";
                    break;
                case ItemType.SHIP:
                    var shipTable = GameData.ShipTable.FindById(Id);
                    var hpExtra = Math.Ceiling(shipTable.Hp * _inventoryItemUpgradeTableRecord.Effect);
                    ValueExtra = $"Attack (+{hpExtra})";
                    break;
            }
        }

        protected InventoryItemUpgradeTableRecord LoadConfigUpgrade(InventoryItem inventoryItem)
        {
            switch (inventoryItem.Type)
            {
                case ItemType.CANNON:
                    return _inventoryItemUpgradeTableRecord = GameData.CannonUpgradeTable.GetGoldAndBlueprintByLevel(inventoryItem.Level);
                case ItemType.AMMO:
                    return _inventoryItemUpgradeTableRecord = GameData.AmmoUpgradeTable.GetGoldAndBlueprintByLevel(inventoryItem.Level);
                case ItemType.SHIP:
                    return _inventoryItemUpgradeTableRecord = GameData.ShipUpgradeTable.GetGoldAndBlueprintByLevel(inventoryItem.Level);

            }
            return null;
        }

        [Binding]
        public async void Close()
        {
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
        }
    }
}
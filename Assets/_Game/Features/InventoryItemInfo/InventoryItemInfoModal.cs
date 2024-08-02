using System;
using System.Reflection;
using _Game.Features.Inventory;
using _Game.Features.InventoryCustomScreen;
using _Game.Scripts.GD.DataManager;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Views;

namespace _Game.Features.InventoryItemInfo
{
    [Binding]
    public class InventoryItemInfoModal : ModalWithViewModel
    {
        [SerializeField] GameObject _enhance;
        // [SerializeField] GameObject _stars;

        [Binding]
        public InventoryItem InventoryItem { get; set; }

        [Binding]
        public string Id { get; set; }

        [Binding]
        public string OwnItemId { get; set; }

        #region Binding Prop: ItemName
        /// <summary>
        /// ItemName
        /// </summary>
        [Binding]
        public string ItemName
        {
            get => m_name;
            set
            {
                if (Equals(m_name, value))
                {
                    return;
                }

                m_name = value;
                OnPropertyChanged(nameof(ItemName));
            }
        }
        private string m_name;
        #endregion


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

        #region Binding Prop: Level
        /// <summary>
        /// Slot
        /// </summary>
        [Binding]
        public int Level
        {
            get => _level;
            set
            {
                if (Equals(_level, value))
                {
                    return;
                }

                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }
        private int _level;
        #endregion

        #region Binding: Stars
        private ObservableList<Star> stars = new ObservableList<Star>();

        [Binding]
        public ObservableList<Star> Stars => stars;
        #endregion

        #region Binding: Skills
        private ObservableList<SkillInvetoryItem> skills = new ObservableList<SkillInvetoryItem>();

        [Binding]
        public ObservableList<SkillInvetoryItem> Skills => skills;
        #endregion

        #region Binding: ItemStats
        private ObservableList<ItemStat> itemStats = new ObservableList<ItemStat>();

        [Binding]
        public ObservableList<ItemStat> ItemStats => itemStats;
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
                var path = Type == null || OperationType == null || Rarity == null ? $"Images/Items/item_ammo_arrow_common" :
                 $"Images/Items/item_{Type.ToString().ToLower()}_{OperationType.ToLower()}_{Rarity.ToString().ToLower()}";
                return Resources.Load<Sprite>(path);
            }
        }
        #endregion

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
            SetupLayOutItem();
            SetDataItemStat();
            SetSkillData();
        }

        private void SetSkillData()
        {
            Skills.Clear();
            switch (Type)
            {
                case ItemType.CANNON:
                    var skilDataCannonDefault = GameData.CannonTable.GetDataSkillDefault(Id);
                    SkillInvetoryItem skillCannonDefault = new SkillInvetoryItem()
                    {
                        OperationType = "default",
                        Details = skilDataCannonDefault.Item2,
                        Type = Type,
                        Name = skilDataCannonDefault.Item3

                    };

                    if (!string.IsNullOrEmpty(skilDataCannonDefault.Item2))
                    {
                        skillCannonDefault.Setup();
                        Skills.Add(skillCannonDefault);

                    }

                    // var skillDataFever = GameData.CannonFeverTable.GetDataSkillDefault(OperationType, Rarity, RarityLevel);
                    var skillDataFever = GameData.CannonFeverTable.GetDataSkillDefault(Id);
                    SkillInvetoryItem skillFever = new SkillInvetoryItem()
                    {
                        OperationType = "fever",
                        Details = skillDataFever.Item2,
                        Type = Type,
                        Name = skillDataFever.Item3

                    };

                    if (!string.IsNullOrEmpty(skillDataFever.Item2))
                    {
                        skillFever.Setup();
                        Skills.Add(skillFever);
                    }
                    break;
                case ItemType.AMMO:
                    var skillDataAmmoDefault = GameData.AmmoTable.GetDataSkillDefault(OperationType, Rarity, RarityLevel);
                    SkillInvetoryItem skillAmmo = new SkillInvetoryItem()
                    {
                        OperationType = skillDataAmmoDefault.Item1,
                        Details = skillDataAmmoDefault.Item2,
                        Type = Type,
                        Name = skillDataAmmoDefault.Item3

                    };

                    if (!string.IsNullOrEmpty(skillDataAmmoDefault.Item2))
                    {
                        skillAmmo.Setup();
                        Skills.Add(skillAmmo);
                    }
                    break;
                case ItemType.CREW:
                    var skillDataCrewDefault = GameData.CrewTable.GetDataSkillDefault(OperationType, Rarity.ToString());
                    SkillInvetoryItem skillCrew = new SkillInvetoryItem()
                    {
                        OperationType = skillDataCrewDefault.Item1,
                        Details = skillDataCrewDefault.Item2,
                        Type = Type,
                        Name = skillDataCrewDefault.Item3

                    };

                    if (!string.IsNullOrEmpty(skillDataCrewDefault.Item2))
                    {
                        skillCrew.Setup();
                        Skills.Add(skillCrew);
                    }
                    break;
            }
        }

        protected void SetDataInventoryItem(InventoryItem inventoryItem)
        {
            Id = inventoryItem.Id;
            OwnItemId = inventoryItem.OwnItemId;
            ItemName = inventoryItem.Name;
            Type = inventoryItem.Type;
            OperationType = inventoryItem.OperationType;
            Rarity = inventoryItem.Rarity;
            RarityLevel = inventoryItem.RarityLevel;
            Slot = inventoryItem.Slot;
            Level = inventoryItem.Level;
            OnPropertyChanged(nameof(SpriteMainItem));
        }

        protected void SetupLayOutItem()
        {
            var enable = Type == ItemType.CANNON ? true : false;
            _enhance.SetActive(enable);
            LoadStarsItem();
        }

        protected void LoadStarsItem()
        {
            if (Type == ItemType.CREW || Type == ItemType.AMMO) return;

            stars.Clear();

            if (int.TryParse(RarityLevel, out int rarityLevel))
            {
                for (int i = 0; i < rarityLevel; i++)
                {
                    stars.Add(new Star());
                }
            }
            else
            {
                // Handle the case where RarityLevel is not a valid integer
                Debug.LogError($"Invalid RarityLevel: {RarityLevel}");
            }
        }


        private void SetDataItemStat()
        {
            ItemStats.Clear();
            var valueExtra = (float)GetValuePropertyUpgrade();

            DataTableRecord dataTableRecord = null;
            switch (Type)
            {
                case ItemType.CANNON:
                    dataTableRecord = GameData.CannonTable.GetDataTableRecord(Id);
                    var cannonRecord = dataTableRecord as CannonTableRecord;
                    cannonRecord.Attack = cannonRecord.Attack + valueExtra;
                    break;
                case ItemType.AMMO:
                    dataTableRecord = GameData.AmmoTable.GetDataTableRecord(Id);
                    var ammoRecord = dataTableRecord as AmmoTableRecord;
                    ammoRecord.AmmoAttack = ammoRecord.AmmoAttack + valueExtra;
                    break;
                case ItemType.CREW:
                    dataTableRecord = GameData.CrewTable.GetDataTableRecord(Id);
                    var crewRecord = dataTableRecord as CrewTableRecord;
                    break;
                case ItemType.SHIP:
                    dataTableRecord = GameData.ShipTable.GetDataTableRecord(Id);
                    var shipRecord = dataTableRecord as ShipTableRecord;
                    shipRecord.Hp = shipRecord.Hp + valueExtra;
                    break;
            }

            var index = 0;
            if (dataTableRecord != null)
            {
                foreach (var item in dataTableRecord.GetType().GetProperties(BindingFlags.Default | BindingFlags.Public | BindingFlags.Instance))
                {
                    var stat = item.GetCustomAttribute<StatAttribute>();
                    if (stat == null)
                        continue;

                    ItemStat itemStat = new ItemStat();
                    itemStat.Index = index;
                    itemStat.NameProperties = stat.Name;
                    itemStat.Value = item.GetValue(dataTableRecord).ToString();
                    itemStat.Setup();
                    ItemStats.Add(itemStat);
                    index++;
                }
            }

        }

        protected double GetValuePropertyUpgrade()
        {
            switch (Type)
            {
                case ItemType.CANNON:
                    var cannonTable = GameData.CannonTable.GetDataTableRecord(Id) as CannonTableRecord;
                    return Math.Ceiling(cannonTable.Attack * _inventoryItemUpgradeTableRecord.Effect);
                case ItemType.AMMO:
                    var amoTable = GameData.AmmoTable.GetDataTableRecord(Id) as AmmoTableRecord;
                    return Math.Ceiling(amoTable.AmmoAttack * _inventoryItemUpgradeTableRecord.Effect);
                case ItemType.SHIP:
                    var shipTable = GameData.AmmoTable.GetDataTableRecord(Id) as ShipTableRecord;
                    return Math.Ceiling(shipTable.Hp * _inventoryItemUpgradeTableRecord.Effect);

            }
            return -1;
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

        [Binding]
        public async void OnClickCustom()
        {
            string screenName = Type switch
            {
                ItemType.CANNON => nameof(CannonCustomScreen),
                ItemType.AMMO => nameof(CannonCustomScreen),
                ItemType.CREW => nameof(CrewCustomScreen),
                _ => null
            };

            if (screenName != null)
            {
                var options = new ViewOptions(screenName);
                await ModalContainer.Find(ContainerKey.Modals).PushAsync(options, InventoryItem, Skills, ItemStats);
            }
        }

        [Binding]
        public async void OnClickEnhance()
        {
            var options = new ViewOptions(nameof(EnhanceItemInventoryModal));
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options, InventoryItem, _inventoryItemUpgradeTableRecord);
        }

        public override UniTask WillPopEnter(Memory<object> args)
        {
            LoadData();
            return UniTask.CompletedTask;
        }

    }
}
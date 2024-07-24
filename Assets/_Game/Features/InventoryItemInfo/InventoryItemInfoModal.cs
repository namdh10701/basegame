using System;
using System.Linq;
using System.Reflection;
using _Game.Features.Inventory;
using _Game.Features.InventoryCustomScreen;
using _Game.Features.Shop;
using _Game.Scripts.GD.DataManager;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
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
        private ObservableList<GameObject> stars = new ObservableList<GameObject>();

        [Binding]
        public ObservableList<GameObject> Stars => stars;
        #endregion

        #region Binding: Stars
        private ObservableList<SkillInvetoryItem> skill = new ObservableList<SkillInvetoryItem>();

        [Binding]
        public ObservableList<SkillInvetoryItem> Skill => skill;
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
                var path = Type == null || OperationType == null || Rarity == null ? $"Items/item_ammo_arrow_common" :
                 $"Items/item_{Type.ToString().ToLower()}_{OperationType.ToLower()}_{Rarity.ToString().ToLower()}";
                return Resources.Load<Sprite>(path);
            }
        }
        #endregion



        public override async UniTask Initialize(Memory<object> args)
        {
            InventoryItem = args.ToArray().FirstOrDefault() as InventoryItem;
            SetDataInventoryItem(InventoryItem);
            SetupLayOutItem();
            SetDataItemStat();

        }

        protected void SetDataInventoryItem(InventoryItem inventoryItem)
        {
            Id = inventoryItem.Id;
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
            // _stars.SetActive(enable);
            LoadStarsItem();
        }

        protected void LoadStarsItem()
        {
            if (Type == ItemType.CREW || Type == ItemType.AMMO) return;

            for (int i = 0; i < int.Parse(RarityLevel); i++)
            {
                Stars.Add(new Star());
            }
        }

        private void SetDataItemStat()
        {
            itemStats.Clear();
            DataTableRecord dataTableRecord = null;
            if (Type == ItemType.CANNON)
            {
                dataTableRecord = GameData.CannonTable.GetDataTableRecord(OperationType, Rarity.ToString());
            }
            else if (Type == ItemType.CREW)
            {
                dataTableRecord = GameData.CrewTable.GetDataTableRecord(OperationType, Rarity.ToString());
            }

            if (dataTableRecord != null)
            {
                foreach (var item in dataTableRecord.GetType().GetProperties(BindingFlags.Default | BindingFlags.Public | BindingFlags.Instance))
                {
                    var stat = item.GetCustomAttribute<StatAttribute>();
                    if (stat == null)
                        continue;

                    ItemStat itemStat = new ItemStat();
                    itemStat.NameProperties = stat.Name;
                    itemStat.Value = item.GetValue(dataTableRecord).ToString();
                    itemStats.Add(itemStat);
                }
            }

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
                ItemType.CREW => nameof(CrewCustomScreen),
                _ => null
            };

            if (screenName != null)
            {
                var options = new ViewOptions(screenName);
                await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
            }
        }

        [Binding]
        public async void OnClickEnhance()
        {
            var options = new ViewOptions(nameof(EnhanceItemInventoryModal));
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options, InventoryItem);
        }

    }
}
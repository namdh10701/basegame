using System;
using System.Linq;
using _Game.Features.Inventory;
using _Game.Features.InventoryItemInfo;
using _Game.Scripts.GD.DataManager;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;

namespace _Game.Features.InventoryCustomScreen
{
    [Binding]
    public class CrewCustomScreen : ModalWithViewModel
    {
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

        #region Binding Prop: ItemName
        /// <summary>
        /// ItemName
        /// </summary>
        [Binding]
        public string ItemName
        {
            get => _itemName;
            set
            {
                if (Equals(_itemName, value))
                {
                    return;
                }

                _itemName = value;
                OnPropertyChanged(nameof(ItemName));
            }
        }
        private string _itemName;
        #endregion

        #region Binding Prop: RarityItem
        /// <summary>
        /// RarityItem
        /// </summary>
        [Binding]
        public string RarityItem
        {
            get => _rarityItem;
            set
            {
                if (Equals(_rarityItem, value))
                {
                    return;
                }

                _rarityItem = value;
                OnPropertyChanged(nameof(RarityItem));
            }
        }
        private string _rarityItem;
        #endregion

        #region Binding Prop: ColorRarity
        /// <summary>
        /// ColorRarity
        /// </summary>
        [Binding]
        public Color ColorRarity
        {
            get => _colorRarity;
            set
            {
                if (Equals(_colorRarity, value))
                {
                    return;
                }

                _colorRarity = value;
                OnPropertyChanged(nameof(ColorRarity));
            }
        }
        private Color _colorRarity;
        #endregion

        #region Binding Prop: IsActive
        /// <summary>
        /// IsActive
        /// </summary>
        [Binding]
        public bool IsActive
        {
            get => _isActive
            ;
            set
            {
                if (Equals(_isActive
                , value))
                {
                    return;
                }
                _isActive = value;
                OnPropertyChanged(nameof(IsActive));
            }
        }
        private bool _isActive;

        #endregion
        #region Binding Prop: IsActiveAttach
        /// <summary>
        /// IsActiveAttach
        /// </summary>
        [Binding]
        public bool IsActiveAttach
        {
            get => _isActiveAttach;
            set
            {
                if (Equals(_isActiveAttach
                , value))
                {
                    return;
                }
                _isActiveAttach
                 = value;
                OnPropertyChanged(nameof(IsActiveAttach));

            }
        }
        private bool _isActiveAttach;
        #endregion

        #region Binding Prop: IndexButton
        private int _indexButton = 0;
        /// <summary>
        /// IndexButton
        /// </summary>
        [Binding]
        public int IndexButton
        {
            get => _indexButton;
            set
            {
                if (_indexButton == value)
                {
                    return;
                }

                _indexButton = value;

                OnPropertyChanged(nameof(IndexButton));
            }
        }
        #endregion

        #region Binding: Skills
        private ObservableList<SkillInvetoryItem> skills = new ObservableList<SkillInvetoryItem>();

        [Binding]
        public ObservableList<SkillInvetoryItem> Skills => skills;
        #endregion

        #region Binding: ItemStats
        private ObservableList<ItemStat> stats = new ObservableList<ItemStat>();

        [Binding]
        public ObservableList<ItemStat> Stats => stats;
        #endregion

        #region Binding: AttachInfoItems
        private ObservableList<AttachInfoItem> attachInfoItems = new ObservableList<AttachInfoItem>();

        [Binding]
        public ObservableList<AttachInfoItem> AttachInfoItems => attachInfoItems;

        #endregion

        #region Binding: ButtonSlot

        private ObservableList<ButtonSlot> buttonSlots = new ObservableList<ButtonSlot>();

        [Binding]
        public ObservableList<ButtonSlot> ButtonSlots => buttonSlots;

        #endregion
        [SerializeField] ButtonGroupInput _buttonGroupInput;
        public InventoryItem InventoryItem { get; set; }
        public GameObject MainItem;
        UICrew _uiCrew;

        public override async UniTask Initialize(Memory<object> args)
        {
            InventoryItem = args.Span[0] as InventoryItem;
            var itemSkill = args.Span[1] as ObservableList<SkillInvetoryItem>;
            foreach (var item in itemSkill)
            {
                Skills.Add(item);
            }

            var itemStats = args.Span[2] as ObservableList<ItemStat>;
            foreach (var item in itemStats)
            {
                Stats.Add(item);
            }
            LoadData();

        }

        private void LoadData()
        {
            ItemName = InventoryItem.Name;
            Slot = InventoryItem.Slot;
            RarityItem = $"[{InventoryItem.Rarity.ToString()}]";
            SetColorRarity();
            InitDataButtonSlot();

            if (_uiCrew != null)
                Destroy(_uiCrew);

            UICrew prefab = _Game.Scripts.DB.Database.GetUICrew(InventoryItem.Id);
            _uiCrew = Instantiate(prefab, MainItem.transform); ;
            _uiCrew.transform.localPosition = Vector3.zero;
        }

        public void OnAddItem(int indexButtom)
        {
            IsActiveAttach = !IsActiveAttach;
        }

        [Binding]
        public void OnEnableAttachItems()
        {
            IsActiveAttach = true;

        }

        [Binding]
        public void OnDisableAttachItems()
        {
            IsActiveAttach = false;
            _buttonGroupInput.Interactable(true);

        }

        [Binding]
        public void OnEquipSelectedItem()
        {
            var attachInfoItem = new AttachInfoItem();
            foreach (var item in AttachInfoItems.Where(v => v.IsHighlight))
            {
                attachInfoItem = item;
            }

            if (buttonSlots[_indexButton].Type == ItemType.None)
            {
                buttonSlots[_indexButton].UpdateData(attachInfoItem.Id, attachInfoItem.Type, attachInfoItem.Rarity);

                var index = GetIndexAttachInfoItemsById(attachInfoItem.Id, attachInfoItem.Rarity);
                AttachInfoItems.Remove(attachInfoItem);
                OnPropertyChanged(nameof(AttachInfoItems));

            }
            else
            {
                var index = GetIndexAttachInfoItemsById(attachInfoItem.Id, attachInfoItem.Rarity);

                var attachInfoItemReturn = new AttachInfoItem();
                attachInfoItemReturn.Id = buttonSlots[_indexButton].Id;
                attachInfoItemReturn.Type = buttonSlots[_indexButton].Type;
                attachInfoItemReturn.Rarity = buttonSlots[_indexButton].Rarity;
                attachInfoItemReturn.IsHighlight = false;

                AttachInfoItems[index] = attachInfoItemReturn;
                AttachInfoItems[index].Setup();
                OnPropertyChanged(nameof(AttachInfoItems));

                buttonSlots[_indexButton].UpdateData(attachInfoItem.Id, attachInfoItem.Type, attachInfoItem.Rarity);


            }
            _buttonGroupInput.Interactable(true);

        }

        private void InitDataButtonSlot()
        {
            for (int i = 0; i < 4; i++)
            {
                var toggle = new ButtonSlot();
                toggle.Id = "";
                toggle.Type = ItemType.None;
                toggle.SlotName = $"Slot {i}";
                buttonSlots.Add(toggle);
            }
            _buttonGroupInput.Setup();

            for (int i = 0; i < 4; i++)
            {
                AttachInfoItem attachInfoItem = new AttachInfoItem()
                {
                    Id = "eq2",
                    Type = ItemType.MISC,
                    Rarity = (Rarity)i
                };
                attachInfoItem.Setup();
                AttachInfoItems.Add(attachInfoItem);
            }

        }

        private void SetColorRarity()
        {
            switch (InventoryItem.Rarity)
            {
                case Rarity.Common:
                    ColorRarity = Color.grey;
                    break;
                case Rarity.Good:
                    ColorRarity = Color.green;
                    break;
                case Rarity.Rare:
                    ColorRarity = Color.cyan;
                    break;
                case Rarity.Epic:
                    ColorRarity = new Color(194, 115, 241, 255);
                    break;
                case Rarity.Legend:
                    ColorRarity = Color.yellow;
                    break;
            }
        }

        public int GetIndexAttachInfoItemsById(string id, Rarity rarity)
        {
            for (int i = 0; i < AttachInfoItems.Count; i++)
            {
                if (AttachInfoItems[i].Id == id && AttachInfoItems[i].Rarity == rarity)
                {
                    return i;
                }
            }
            return -1; // Trả về -1 nếu không tìm thấy
        }

        [Binding]
        public async void Close()
        {
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
        }
    }
}

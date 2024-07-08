using System.Linq;
using _Game.Features.Inventory;
using _Game.Scripts.UI;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.InventoryCustomScreen
{
    public interface IInventoryCustomScreen
    {
        public bool IsActive { get; set; }
        public bool IsActiveAttach { get; set; }
        public int IndexButton { get; set; }
        public void OnEnableAttachItems();
    }

    public enum InventoryCustomScreenType
    {
        GunCustomScreen,
        CrewCustomScreen
    }

    [Binding]
    public class CrewCustomScreen : RootViewModel, IInventoryCustomScreen
    {
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
        private bool _isActiveAttach;

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

        public ItemType AttachItemType = ItemType.None;
        public string AttachItemId { get; set; }

        #region Binding: SkillInfoItems

        private ObservableList<SkillInfoItem> skillInfoItems = new ObservableList<SkillInfoItem>();

        [Binding]
        public ObservableList<SkillInfoItem> SkillInfoItems => skillInfoItems;

        #endregion

        #region Binding: SkinInfoItems

        private ObservableList<SkinInfoItem> skinInfoItems = new ObservableList<SkinInfoItem>();

        [Binding]
        public ObservableList<SkinInfoItem> SkinInfoItems => skinInfoItems;

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
        public static CrewCustomScreen Instance;
        async void Awake()
        {
            Instance = this;
            InitDataTest();
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
                AttachItemId = item.Id;
                AttachItemType = item.Type;
                attachInfoItem = item;
            }

            if (buttonSlots[_indexButton].Type == ItemType.None)
            {
                buttonSlots[_indexButton].UpdateData(AttachItemId, AttachItemType);
                AttachInfoItems.Remove(attachInfoItem);
            }
            else
            {
                var attachInfoItemReturn = new AttachInfoItem();
                attachInfoItemReturn.Id = buttonSlots[_indexButton].Id;
                attachInfoItemReturn.Type = buttonSlots[_indexButton].Type;
                attachInfoItemReturn.IsHighlight = false;
                AttachInfoItems.Add(attachInfoItemReturn);

                buttonSlots[_indexButton].UpdateData(AttachItemId, AttachItemType);
                AttachInfoItems.Remove(attachInfoItem);
            }
            _buttonGroupInput.Interactable(true);

        }

        public void InitDataTest()
        {
            for (int i = 0; i < 2; i++)
            {
                var SkillInfoItem = new SkillInfoItem();
                SkillInfoItem.Id = "0001";
                SkillInfoItem.SkillName = "Skill Nam 1";
                SkillInfoItem.Type = ItemType.CREW;
                SkillInfoItem.Details = "111111111111111111111111111111111111111111";
                skillInfoItems.Add(SkillInfoItem);
            }


            var SkinInfoItem = new SkinInfoItem();
            SkinInfoItem.Id = "0001";
            SkinInfoItem.Type = ItemType.CANNON;
            SkinInfoItem.Details = "22222222222";
            skinInfoItems.Add(SkinInfoItem);


            for (int i = 0; i < 3; i++)
            {
                var tachItem = new AttachInfoItem();
                tachItem.Id = $"000{i + 1}";
                tachItem.Type = ItemType.CREW;
                attachInfoItems.Add(tachItem);
            }

            for (int i = 0; i < 4; i++)
            {
                var toggle = new ButtonSlot();
                toggle.Id = "";
                toggle.Type = ItemType.None;
                toggle.SlotName = $"Slot {i}";
                buttonSlots.Add(toggle);
            }
        }
    }
}

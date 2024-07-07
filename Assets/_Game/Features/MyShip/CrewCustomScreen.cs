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
        public int FilterItemTypeIndex { get; set; }
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
        [SerializeField] GameObject _attachItems;
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
                _isActive
                 = value;
                OnPropertyChanged(nameof(IsActive));
            }
        }

        private bool _isActive
        ;

        #endregion

        #region Binding Prop: IsActiveAttach
        private bool _isActiveAttach = true;

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

        #region Binding Prop: FilterItemTypeIndex

        private int _filterItemTypeIndex = 0;
        /// <summary>
        /// FilterItemTypeIndex
        /// </summary>
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
            }
        }

        #endregion

        #region Binding Prop: Interactable

        private bool _interactable = true;
        /// <summary>
        /// Interactable
        /// </summary>
        [Binding]
        public bool Interactable
        {
            get => _interactable;
            set
            {
                if (_interactable == value)
                {
                    return;
                }

                _interactable = value;

                OnPropertyChanged(nameof(Interactable));
            }
        }

        #endregion

        public ItemType AttachItemType;
        public string AttachItemId { get; set; }


        #region Binding Prop: Thumbnail
        /// <summary>
        /// Thumbnail
        /// </summary>
        [Binding]
        public Sprite Thumbnail
        {
            get
            {
                switch (AttachItemType)
                {
                    case ItemType.CANNON:
                        return _Game.Scripts.DB.Database.GetCannonImage(AttachItemId);
                    case ItemType.CREW:
                        return _Game.Scripts.DB.Database.GetCrewImage(AttachItemId);
                    case ItemType.AMMO:
                        return _Game.Scripts.DB.Database.GetAmmoImage(AttachItemId);
                    default:
                        return null;
                }
            }
        }
        #endregion

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

        public static CrewCustomScreen Instance;

        async void Awake()
        {
            Instance = this;
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
                tachItem.Id = "0001";
                tachItem.Type = ItemType.CREW;
                attachInfoItems.Add(tachItem);
            }


        }

        public void OnAddItem(int indexButtom)
        {
            IsActiveAttach = !IsActiveAttach;
        }

        [Binding]
        public void OnEnableAttachItems()
        {
            // _attachItems.SetActive();
            Interactable = !Interactable;
            IsActiveAttach = !IsActiveAttach;
        }

        [Binding]
        public void OnEquipSelectedItem()
        {
            foreach (var item in AttachInfoItems.Where(v => v.IsHighlight))
            {
                AttachItemId = item.Id;
                AttachItemType = item.Type;
            }
        }
    }
}

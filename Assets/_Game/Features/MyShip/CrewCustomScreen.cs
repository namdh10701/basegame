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

        public static CrewCustomScreen Instance;

        async void Awake()
        {
            Instance = this;
            for (int i = 0; i < 2; i++)
            {
                var SkillInfoItem = new SkillInfoItem();
                SkillInfoItem.Id = "i";
                SkillInfoItem.SkillName = "Skill Nam 1";
                SkillInfoItem.Type = ItemType.CREW;
                SkillInfoItem.Details = "111111111111111111111111111111111111111111";
                skillInfoItems.Add(SkillInfoItem);
            }


            var SkinInfoItem = new SkinInfoItem();
            SkinInfoItem.Id = "0001";
            SkinInfoItem.Type = ItemType.CREW;
            SkinInfoItem.Details = "22222222222";
            skinInfoItems.Add(SkinInfoItem);


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
    }
}
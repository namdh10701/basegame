using _Game.Scripts.UI;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.InventoryCustomScreen
{
    [Binding]
    public class InventoryCustomScreen : RootViewModel
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
                _isActive
                 = value;
                OnPropertyChanged(nameof(IsActive));
            }
        }

        private bool _isActive
        ;

        #endregion

        #region Binding Prop: IsActiveAttach

        /// <summary>
        /// IsActiveAttach
        /// </summary>
        [Binding]
        public bool IsActiveAttach
        {
            get => _isActiveAttach
            ;
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

        public static InventoryCustomScreen Instance;

        async void Awake()
        {
            Instance = this;
        }

        public void OnAddItem(int indexButtom)
        {
            IsActiveAttach = !IsActiveAttach;
        }
    }
}
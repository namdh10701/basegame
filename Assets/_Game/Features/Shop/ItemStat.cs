using System;
using _Game.Scripts.UI;
using UnityWeld.Binding;
namespace _Game.Features.InventoryItemInfo
{
    [Binding]
    public class ItemStat : SubViewModel
    {
        [Binding]
        public int Index { get; set; }

        #region Binding Prop: IsActiveBG
        /// <summary>
        /// IsActiveBG
        /// </summary>
        [Binding]
        public bool IsActiveBG
        {
            get => _isActiveBG;
            set
            {
                if (Equals(_isActiveBG, value))
                {
                    return;
                }

                _isActiveBG = value;
                OnPropertyChanged(nameof(IsActiveBG));
            }
        }
        private bool _isActiveBG;
        #endregion

        #region Binding Prop: NameProperties
        /// <summary>
        /// NameProperties
        /// </summary>
        [Binding]
        public string NameProperties
        {
            get => _nameProperties;
            set
            {
                if (Equals(_nameProperties, value))
                {
                    return;
                }

                _nameProperties = value;
                OnPropertyChanged(nameof(NameProperties));
            }
        }
        private string _nameProperties;
        #endregion

        #region Binding Prop: Value
        /// <summary>
        /// Value
        /// </summary>
        [Binding]
        public string Value
        {
            get => _value;
            set
            {
                if (Equals(_value, value))
                {
                    return;
                }

                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
        private string _value;
        #endregion
        internal void Setup()
        {
            IsActiveBG = Index % 2 == 0 ? true : false;
        }
    }
}

using _Game.Scripts.UI;
using UnityWeld.Binding;
namespace _Game.Features.Shop
{
    [Binding]
    public class ItemStat : SubViewModel
    {
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
    }
}

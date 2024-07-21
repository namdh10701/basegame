using _Game.Scripts.UI;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.Shop
{
    [Binding]
    public class ShopPirateItemReceived : SubViewModel
    {
        #region Binding Prop: Type
        /// <summary>
        /// Type
        /// </summary>
        [Binding]
        public string Type
        {
            get => _type;
            set
            {
                if (Equals(_type, value))
                {
                    return;
                }

                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }
        private string _type;
        #endregion

        #region Binding Prop: Amount
        /// <summary>
        /// Amount
        /// </summary>
        [Binding]
        public string Amount
        {
            get => _amount;
            set
            {
                if (Equals(_amount, value))
                {
                    return;
                }

                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }
        private string _amount;
        #endregion

        [Binding]
        public Sprite Thumbnail
        {
            get
            {
                var path = Type == null ? $"Images/ShopPirate/other/gem" :
                $"Images/ShopPirate/other/{Type.ToLower()}";
                return Resources.Load<Sprite>(path);
            }
        }

    }
}
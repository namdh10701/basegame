using _Game.Scripts.UI;
using UnityWeld.Binding;

namespace _Game.Features.GamePause
{

    [Binding]
    public class TempItem : SubViewModel
    {
        [Binding]
        public string Id { get; set; }
        
        #region Binding Prop: Name

        /// <summary>
        /// Name
        /// </summary>
        [Binding]
        public string Name
        {
            get => m_name;
            set
            {
                if (Equals(m_name, value))
                {
                    return;
                }

                m_name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string m_name;

        #endregion

        #region Binding Prop: Quantity

        /// <summary>
        /// Quantity
        /// </summary>
        [Binding]
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (Equals(_quantity, value))
                {
                    return;
                }

                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        private int _quantity;

        #endregion

    }
}
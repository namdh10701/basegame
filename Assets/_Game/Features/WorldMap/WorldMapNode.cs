using _Game.Scripts.UI;
using UnityWeld.Binding;

namespace _Game.Features.WorldMap
{
    [Binding]
    public class WorldMapNode : SubViewModel
    {
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

        #region Binding Prop: IsCompleted

        /// <summary>
        /// IsCompleted
        /// </summary>
        [Binding]
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                if (Equals(_isCompleted, value))
                {
                    return;
                }

                _isCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
            }
        }

        private bool _isCompleted;

        #endregion
        
        #region Binding Prop: IsSelected

        /// <summary>
        /// IsSelected
        /// </summary>
        [Binding]
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (Equals(_isSelected, value))
                {
                    return;
                }

                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        private bool _isSelected;

        #endregion
        
        #region Binding Prop: IsLocked

        /// <summary>
        /// IsLocked
        /// </summary>
        [Binding]
        public bool IsLocked
        {
            get => _isLocked;
            set
            {
                if (Equals(_isLocked, value))
                {
                    return;
                }

                _isLocked = value;
                OnPropertyChanged(nameof(IsLocked));
            }
        }

        private bool _isLocked;

        #endregion
    }
}
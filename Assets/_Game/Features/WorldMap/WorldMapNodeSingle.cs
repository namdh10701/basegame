using _Base.Scripts.UI.Managers;
using _Game.Scripts.UI;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Screens;
using ZBase.UnityScreenNavigator.Core.Views;

namespace _Game.Features.WorldMap
{
    [Binding]
    public class WorldMapNodeSingle : RootViewModel
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
                if (value)
                {
                    OnSelected();
                }
                
                if (Equals(_isSelected, value))
                {
                    return;
                }

                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        private async void OnSelected()
        {
            PopupManager.Instance.ShowPopup<SeaMapNodeInfoPopup.SeaMapNodeInfoPopup>();
            
            // var options = new ViewOptions("SeaMapScreen", true);
            // await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);
            // ModalContainer.Find(ContainerKey.Modals).Push(options);
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
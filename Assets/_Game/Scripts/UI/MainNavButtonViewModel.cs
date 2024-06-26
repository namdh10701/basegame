using UnityWeld.Binding;

namespace _Game.Scripts.UI
{
    [Binding]
    public class MainNavButtonViewModel: RootViewModel
    {
        #region Binding Prop: IsActive

        private bool _isActive = false;

        [Binding]
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;

                OnPropertyChanged(nameof(IsActive));
                OnPropertyChanged(nameof(FlexibleWidth));
            }
        }

        #endregion
        
        #region Binding Prop: FlexibleWidth

        [Binding]
        public float FlexibleWidth => IsActive ? 1.25f : 1f;

        #endregion
    }
}
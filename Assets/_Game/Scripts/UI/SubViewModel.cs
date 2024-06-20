using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using _Game.Features.Inventory;
using UnityWeld.Binding;

namespace _Game.Scripts.UI
{
    [Binding]
    public abstract class SubViewModel: INotifyPropertyChanged, IInitializable
    {

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion

        private bool _isInitialized;
        public void Initialize()
        {
            InitializeInternal();
            _isInitialized = true;
        }

        protected abstract void InitializeInternal();

        public bool IsInitialized => _isInitialized;
    }
}
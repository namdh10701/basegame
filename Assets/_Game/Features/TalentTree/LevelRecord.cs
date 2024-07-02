using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityWeld.Binding;

namespace _Game.Scripts.Gameplay.TalentTree
{
    [Binding]
    public class LevelRecord : INotifyPropertyChanged
    {
        #region Binding Prop: NormalNode

        private NodeViewModel _normalNode = new();

        [Binding]
        public NodeViewModel NormalNode
        {
            get => _normalNode;
            set
            {
                if (_normalNode == value)
                {
                    return;
                }

                _normalNode = value;

                OnPropertyChanged(nameof(NormalNode));
            }
        }

        #endregion

        #region Binding Prop: LevelNode

        /// <summary>
        /// LevelNode
        /// </summary>
        [Binding]
        public NodeViewModel LevelNode
        {
            get => _levelNode;
            set
            {
                if (Equals(_levelNode, value))
                {
                    return;
                }

                _levelNode = value;
                OnPropertyChanged(nameof(LevelNode));
            }
        }

        private NodeViewModel _levelNode;

        #endregion

        #region Binding Prop: PremiumNode

        /// <summary>
        /// PremiumNode
        /// </summary>
        [Binding]
        public NodeViewModel PremiumNode
        {
            get => _premiumNode;
            set
            {
                if (Equals(_premiumNode, value))
                {
                    return;
                }

                _premiumNode = value;
                OnPropertyChanged(nameof(PremiumNode));
            }
        }

        private NodeViewModel _premiumNode;

        #endregion
        
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
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Scripts.Gameplay.TalentTree
{
    [Serializable]
    [Binding]
    public class NodeViewModel : INotifyPropertyChanged
    {
        private float _canvasGroupAlpha = 1f;

        [Binding]
        public float CanvasGroupAlpha
        {
            get => _canvasGroupAlpha;
            set
            {
                _canvasGroupAlpha = value;

                OnPropertyChanged(nameof(CanvasGroupAlpha));
            }
        }

        [Binding]
        public void Test()
        {
            CanvasGroupAlpha -= 0.1f;
        }
        
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
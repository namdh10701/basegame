using _Base.Scripts.Audio;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using ZBase.UnityScreenNavigator.Core.Modals;

namespace _Game.Features
{
    public class ModalWithViewModel : Modal, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;


        public override UniTask WillPopExit(Memory<object> args)
        {
            AudioManager.Instance.PlayPopupClose();
            return base.WillPopExit(args);
        }

        public override UniTask WillPushEnter(Memory<object> args)
        {
            AudioManager.Instance.PlayPopupOpen();
            return base.WillPushEnter(args);
        }

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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Slash.Unity.DataBind.Core.Data;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Scripts.Gameplay.TalentTree
{
    [Binding]
    public class TalentTreeViewModel : MonoBehaviour, INotifyPropertyChanged
    {
        private ObservableList<LevelRecord> items = new ObservableList<LevelRecord>()
        {
            new LevelRecord() { },
            new LevelRecord(),
            new LevelRecord()
        };

        [Binding]
        public ObservableList<LevelRecord> Items
        {
            get
            {
                return items;
            }
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
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
        #region Binding Prop: CanvasGroupAlpha

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

        #endregion

        #region Binding Prop: Cost

        /// <summary>
        /// Cost
        /// </summary>
        [Binding]
        public float Cost
        {
            get => m_cost;
            set
            {
                if (Equals(m_cost, value))
                {
                    return;
                }

                m_cost = value;
                OnPropertyChanged(nameof(Cost));
            }
        }

        private float m_cost;

        #endregion

        #region Binding Prop: ItemId

        /// <summary>
        /// ItemId
        /// </summary>
        [Binding]
        public string ItemId
        {
            get => m_itemId;
            set
            {
                if (Equals(m_itemId, value))
                {
                    return;
                }

                m_itemId = value;
                OnPropertyChanged(nameof(ItemId));
            }
        }

        private string m_itemId;

        #endregion

        #region Binding Prop: Level

        /// <summary>
        /// Level
        /// </summary>
        [Binding]
        public int Level
        {
            get => m_level;
            set
            {
                if (Equals(m_level, value))
                {
                    return;
                }

                m_level = value;
                OnPropertyChanged(nameof(Level));
            }
        }

        private int m_level;

        #endregion
        
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
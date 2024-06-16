using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using _Game.Scripts.UI;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Scripts.Gameplay
{
    [Binding]
    public class AppViewModel: ViewModel 
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
    }
}
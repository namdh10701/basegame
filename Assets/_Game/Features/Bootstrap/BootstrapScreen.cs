using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.Bootstrap
{
    [Binding]
    public class BootstrapScreen : ScreenWithViewModel
    {
        #region Binding Prop: LoadingProgress

        /// <summary>
        /// LoadingProgress
        /// </summary>
        [Binding]
        public float LoadingProgress
        {
            get => _loadingProgress;
            set
            {
                if (Equals(_loadingProgress, value))
                {
                    return;
                }

                _loadingProgress = value;
                OnPropertyChanged(nameof(LoadingProgress));
                OnPropertyChanged(nameof(LoadingInfo));
            }
        }

        private float _loadingProgress;

        #endregion

        #region Binding Prop: LoadingInfo

        /// <summary>
        /// LoadingInfo
        /// </summary>
        [Binding]
        public string LoadingInfo => $"Loading data...{Mathf.RoundToInt(LoadingProgress * 100)}%";

        #endregion
    }
}

using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.UI;
using UnityEngine;
using UnityWeld.Binding;
namespace _Game.Features.Shop
{
    public class PirateTabNavsViewModel : RootViewModel
    {
        #region Binding Prop: Name

        /// <summary>
        /// Name
        /// </summary>
        [Binding]
        public CanvasGroup CanvasGroup
        {
            get => m_CanvasGroup;
            set
            {
                if (Equals(m_CanvasGroup, value))
                {
                    return;
                }

                m_CanvasGroup = value;
                OnPropertyChanged(nameof(CanvasGroup));
            }
        }

        private CanvasGroup m_CanvasGroup;

        #endregion
    }
}

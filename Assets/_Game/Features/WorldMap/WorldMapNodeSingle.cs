using System;
using _Game.Features.FightNodeInfoPopup;
using _Game.Features.SeaMap;
using _Game.Scripts.UI;
using UnityEngine;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Screens;
using ZBase.UnityScreenNavigator.Core.Views;

namespace _Game.Features.WorldMap
{
    [Binding]
    public class WorldMapNodeSingle : RootViewModel
    {
        #region Binding Prop: IsCompleted

        /// <summary>
        /// IsCompleted
        /// </summary>
        [Binding]
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                if (Equals(_isCompleted, value))
                {
                    return;
                }

                _isCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
            }
        }

        private bool _isCompleted;

        #endregion
        
        #region Binding Prop: IsCurrentNode

        /// <summary>
        /// IsCurrentNode
        /// </summary>
        [Binding]
        public bool IsCurrentNode
        {
            get => _isCurrentNode;
            set
            {
                if (Equals(_isCurrentNode, value))
                {
                    return;
                }

                _isCurrentNode = value;
                OnPropertyChanged(nameof(IsCurrentNode));
            }
        }

        private bool _isCurrentNode;

        #endregion

        /// <summary>
        /// Id
        /// </summary>
        public string Id;

        private void Awake()
        {
            
        }

        private void Start()
        {
            IsCurrentNode = PlayerPrefs.GetString("currentStage") == Id;
        }

        [Binding]
        public void OnClick()
        {
            // IsCompleted = true;
            // ModalContainer.Find(ContainerKey.Modals).PushAsync(new
            //     ViewOptions(nameof(FightNodeInfoModal)), 
            //     Id);
            
            ScreenContainer.Find(ContainerKey.Screens).PushAsync(new
                    ViewOptions(nameof(SeaMapScreen)), 
                Id);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Features.Battle;
using _Game.Features.BattleLoading;
using _Game.Features.Gameplay;
using _Game.Features.Home;
using _Game.Features.Inventory;
using _Game.Scripts.Gameplay;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.UI.Utils;
using Cysharp.Threading.Tasks;
using Map;
using UnityEngine;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Screens;

namespace _Game.Features.FightNodeInfoPopup
{
    [Binding]
    public class FightNodeInfoModal : ModalWithViewModel
    {
        #region Binding Prop: NodeThumbnail

        /// <summary>
        /// NodeThumbnail
        /// </summary>
        [Binding]
        public Sprite NodeThumbnail => _mapNode == null ? null : NodeSprites[_mapNode.nodeType];

        #endregion
        
        // private string _stageId;
        
        public enum Style
        {
            Normal,
            Boss
        }

        public Style style = Style.Normal;

        [Binding] 
        public bool IsBossStyle => style == Style.Boss;
        
        #region Binding: Items

        private ObservableList<InventoryItem> items = new ObservableList<InventoryItem>();

        [Binding]
        public ObservableList<InventoryItem> Items => items;

        #endregion


        [Binding]
        public async void NavToBattle()
        {
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
            var screenContainer = ScreenContainer.Find(ContainerKey.Screens);
            
            // TODO load data here
            // var data = load(_stageId)
            
            await screenContainer.PushAsync(new 
                ScreenOptions(nameof(BattleLoadingScreen), stack: false));
            
            await UniTask.Delay(3000);
            
            await screenContainer.PushAsync(
                new ScreenOptions(nameof(BattleScreen), stack: false));

        }

        [Binding]
        public async void NavToMyShip()
        {
            ScreenContainer.Find(ContainerKey.Screens).PushAsync(
                new ScreenOptions(nameof(MainScreen), false));
            MainViewModel.Instance.ActiveMainNavIndex = (int)MainViewModel.Nav.SHIP;
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
        }


        private Dictionary<NodeType, Sprite> NodeSprites;

        protected override void Awake()
        {
            base.Awake();
            NodeSprites = new()
            {
                { NodeType.Boss, Resources.Load<Sprite>("Images/SeaMap/node_Boss") },
                { NodeType.MiniBoss, Resources.Load<Sprite>("Images/SeaMap/node_MiniBoss") },
                { NodeType.MinorEnemy, Resources.Load<Sprite>("Images/SeaMap/node_Monster") },
                { NodeType.Treasure, Resources.Load<Sprite>("Images/SeaMap/node_Treasure") },
                { NodeType.Mystery, Resources.Load<Sprite>("Images/SeaMap/node_Unknown") },
                { NodeType.Armory, Resources.Load<Sprite>("Images/SeaMap/node_MyShip") },
            };
        }

        #region Binding Prop: MapNode

        /// <summary>
        /// MapNode
        /// </summary>
        [Binding]
        public Node MapNode
        {
            get => _mapNode;
            set
            {
                if (Equals(_mapNode, value))
                {
                    return;
                }

                _mapNode = value;
                OnPropertyChanged(nameof(MapNode));
                OnPropertyChanged(nameof(NodeThumbnail));
            }
        }

        private Node _mapNode;

        #endregion

        public override async UniTask Initialize(Memory<dynamic> args)
        {
            MapNode = args.Span[0] as Node;

            if (MapNode == null)
            {
                return;
            }
            
            // _stageId = args.ToArray().FirstOrDefault() as string;

            // if (string.IsNullOrEmpty(_stageId))
            // {
            //     return;
            // }
            
            Items.Clear();

            Items.Add(GameData.CannonTable.FindById("0001"));
            Items.Add(GameData.CannonTable.FindById("0002"));
            Items.Add(GameData.CannonTable.FindById("0005"));
            Items.Add(GameData.CannonTable.FindById("0010"));
            Items.Add(GameData.CannonTable.FindById("0022"));
            
            OnPropertyChanged(nameof(Items));
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using _Game.Features.Dialogs;
using _Game.Features.Inventory;
using _Game.Scripts.DB;
using _Game.Scripts.UI;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityWeld.Binding;

namespace _Game.Features.Ranking
{
    [Binding]
    public class RankingBattleConfirmModal : AsyncModal<bool, RankingBattleConfirmModal.Params>
    {
        [Binding] 
        public ObservableList<OwnedConsumableResource> Resources { get; set; } = new();
        
        [Binding]
        public async void OnOk()
        {
            await Close(true);
        }
        
        [Binding]
        public async void OnCancel()
        {
            await Close(false);
        }
        
        protected override UniTask InternalInitialize(Params args)
        {
            Resources.Clear();
            Resources.AddRange(args.Resources);
            return UniTask.CompletedTask;
        }
        
        public static async Task<bool> Show(Params data)
        {
            return await Show<RankingBattleConfirmModal>(data);
        }
        
        public class Params
        {
            public List<OwnedConsumableResource> Resources;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Binding]
    public class OwnedConsumableResource: SubViewModel
    {
        public int NeedAmount;
        public int TotalAmount;
        
        public string ItemId { get; set; }
        
        #region Binding Prop: ItemType
        /// <summary>
        /// ItemType
        /// </summary>
        [Binding]
        public ItemType ItemType
        {
            get => _mItemType;
            set
            {
                if (Equals(_mItemType, value))
                {
                    return;
                }

                _mItemType = value;
                OnPropertyChanged(nameof(ItemType));
            }
        }
        private ItemType _mItemType;

        #endregion

        [Binding] public string ConsumeAmountInfo => $"{NeedAmount}/{TotalAmount}";
        
        [Binding]
        public Sprite Thumbnail => Database.GetItemSprite(ItemType, ItemId);
    }
}
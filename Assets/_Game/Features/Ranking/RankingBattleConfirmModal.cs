using System.Collections.Generic;
using System.Threading.Tasks;
using _Game.Features.Dialogs;
using _Game.Features.Inventory;
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
            Resources.AddRange(args.Resources);
            return UniTask.CompletedTask;
        }
        
        public static async Task<bool> Show(Params data)
        {
            return await Show<ConfirmModal>(data);
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
        public int ConsumeAmount;
        public int CurrentAmount;
        
        public string Id { get; set; }
        
        #region Binding Prop: ItemType
        /// <summary>
        /// ItemType
        /// </summary>
        [Binding]
        public ItemType Type
        {
            get => m_type;
            set
            {
                if (Equals(m_type, value))
                {
                    return;
                }

                m_type = value;
                OnPropertyChanged(nameof(Type));
            }
        }
        private ItemType m_type;

        #endregion

        [Binding] public string ConsumeAmountInfo => $"{ConsumeAmount}/{CurrentAmount}";
        
        [Binding]
        public Sprite Thumbnail
        {
            get
            {
                var path = Id == null ? $"Items/item_ammo_arrow_common" : $"Items/item_misc_{Id.ToString().ToLower()}";
                return Resources.Load<Sprite>(path);
            }
        }
    }
}
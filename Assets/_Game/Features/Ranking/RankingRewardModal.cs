using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityWeld.Binding;

namespace _Game.Features.RankingReward
{
    [Binding]
    public class RankingRewardModal : ModalWithViewModel
    {
        
        private List<RankingRewardItem> itemSource = new List<RankingRewardItem>();
        
        #region Binding: Items
        [Binding] 
        public ObservableList<RankingRewardItem> Items { get; set; } = new();
        #endregion

        #region Binding Prop: FilterItemTypeIndex

        private int _filterItemTypeIndex = 0;

        [Binding]
        public int FilterItemTypeIndex
        {
            get => _filterItemTypeIndex;
            set
            {
                if (_filterItemTypeIndex == value)
                {
                    return;
                }

                _filterItemTypeIndex = value;

                OnPropertyChanged(nameof(FilterItemTypeIndex));
                // OnPropertyChanged(nameof(Items));
                DoFilter();
            }
        }

        #endregion
        
        private void DoFilter()
        {
            var itemType = (ItemType)_filterItemTypeIndex;
            Items.Clear();
            Items.AddRange(itemSource.Where(v => v.Type == itemType));
        }
        public override async UniTask Initialize(Memory<object> args)
        {
            for (int i = 0; i < 3; i++)
            {
                itemSource.Add(new RankingRewardItem {  RankingRewardModal = this, Type = ItemType.MAIN, Name = $"Main quest {i+1}"});
            }
            
            for (int i = 0; i < 3; i++)
            {
                itemSource.Add(new RankingRewardItem {  RankingRewardModal = this, Type = ItemType.DAILY, Name = $"Daily quest {i+1}"});
            }
            
            DoFilter();
        }
        
        [Binding]
        public void ClaimAll()
        {
            
        }
    }
}
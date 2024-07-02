using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityWeld.Binding;

namespace _Game.Features.Quest
{
    [Binding]
    public class QuestModal : ModalWithViewModel
    {
        
        private List<QuestItem> itemSource = new List<QuestItem>();
        
        #region Binding: Items
        [Binding] 
        public ObservableList<QuestItem> Items { get; set; } = new();
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
                itemSource.Add(new QuestItem {  QuestViewModal = this, Type = ItemType.MAIN, Name = $"Main quest {i+1}"});
            }
            
            for (int i = 0; i < 3; i++)
            {
                itemSource.Add(new QuestItem {  QuestViewModal = this, Type = ItemType.DAILY, Name = $"Daily quest {i+1}"});
            }
            
            DoFilter();
        }
        
        [Binding]
        public void ClaimAll()
        {
            
        }
    }
}
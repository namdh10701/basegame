using System;
using System.Collections.Generic;
using _Base.Scripts.EventSystem;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Screens;
using Random = UnityEngine.Random;

namespace _Game.Features.GamePause
{
    [Binding]
    public class GamePauseModal : ModalWithViewModel
    {

        private List<TempItem> itemSource = new List<TempItem>();

        #region Binding: Items
        [Binding]
        public ObservableList<TempItem> Items { get; set; } = new();
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
                OnPropertyChanged(nameof(Items));
            }
        }

        #endregion

        private void DoFilter()
        {
            Items.Clear();
            Items.AddRange(itemSource);
        }
        public override async UniTask Initialize(Memory<object> args)
        {
            for (int i = 0; i < 3; i++)
            {
                itemSource.Add(new TempItem { Name = $"Item {i + 1}", Quantity = Random.Range(100, 2000) });
            }
            DoFilter();
        }

        [Binding]
        public async void Close()
        {
            GlobalEvent<bool>.Send("TOGGLE_PAUSE", false);
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
        }


        [Binding]
        public async void Quit()
        {
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
            var options = new ScreenOptions("MainScreen", true);
            await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);
        }
    }
}
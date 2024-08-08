using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Base.Scripts.Utils;
using _Game.Features.Dialogs;
using _Game.Features.Gameplay;
using _Game.Features.Home;
using _Game.Features.WorldMap;
using _Game.Scripts.UI.Utils;
using Cysharp.Threading.Tasks;
using Online.Model;
using Unity.VisualScripting;
using UnityEngine;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Screens;

namespace _Game.Features.Ranking
{

    [Binding]
    public partial class BattleDefeatModal : AsyncModal<object, BattleDefeatModal.Params>
    {
        public class Params
        {
            public int revive;
        }
        bool _isRevive;
        [Binding]
        public bool IsRevive
        {
            get => _isRevive;
            set
            {
                if (Equals(_isRevive, value))
                {
                    return;
                }

                _isRevive = value;
                OnPropertyChanged(nameof(IsRevive));
            }
        }
        public WinLoseAnimation winLoseAnimation;
        protected override async UniTask InternalInitialize(Params prm)
        {
            winLoseAnimation.SetStateIsWin(false);
            IsRevive = (prm.revive == 1);
        }
        [Binding]
        public void BattleAgain()
        {
            DoClose();
            BattleManager.Instance.PlayAgain();
        }

        [Binding]
        public async void NavWorldMap()
        {
            DoClose();
            Debug.LogWarning("nav world");
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
            var options = new ScreenOptions("WorldMapScreen", true);
            await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);
        }

        [Binding]
        public async void NavToHome()
        {
            DoClose();
            Debug.LogWarning("nav home");
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
            var options = new ScreenOptions("MainScreen", true);
            await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);
        }

        public static async Task Show(Params prm)
        {
            await Show<BattleDefeatModal>(prm, new AsyncModalOptions(closeWhenClickOnBackdrop: false));
        }

        [Binding]
        public void Revive()
        {
            DoClose();
            BattleManager.Instance.Revive();
        }

    }
}
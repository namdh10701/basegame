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
    public partial class BattleDefeatModal : AsyncModal<object, Null>
    {
        public WinLoseAnimation winLoseAnimation;
        protected override async UniTask InternalInitialize(Null @null)
        {
            winLoseAnimation.SetStateIsWin(false);
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

        public static async Task Show()
        {
            await Show<BattleDefeatModal>();
        }

    }
}
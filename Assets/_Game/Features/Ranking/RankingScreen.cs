using System;
using System.Linq;
using _Game.Features.Battle;
using _Game.Features.FightNodeInfoPopup;
using _Game.Features.Quest;
using Cysharp.Threading.Tasks;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Screens;
using ZBase.UnityScreenNavigator.Core.Views;

namespace _Game.Features.Ranking
{
    [Binding]
    public class RankingScreen : ScreenWithViewModel
    {
        private string _stageId;
        public override async UniTask Initialize(Memory<object> args)
        {
            _stageId = args.ToArray().FirstOrDefault() as string;

            // TODO init
        }
        
        [Binding]
        public async void NavBack()
        {
            await ScreenContainer.Find(ContainerKey.Screens).PopAsync(true);
        }

        [Binding]
        public async void ShowNormalFightPopupInfo()
        {
            var options = new ViewOptions(nameof(FightNodeInfoModal));
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options, _stageId);
        }

        [Binding]
        public async void ShowBossFightPopupInfo()
        {
            var options = new ViewOptions(nameof(FightNodeInfoModal));
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
        }
        
        [Binding]
        public async void ShowQuestPopup()
        {
            var options = new ViewOptions(nameof(QuestModal));
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
        }
        
        [Binding]
        public async void NavToWorldMap()
        {
            // var options = new ViewOptions(nameof(WorldMapScreen));
            // await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);
            
            await ScreenContainer.Find(ContainerKey.Screens).PopAsync(true);
        }
        
        [Binding]
        public async void NavToHome()
        {
            // var options = new ViewOptions(nameof(MainScreen));
            await ScreenContainer.Find(ContainerKey.Screens).PopAsync(false);
            await ScreenContainer.Find(ContainerKey.Screens).PopAsync(true);
        }

        [Binding]
        public async void NavBattleScene()
        {
            var options = new ViewOptions(nameof(BattleScreen));
            await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);
        }
    }
}

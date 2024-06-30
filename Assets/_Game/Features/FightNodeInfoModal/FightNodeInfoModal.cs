using System;
using _Game.Features.FightNodeInfo;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Sheets;
using ZBase.UnityScreenNavigator.Core.Views;

namespace _Game.Features.FightNodeInfoPopup
{
    public class FightNodeInfoModal : Modal
    {
        public FightNodeInfoViewModel ViewModel;
        
        public override async UniTask Initialize(Memory<object> args)
        {
            await ViewModel.Init();
        }

        public override async UniTask WillPushEnter(Memory<object> args)
        {
            
        }

        public override UniTask Cleanup(Memory<object> args)
        {
            return UniTask.CompletedTask;
        }

        // private void OnExpandButtonClicked()
        // {
        //     var options = new ViewOptions(ResourceKey.CharacterImageModalPrefab(), true,
        //         onLoaded: (modal, args) =>
        //         {
        //             var characterImageModal = (CharacterImageModal) modal;
        //             characterImageModal.Setup(_characterId, _selectedRank);
        //         });
        //
        //     ModalContainer.Find(ContainerKey.Modals).Push(options);
        // }
    }
}
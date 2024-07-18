using System;
using System.Collections.Generic;
using _Base.Scripts.Audio;
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
    public class GameSettingsModal : ModalWithViewModel
    {
        public override async UniTask Initialize(Memory<object> args)
        {

        }

        [Binding]
        public async void Close()
        {
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
        }

        void ToggleBgm(bool isOn)
        {
            AudioManager.Instance.IsBgmOn = isOn;
        }

        void ToggleGameSound(bool isOn)
        {
            AudioManager.Instance.IsSfxOn = isOn;
        }
    }
}
using System;
using _Base.Scripts.Audio;
using Cysharp.Threading.Tasks;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;

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
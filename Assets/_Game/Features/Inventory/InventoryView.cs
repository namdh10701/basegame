using System;
using Cysharp.Threading.Tasks;
using ZBase.UnityScreenNavigator.Core.Screens;

namespace _Game.Features.Inventory
{
    public class InventoryView : ZBase.UnityScreenNavigator.Core.Screens.Screen
    {

        public override async UniTask Initialize(Memory<object> args)
        {
            // _settingButton.onClick.RemoveAllListeners();
            // _shopButton.onClick.RemoveAllListeners();
            //
            // _settingButton.onClick.AddListener(OnSettingButtonClicked);
            // _shopButton.onClick.AddListener(OnShopButtonClicked);
            //
            // // Preload the "Shop" page prefab.
            // await ScreenContainer.Of(transform).PreloadAsync(ResourceKey.ShopScreenPrefab());
            // Simulate loading time.
            ScreenContainer.Of(transform);
            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }

        public override void DidPushEnter(Memory<object> args)
        {
            // ActivityContainer.Find(ContainerKey.Activities).Hide(ResourceKey.LoadingActivity());
        }

        // public override UniTask Cleanup(Memory<object> args)
        // {
        //     // _settingButton.onClick.RemoveListener(OnSettingButtonClicked);
        //     // _shopButton.onClick.RemoveListener(OnShopButtonClicked);
        //     // ScreenContainer.Of(transform).KeepInPool(ResourceKey.ShopScreenPrefab(), 0);
        //     return UniTask.CompletedTask;
        // }
    }
}

using System;
using _Base.Scripts.Audio;
using _Game.Features.Inventory;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ZBase.UnityScreenNavigator.Core.Sheets;

namespace _Game.Features.Home
{
    public class MainScreen : ZBase.UnityScreenNavigator.Core.Screens.Screen
    {
        private const int ItemGridSheetCount = 5;

        [SerializeField] private ToggleGroupInput _toggleGroupInput;
        [SerializeField] private SheetContainer _itemGridContainer;
        private readonly int[] _itemGridSheetIds = new int[ItemGridSheetCount];

        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _shopButton;

        // public override async UniTask Initialize(Memory<object> args)
        // {
        //     var itemGridContainer = _itemGridContainer;
        //     var itemGridSheetIds = _itemGridSheetIds;
        //     var itemGridButtons = _itemGridButtons;
        //
        //     for (var i = 0; i < ItemGridSheetCount; i++)
        //     {
        //         var index = i;
        //         var options = new SheetOptions(
        //             resourcePath: ResourceKey.ShopItemGridSheetPrefab(),
        //             // onLoaded: (id, sheet, args) => OnSheetLoaded(id, sheet, index)
        //         );
        //
        //         var sheetId = await itemGridContainer.RegisterAsync(options, args);
        //         var button = itemGridButtons[index];
        //
        //         button.onClick.RemoveAllListeners();
        //         button.onClick.AddListener(() => ShowSheet(sheetId).Forget());
        //     }
        //
        //     
        //     _toggleGroupInput.onValueChanged.AddListener(OnMainNavActiveIndexChanged);
        //
        //     await itemGridContainer.ShowAsync(itemGridSheetIds[0], false);
        // }
        //
        // private void OnSheetLoaded(int sheetId, Sheet sheet, int index)
        // {
        //     _itemGridSheetIds[index] = sheetId;
        //     var shopItemGrid = (ShopItemGridSheet)sheet;
        //     shopItemGrid.Setup(index, GetCharacterId(index));
        // }

        // void OnMainNavActiveIndexChanged(int index)
        // {
        //     ShowSheet(index).Forget();
        // }

        // private async UniTaskVoid ShowSheet(int sheetId)
        // {
        //     if (_itemGridContainer.IsInTransition)
        //     {
        //         return;
        //     }
        //
        //     if (_itemGridContainer.ActiveSheetId == sheetId)
        //     {
        //         // This sheet is already displayed.
        //         return;
        //     }
        //
        //     await _itemGridContainer.ShowAsync(sheetId, true);
        // }

        // // Called just after this screen is loaded.
        // public override UniTask Initialize(Memory<object> args)
        // {
        //     Debug.Log("XXX: Initialize");
        //     return UniTask.CompletedTask;
        // }
        //
        // // Called just before this screen is displayed by the Push transition.
        // public override UniTask WillPushEnter(Memory<object> args)
        // {
        //     Debug.Log("XXX: WillPushEnter");
        //     return UniTask.CompletedTask;
        // }
        //
        // // Called just after this screen is displayed by the Push transition.
        // public override void DidPushEnter(Memory<object> args)
        // {
        //     Debug.Log("XXX: DidPushEnter");
        // }
        //
        // // Called just before this screen is hidden by the Push transition.
        // public override UniTask WillPushExit(Memory<object> args)
        // {
        //     Debug.Log("XXX: WillPushExit");
        //     return UniTask.CompletedTask;
        // }
        //
        // // Called just after this screen is hidden by the Push transition.
        // public override void DidPushExit(Memory<object> args)
        // {
        //     Debug.Log("XXX: DidPushExit");
        // }
        //
        // // Called just before this screen is displayed by the Pop transition.
        // public override UniTask WillPopEnter(Memory<object> args)
        // {
        //     Debug.Log("XXX: WillPopEnter");
        //     return UniTask.CompletedTask;
        // }
        //
        // // Called just after this screen is displayed by the Pop transition.
        // public override void DidPopEnter(Memory<object> args)
        // {
        //     Debug.Log("XXX: DidPopEnter");
        // }
        //
        // // Called just before this screen is hidden by the Pop transition.
        // public override UniTask WillPopExit(Memory<object> args)
        // {
        //     Debug.Log("XXX: WillPopExit");
        //     return UniTask.CompletedTask;
        // }
        //
        // // Called just after this screen is hidden by the Pop transition.
        // public override void DidPopExit(Memory<object> args)
        // {
        //     Debug.Log("XXX: DidPopExit");
        //
        // }

        public override UniTask Initialize(Memory<object> args)
        {
            return base.Initialize(args);
        }

        public override async UniTask WillPushExit(Memory<object> args)
        {
            await SceneManager.UnloadSceneAsync("HaborScene");
            await base.WillPushExit(args);
        }
        public override async UniTask WillPushEnter(Memory<object> args)
        {
            AudioManager.Instance.PlayBgmHome();
            await SceneManager.LoadSceneAsync("HaborScene", LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
            await base.WillPushEnter(args);
        }


        public override async UniTask WillPopEnter(Memory<object> args)
        {
            AudioManager.Instance.PlayBgmHome();
            await SceneManager.LoadSceneAsync("HaborScene", LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
            await UniTask.CompletedTask;
        }

        // Called just after this screen is displayed by the Pop transition.
        public override void DidPopEnter(Memory<object> args)
        {
            Debug.Log("XXX: DidPopEnter");
        }
    }
}

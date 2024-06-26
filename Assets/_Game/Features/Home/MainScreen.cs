using System;
using _Game.Features.Inventory;
using Cysharp.Threading.Tasks;
using UnityEngine;
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
        
        // Return from other screen
        public override UniTask WillPopEnter(Memory<object> args)
        {
            Debug.Log("XXX: WillPopEnter");
            return UniTask.CompletedTask;
        }
        
        // Called just after this screen is displayed by the Pop transition.
        public override void DidPopEnter(Memory<object> args)
        {
            Debug.Log("XXX: DidPopEnter");
        }
    }
}

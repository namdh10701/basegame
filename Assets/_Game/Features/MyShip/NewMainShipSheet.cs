using System;
using _Base.Scripts.UI;
using _Game.Features.MyShipScreen;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.MyShip
{

    [Binding]
    public class NewMainShipSheet : SheetWithViewModel
    {
        public class InputData<T>
        {
            public T Value { get; }

            protected InputData(T data)
            {
                Value = data;
            }
        }
        GridManager _gridManager;
        GameObject _ship;

        public override UniTask Initialize(Memory<object> args)
        {
            // Initialize(_shipsConfig.currentShipId);
            return UniTask.CompletedTask;
        }


        [Binding]
        public async void NavToShipEditSheet()
        {
            await MyShipScreen.MyShipScreen.Instance.ShowSheet(Sheets.NewShipEditSheet);
        }
    }
}
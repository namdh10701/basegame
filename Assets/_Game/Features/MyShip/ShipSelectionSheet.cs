using System;
using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.UI;
using _Game.Features.Home;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;

namespace _Game.Features.MyShip
{
    [Binding]
    public class ShipSelectionSheet : SheetWithViewModel
    {

        [SerializeField] Button _btnNext;
        [SerializeField] Button _btnPrevious;
        [SerializeField] Button _btnEquip;
        [SerializeField] TextMeshProUGUI _page;

        [SerializeField] List<GameObject> _ships;
        [SerializeField] Transform _parentShip;
        [SerializeField] GameObject _iconApprove;
        public ShipSelectionInfoConfig ShipSelectionInfoConfig;
        GameObject _ship;

        public string ShipID;

        int _currentIndex = 0;

        public override UniTask Initialize(Memory<object> args)
        {
            _btnNext.onClick.AddListener(OnNextClick);
            _btnPrevious.onClick.AddListener(OnPreviousClick);
            _btnEquip.onClick.AddListener(OnEquip);

            return UniTask.CompletedTask;
        }

        void LoadShip(int shipIndex)
        {
            if (_ship != null)
                Destroy(_ship);

            _ship = Instantiate<GameObject>(_ships[shipIndex]);
            _ship.transform.SetParent(_parentShip);
            _ship.transform.localScale = Vector3.one;
            _ship.transform.localPosition = Vector3.zero;
            _iconApprove.SetActive(ShipSelectionInfoConfig.shipSelectionInfo[_currentIndex].isApprove);
            _page.text = $"{_currentIndex + 1}/{_ships.Count}";
            ShipID = ShipSelectionInfoConfig.shipSelectionInfo[_currentIndex].id;

        }

        private void OnPreviousClick()
        {
            _currentIndex--;
            if (0 <= _currentIndex && _currentIndex < _ships.Count)
            {
                LoadShip(_currentIndex);
            }
        }

        private void OnNextClick()
        {
            _currentIndex++;
            if (0 <= _currentIndex && _currentIndex < _ships.Count)
            {
                LoadShip(_currentIndex);
            }
        }

        public void OnEquip()
        {
            ShipSelectionInfoConfig.shipSelectionInfo[_currentIndex].isApprove = true;
            _iconApprove.SetActive(true);
        }


        public override UniTask Cleanup(Memory<object> args)
        {
            _btnNext.onClick.RemoveListener(OnNextClick);
            _btnPrevious.onClick.RemoveListener(OnPreviousClick);
            return UniTask.CompletedTask;
        }

        public class ShipSelectionSheetOutputData : MainShipSheet.InputData<string>
        {
            public ShipSelectionSheetOutputData(string data) : base(data)
            {
            }
        }

        public override UniTask WillEnter(Memory<object> args)
        {
            ShipID = args.ToArray().FirstOrDefault() as string;
            char character = ShipID[3];
            _currentIndex = int.Parse(character.ToString()) - 1;
            LoadShip(_currentIndex);
            return UniTask.CompletedTask;
        }

        [Binding]
        public async void NavBack()
        {
            var selectedShipId = GetSelectedShipId();

            var output = new ShipSelectionSheetOutputData(selectedShipId);
            await MyShipScreen.Instance.ShowSheet(Sheets.MainShipSheet, output);
        }

        private string GetSelectedShipId()
        {
            return ShipID;
        }
    }
}
using System;
using System.Collections.Generic;
using _Base.Scripts.UI;
using _Game.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Screens;
using ZBase.UnityScreenNavigator.Core.Views;
using Screen = ZBase.UnityScreenNavigator.Core.Screens.Screen;

namespace _Game.Features.Home
{
    [Binding]
    public class ShipSelectionScreen : Screen
    {
        [SerializeField] Button _btnNext;
        [SerializeField] Button _btnPrevious;
        [SerializeField] TextMeshProUGUI _page;

        [SerializeField] List<GameObject> _ships;
        [SerializeField] Transform _parentShip;
        GameObject _ship;

        int _currentIndex = 0;

        void Awake()
        {
            LoadShip(_currentIndex);

        }

        void LoadShip(int shipIndex)
        {
            if (_ship != null)
                Destroy(_ship);

            _ship = Instantiate<GameObject>(_ships[shipIndex]);
            _ship.transform.SetParent(_parentShip);
            _page.text = $"{_currentIndex + 1}/{_ships.Count}";
        }

        void OnEnable()
        {
            _btnNext.onClick.AddListener(OnNextClick);
            _btnPrevious.onClick.AddListener(OnPreviousClick);
        }

        private void OnPreviousClick()
        {
            _currentIndex++;
            if (0 < _currentIndex && _currentIndex < _ships.Count)
            {
                LoadShip(_currentIndex);
            }
        }

        private void OnNextClick()
        {
            _currentIndex--;
            if (0 < _currentIndex && _currentIndex < _ships.Count)
            {
                LoadShip(_currentIndex);
            }
        }

        void OnDisable()
        {
            _btnNext.onClick.RemoveListener(OnNextClick);
            _btnPrevious.onClick.RemoveListener(OnPreviousClick);
        }

    }
}
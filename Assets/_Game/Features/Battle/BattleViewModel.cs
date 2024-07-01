using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Base.Scripts.EventSystem;
using _Game.Features.Inventory;
using _Game.Scripts.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Screens;

namespace _Game.Features.Battle
{
    [Binding]
    public class BattleViewModel : RootViewModel
    {
        #region Binding Prop: HP

        /// <summary>
        /// HP
        /// </summary>
        [Binding]
        public float HP
        {
            get => _hp;
            set
            {
                /*if (Equals(_hp, value))
                {
                    return;
                }*/
                _hp = value;
                OnPropertyChanged(nameof(HP));
                OnPropertyChanged(nameof(HPText));
            }
        }

        private float _hp = -1;

        #endregion

        #region Binding Prop: MaxHP

        /// <summary>
        /// MaxHP
        /// </summary>
        [Binding]
        public float MaxHP
        {
            get => _maxHP;
            set
            {
                /*if (Equals(_maxHP, value))
                {
                    return;
                }*/

                _maxHP = value;
                OnPropertyChanged(nameof(MaxHP));
                OnPropertyChanged(nameof(HPText));
            }
        }

        private float _maxHP = -1;

        #endregion

        #region Binding Prop: MP

        /// <summary>
        /// MP
        /// </summary>
        [Binding]
        public float MP
        {
            get => _mp;
            set
            {
                /*if (Equals(_mp, value))
                {
                    return;
                }*/

                _mp = value;
                OnPropertyChanged(nameof(MP));
                OnPropertyChanged(nameof(MPText));
            }
        }

        private float _mp = -1;

        #endregion

        #region Binding Prop: MaxMP

        /// <summary>
        /// MaxMP
        /// </summary>
        [Binding]
        public float MaxMP
        {
            get => _maxMP;
            set
            {
                /*if (Equals(_maxMP, value))
                {
                    return;
                }*/

                _maxMP = value;
                OnPropertyChanged(nameof(MaxMP));
                OnPropertyChanged(nameof(MPText));
            }
        }

        private float _maxMP = -1;

        #endregion

        #region Binding Prop: Fever

        /// <summary>
        /// Fever
        /// </summary>
        [Binding]
        public float Fever
        {
            get => _fever;
            set
            {
                if (Equals(_fever, value))
                {
                    return;
                }

                _fever = value;
                OnPropertyChanged(nameof(Fever));
                OnPropertyChanged(nameof(FeverText));
            }
        }

        private float _fever;

        #endregion

        #region Binding Prop: MaxFever

        /// <summary>
        /// MaxFever
        /// </summary>
        [Binding]
        public float MaxFever
        {
            get => _maxFever;
            set
            {
                if (Equals(_maxFever, value))
                {
                    return;
                }

                _maxFever = value;
                OnPropertyChanged(nameof(MaxFever));
                OnPropertyChanged(nameof(FeverText));
            }
        }

        private float _maxFever;

        #endregion

        #region Binding Prop: IsPause

        /// <summary>
        /// IsPause
        /// </summary>
        [Binding]
        public bool IsPause
        {
            get => _isPause;
            set
            {
                if (Equals(_isPause, value))
                {
                    return;
                }

                _isPause = value;
                OnPropertyChanged(nameof(IsPause));
                UpdateTimeScale();
            }
        }

        private bool _isPause;

        #endregion

        #region Binding Prop: SpeedUpRate

        /// <summary>
        /// SpeedUpRate
        /// </summary>
        [Binding]
        public float SpeedUpRate
        {
            get => _speedUpRate;
            set
            {
                if (Equals(_speedUpRate, value))
                {
                    return;
                }

                _speedUpRate = value;
                OnPropertyChanged(nameof(SpeedUpRate));
                OnPropertyChanged(nameof(SpeedUpRateText));
                
                UpdateTimeScale();
            }
        }

        private float _speedUpRate = 1f;

        #endregion

        [Binding] public string SpeedUpRateText => $"X{SpeedUpRate}";
        [Binding] public string HPText => $"{HP}/{MaxHP}";
        [Binding] public string MPText => $"{MP}/{MaxMP}";
        [Binding] public string FeverText => $"{Fever}/{MaxFever}";

        private void UpdateTimeScale()
        {
            var timeScale = _speedUpRate;

            if (IsPause)
            {
                timeScale = 0;
            }

            Time.timeScale = timeScale;
        }
        
        [Binding]
        public async void PauseGame()
        {
            IsPause = !IsPause;

            if (!IsPause) return;
            
            var options = new ModalOptions("GamePauseModal");
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
            GlobalEvent.Send("GAMEPLAY_PAUSED");
        }

        private readonly int _minSpeedUpRate = 1;
        private readonly int _maxSpeedUpRate = 3;
        private readonly int _speedUpRateStep = 1;
        
        [Binding]
        public void SpeedUpGame()
        {
            var rate = _speedUpRate + _speedUpRateStep;
            if (rate > _maxSpeedUpRate)
            {
                rate = _minSpeedUpRate;
            }

            SpeedUpRate = rate;
        }
        
        [Binding]
        public async void NavToBattle()
        {
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
            
            var options = new ScreenOptions("BattleLoadingScreen", true, stack: false);
            await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);
            await UniTask.Delay(500);
        
            options = new ScreenOptions("BattleScreen", true, stack: false);
            await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);

        }
        
        [Binding]
        public async void NavToMyShip()
        {
            var options = new ScreenOptions("MyShipScreen", true);
            await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);
        }
    }
}
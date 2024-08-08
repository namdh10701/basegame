using _Base.Scripts.EventSystem;
using _Game.Features.Gameplay;
using _Game.Scripts;
using _Game.Scripts.Gameplay;
using _Game.Scripts.UI;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Screens;

namespace _Game.Features.Battle
{
    public interface IBattleView
    {
        public void Hide();
    }

    [Binding]
    public class BattleViewModel : RootViewModel
    {
        public FeverView FeverView;
        public CanvasGroup BtnCanvasGroup;
        public void Hide()
        {
            BtnCanvasGroup.alpha = 0;
            BtnCanvasGroup.blocksRaycasts = false;
        }
        public void CleanUp()
        {
            FeverView.ClearState();
        }

        #region Binding Prop: Shield

        /// <summary>
        /// HP
        /// </summary>
        [Binding]
        public float Shield
        {
            get => _shield;
            set
            {
                if (Equals(_shield, value))
                {
                    return;
                }
                _shield = value;
                OnPropertyChanged(nameof(Shield));
                OnPropertyChanged(nameof(HPText));
            }
        }

        private float _shield = -1;
        private float _maxShield = -1;

        #endregion

        #region Binding Prop: MaxHP

        /// <summary>
        /// MaxHP
        /// </summary>
        [Binding]
        public float MaxShield
        {
            get => _maxShield;
            set
            {
                if (Equals(_maxShield, value))
                {
                    return;
                }

                _maxShield = value;
                OnPropertyChanged(nameof(MaxShield));
            }
        }


        #endregion


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
                if (Equals(_hp, value))
                {
                    return;
                }
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
                if (Equals(_maxHP, value))
                {
                    return;
                }

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
                if (Equals(_mp, value))
                {
                    return;
                }

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
            }
        }

        private float _speedUpRate = 1f;

        #endregion

        [Binding] public string SpeedUpRateText => $"X{SpeedUpRate}";
        [Binding] public string HPText => $"{Math.Round(HP + Shield)}/{MaxHP}";
        [Binding] public string MPText => $"{Math.Round(MP)}/{MaxMP}";
        //[Binding] public string ShieldText => $"{Math.Round(Shield)}/{MaxShield}";

        [Binding]
        public async void PauseGame()
        {
            IsPause = true;
            GlobalEvent<bool>.Send("TOGGLE_PAUSE", true);
            var options = new ModalOptions("GamePauseModal", .5f, false);
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);

        }


        [Binding]
        public void SpeedUpGame()
        {
            BattleManager.Instance.SpeedUp();
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


        public void Init(BattleManager battleManager)
        {
            BtnCanvasGroup.alpha = 1;
            BtnCanvasGroup.blocksRaycasts = true;
            battleManager.TimeScaleChanged += UpdateCurrentSpeed;
            battleManager.OnEnded += Hide;
            Ship ship = battleManager.EntityManager.Ship;
            ShipStats shipStats = ship.Stats as ShipStats;

            MaxMP = shipStats.ManaPoint.Value;
            MP = shipStats.ManaPoint.Value;

            MaxHP = shipStats.HealthPoint.Value;
            HP = shipStats.HealthPoint.Value;

            Shield = ship.Shield.Value;
            MaxShield = MaxHP;
            ship.Shield.OnValueChanged += Shield_OnValueChanged;
            shipStats.HealthPoint.OnValueChanged += HealthPoint_OnValueChanged;
            shipStats.ManaPoint.OnValueChanged += ManaPoint_OnValueChanged;
            FeverView.Init(ship.FeverModel);
        }

        private void Shield_OnValueChanged(_Base.Scripts.RPG.Stats.RangedStat obj)
        {

            Debug.Log(Shield);
            Shield = obj.Value;
            //MaxShield = obj.MaxValue;
        }

        private void UpdateCurrentSpeed(float obj)
        {
            SpeedUpRate = obj;
        }

        private void ManaPoint_OnValueChanged(_Base.Scripts.RPG.Stats.RangedStat obj)
        {
            MP = obj.Value;
            MaxMP = obj.MaxValue;
        }
        private void HealthPoint_OnValueChanged(_Base.Scripts.RPG.Stats.RangedStat obj)
        {
            HP = obj.Value;
            MaxHP = obj.MaxValue;
        }

    }
}
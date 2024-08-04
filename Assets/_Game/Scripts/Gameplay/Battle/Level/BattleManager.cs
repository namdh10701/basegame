using _Base.Scripts.Audio;
using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Features;
using _Game.Features.Battle;
using _Game.Scripts;
using _Game.Scripts.Battle;
using _Game.Scripts.Entities;
using _Game.Scripts.SaveLoad;
using Map;
using System;
using System.Collections;
using UnityEngine;
using ZBase.UnityScreenNavigator.Core.Modals;

namespace _Game.Features.Gameplay
{
    public abstract class BattleManager : MonoBehaviour
    {
        #region Singleton
        private static BattleManager instance;
        public static BattleManager Instance => instance;
        private void Awake()
        {
            instance = this;
            GlobalEvent<bool>.Register("TOGGLE_PAUSE", TogglePause);
            Initialize();
        }
        #endregion

        public static string SelectedShipId;

        public Transform shipStartPos;
        public EntityManager EntityManager;
        public LevelStartSequence LevelStartSequence;
        public EnemyWaveManager EnemyManager;
        public BattleInputManager BattleInputManager;

        // require for enemy attacks
        public GridAttackHandler GridAttackHandler;
        public GridPicker GridPicker;
        public WinCondition winCondition;

        public Action<float> TimeScaleChanged;

        public abstract void Initialize();
        public abstract void HandleWinData();
        public abstract void ShowWinUIAsync();
        public abstract IEnumerator LevelEntryCoroutine();



        private void OnDestroy()
        {
            Time.timeScale = 1;
            GlobalEvent<bool>.Unregister("TOGGLE_PAUSE", TogglePause);
            currentRate = 1;
        }

        private readonly int _minSpeedUpRate = 1;
        private readonly int _maxSpeedUpRate = 3;
        private readonly int _speedUpRateStep = 1;


        private float currentRate = 1;
        public void SpeedUp()
        {
            currentRate += _speedUpRateStep;

            if (currentRate > _maxSpeedUpRate)
            {
                currentRate = _minSpeedUpRate;
            }

            UpdateTimeScale();
        }
        public void TogglePause(bool isPause)
        {
            if (isPause)
            {
                BattleInputManager.gameObject.SetActive(false);
                Time.timeScale = 0;
            }
            else
            {
                BattleInputManager.gameObject.SetActive(true);
                Time.timeScale = currentRate;
            }
        }

        public void UpdateTimeScale()
        {
            Time.timeScale = currentRate;
            TimeScaleChanged?.Invoke(currentRate);
        }

        public void Win()
        {
            HandleWinData();
            ShowWinUIAsync();
        }
    }
}
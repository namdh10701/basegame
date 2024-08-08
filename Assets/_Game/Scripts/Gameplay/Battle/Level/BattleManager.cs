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
using _Game.Scripts.Utils;
using Map;
using System;
using System.Collections;
using UnityEngine;
using ZBase.UnityScreenNavigator.Core.Modals;

namespace _Game.Features.Gameplay
{
    public enum BatleState
    {
        Intro, Playing, End
    }
    public abstract class BattleManager : MonoBehaviour
    {
        #region Singleton
        private static BattleManager instance;
        public static BattleManager Instance => instance;
        private void Awake()
        {
            instance = this;
            GlobalEvent<bool>.Register("TOGGLE_PAUSE", TogglePause);
            //Initialize();
        }
        #endregion

        public static string SelectedShipId;

        public Transform shipStartPos;
        public EntityManager EntityManager;
        public LevelStartSequence LevelStartSequence;
        public EnemyWaveManager EnemyManager;
        public BattleInputManager BattleInputManager;

        public bool IsWin;
        public bool IsEnded;
        public GridAttackHandler GridAttackHandler;
        public GridPicker GridPicker;
        public WinCondition winCondition;
        public Action<float> TimeScaleChanged;
        public Action OnEnded;
        public abstract void Initialize();
        public abstract void HandleLoseData();
        public abstract void HandleWinData();
        public abstract void ShowWinUIAsync();
        public abstract void ShowLoseUIAsync(int revive);
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

        protected virtual void StopGame()
        {
            IsEnded = true;
            OnEnded?.Invoke();
            Time.timeScale = 1;
            TimeScaleChanged?.Invoke(1);
            winCondition.StopCheck();
            BattleInputManager.gameObject.SetActive(false);
            EnemyManager.StopSpawn();
            EntityManager.Cleanup();

        }
        public void Win()
        {
            if (IsEnded)
            {
                return;
            }

            StopGame();
            IsWin = true;
            HandleWinData();
            ShowWinUIAsync();
        }

        public void Lose(int revive)
        {
            if (IsEnded)
            {
                return;
            }

            StopGame();
            IsWin = false;
            HandleLoseData();
            StartCoroutine(LoseCoroutine(revive));
        }


        public CameraShake cameraShake;
        public ParticleSystem explosion;
        IEnumerator LoseCoroutine(int revive)
        {
            float elapsedTime = 0;
            float duration = 4;
            while (elapsedTime < duration)
            {
                for (int i = 0; i < 10; i++)
                {
                    float interval = UnityEngine.Random.Range(.5f, 1);
                    elapsedTime += interval;
                    yield return new WaitForSeconds(interval);
                    Vector2 pos = EntityManager.Ship.ShipBound.SamplePoint();
                    ParticleSystem particle = Instantiate(explosion, pos, Quaternion.identity, null);
                    particle.gameObject.SetActive(true);
                    cameraShake.Shake(.15f, new Vector3(.15f, .15f, .15f));
                }
            }
            ShowLoseUIAsync(revive);
        }

        public abstract void PlayAgain();
        public virtual void Revive()
        {
            IsEnded = false;
            BattleInputManager.gameObject.SetActive(true);
        }
    }
}
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
    public class BattleManager : MonoBehaviour
    {
        private static BattleManager instance;
        public static BattleManager Instance => instance;
        private void Awake()
        {
            instance = this;
            //Initialize(null);
        }

        public static string SelectedShipId = "0003";
        public static string StageId;

        public Transform shipStartPos;
        public EntityManager EntityManager;
        public LevelStartSequence LevelStartSequence;
        public EnemyManager EnemyManager;
        public BattleInputManager BattleInputManager;
        public GridAttackHandler GridAttackHandler;
        public GridPicker GridPicker;
        public FeverSpeedFx FeverSpeedFx;

        public BattleViewModel BattleViewModel;
        public void Initialize(BattleViewModel battleViewModel)
        {
            EntityManager.OnEnter();

            this.BattleViewModel = battleViewModel;
            BattleInputManager.gameObject.SetActive(false);
            EntityManager.SpawnShip(SaveSystem.GameSave.ShipSetupSaveData.CurrentShipId, shipStartPos.position);
            EntityManager.Ship.Initialize(BattleViewModel);
            LevelStartSequence.shipSpeed = EntityManager.Ship.ShipSpeed;
            GridAttackHandler.ship = EntityManager.Ship;
            EntityManager.Ship.BattleViewModel = battleViewModel;
            GridPicker.ShipGrid = EntityManager.Ship.ShipSetup;

            BattleInputManager.ShipHUD = EntityManager.Ship.HUD;
            BattleInputManager.Ship = EntityManager.Ship;

            StartCoroutine(LevelEntryCoroutine());
            GlobalEvent.Register("UseFullFever", UseFullFever);
            GlobalEvent<bool>.Register("TOGGLE_PAUSE", TogglePause);
        }

        IEnumerator LevelEntryCoroutine()
        {
            AudioManager.Instance.PlayBgmGameplay();
            yield return LevelStartSequence.Play();
            EnemyManager.StartLevel();
            MapPlayerTracker.Instance.OnGameStart();
            BattleInputManager.gameObject.SetActive(true);
            EntityManager.Ship.ShipSetup.CrewController.ActivateCrews();
            StartCoroutine(CheckLevelDone());
        }

        IEnumerator CheckLevelDone()
        {
            while (true)
            {
                yield return new WaitForSeconds(5);
                if (EnemyManager.IsLevelDone && EntityManager.aliveEnemies.Count == 0)
                {
                    Win();
                    MapPlayerTracker.Instance.OnGamePassed();
                    yield break;
                }
                if (EnemyManager.IsLevelDone)
                {
                    bool ended = true;
                    foreach (EnemyModel a in EntityManager.aliveEnemies)
                    {
                        if (a != null)
                        {
                            EnemyStats stats = (EnemyStats)a.Stats;
                            if (stats.HealthPoint.Value > 0)
                            {
                                ended = false;
                            }

                        }
                    }
                    if (ended)
                    {
                        Win();
                        MapPlayerTracker.Instance.OnGamePassed();
                        yield break;
                    }
                }
            }
        }

        async void Win()
        {
            //CleanUp();
            PlayerPrefs.SetFloat("fever", EntityManager.Ship.stats.Fever.Value);
            var options = new ModalOptions("BattleVictory1Screen", true, loadAsync: false);

            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
        }

        private void OnDestroy()
        {
            CleanUp();
        }
        public void CleanUp()
        {
            GlobalEvent.Unregister("UseFullFever", UseFullFever);
            Time.timeScale = 1;
            GlobalEvent<bool>.Unregister("TOGGLE_PAUSE", TogglePause);
            currentRate = 1;
            BattleViewModel.SpeedUpRate = currentRate;
            EntityManager.CleanUp();
            EnemyManager.CleanUp();
            EnemyModel[] a = GameObject.FindObjectsByType<EnemyModel>(FindObjectsSortMode.None);
            foreach (EnemyModel enemy in a)
            {
                GameObject.Destroy(enemy.gameObject);
            }
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
            BattleViewModel.SpeedUpRate = currentRate;
        }
        public void TogglePause(bool isPause)
        {
            Debug.Log("TOGGLE PAUSE " + isPause);
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
        }

        internal void UseFever(Cannon cannon)
        {
            EntityManager.Ship.UseFever(cannon);
        }

        void UseFullFever()
        {
            EntityManager.Ship.UseFullFever();
        }
    }
}
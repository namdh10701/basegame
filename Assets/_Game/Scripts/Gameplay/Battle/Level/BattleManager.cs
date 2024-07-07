using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Features;
using _Game.Features.Battle;
using _Game.Scripts.Battle;
using _Game.Scripts.Entities;
using Map;
using System.Collections;
using UnityEngine;
using ZBase.UnityScreenNavigator.Core.Modals;

namespace _Game.Scripts.Gameplay
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


        public BattleViewModel BattleViewModel;
        public void Initialize(BattleViewModel battleViewModel)
        {
            this.BattleViewModel = battleViewModel;
            BattleInputManager.gameObject.SetActive(false);
            EntityManager.SpawnShip(BattleManager.SelectedShipId, shipStartPos.position);
            LevelStartSequence.shipSpeed = EntityManager.Ship.ShipSpeed;
            BattleInputManager.shipSetup = EntityManager.Ship.ShipSetup;
            GridAttackHandler.ship = EntityManager.Ship;
            GridPicker.ShipGrid = EntityManager.Ship.ShipSetup;
            StartCoroutine(LevelEntryCoroutine());

            GlobalEvent<bool>.Register("TOGGLE_PAUSE", TogglePause);
        }

        IEnumerator LevelEntryCoroutine()
        {
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
                if (EnemyManager.IsLevelDone && EntityManager.aliveEntities.Count == 0)
                {
                    Win();
                    MapPlayerTracker.Instance.OnGamePassed();
                    yield break;
                }
                if (EnemyManager.IsLevelDone)
                {
                    bool ended = true;
                    foreach (IAliveStats a in EntityManager.aliveEntities)
                    {
                        if (a != null)
                        {
                            if (a is EnemyStats s)
                            {
                                if (s.HealthPoint.Value > 0)
                                {
                                    ended = false;
                                }
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
            var options = new ModalOptions("BattleVictory1Screen", true, loadAsync: false);

            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
        }


        public void CleanUp()
        {
            Time.timeScale = 1;
            GlobalEvent<bool>.Unregister("TOGGLE_PAUSE", TogglePause);
            currentRate = 1; 
            BattleViewModel.SpeedUpRate = currentRate;
            EntityManager.CleanUp();
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

    }
}
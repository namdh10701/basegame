using _Base.Scripts.EventSystem;
using _Game.Features;
using _Game.Features.Battle;
using _Game.Scripts.Battle;
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
        }

        public static string SelectedShipId = "0001";
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
                    yield break;
                }
                else
                {
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
            EntityManager.CleanUp();
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
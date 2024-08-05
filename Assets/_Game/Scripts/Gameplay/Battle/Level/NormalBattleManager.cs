using _Base.Scripts.Audio;
using _Game.Features.Battle;
using _Game.Scripts.Battle;
using _Game.Scripts.SaveLoad;
using Map;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using ZBase.UnityScreenNavigator.Core.Modals;
namespace _Game.Features.Gameplay
{
    public class NormalBattleManager : BattleManager
    {
        public override void Initialize()
        {
            BattleInputManager.gameObject.SetActive(false);
            EntityManager.SpawnShip(SaveSystem.GameSave.ShipSetupSaveData.CurrentShip.ItemId, shipStartPos.position);
            if (EnemyWaveManager.floorId == "1")
            {
                EntityManager.Ship.stats.Fever.StatValue.BaseValue = 0;
            }
            else
            {
                float fever = PlayerPrefs.GetFloat("fever", 0);
                EntityManager.Ship.stats.Fever.StatValue.BaseValue = fever;
            }
            LevelStartSequence.shipSpeed = EntityManager.Ship.ShipSpeed;
            GridAttackHandler.ship = EntityManager.Ship;
            BattleInputManager.reloadCannonController = EntityManager.Ship.reloadCannonController;
            GridPicker.ShipGrid = EntityManager.Ship.ShipSetup;
            BattleInputManager.CrewJobData = EntityManager.Ship.CrewJobData;
            BattleViewModel batleView = FindAnyObjectByType<BattleViewModel>();
            if (batleView != null)
                batleView.Init(this);
            StartCoroutine(LevelEntryCoroutine());
        }
        public override void HandleLoseData()
        {
        }
        public override void HandleWinData()
        {
            MapPlayerTracker.Instance.OnGamePassed();
            PlayerPrefs.SetFloat("fever", EntityManager.Ship.stats.Fever.Value);
        }

        public override IEnumerator LevelEntryCoroutine()
        {
            yield return LevelStartSequence.Play();
            EnemyManager.StartLevel();
            MapPlayerTracker.Instance.OnGameStart();
            BattleInputManager.gameObject.SetActive(true);
            EntityManager.Ship.ShipSetup.CrewController.ActivateCrews();
            winCondition.StartChecking();
        }
        public override async void ShowWinUIAsync()
        {
            var options = new ModalOptions("BattleVictory1Screen", true, loadAsync: false);
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
        }

        public override async void ShowLoseUIAsync()
        {
            var options = new ModalOptions("BattleLose1Screen", true, loadAsync: false);
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
        }

    }
}
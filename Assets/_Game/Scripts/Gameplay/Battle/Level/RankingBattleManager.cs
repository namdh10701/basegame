using _Base.Scripts.Audio;
using _Game.Features.Battle;
using _Game.Scripts.Battle;
using _Game.Scripts.SaveLoad;
using Map;
using System.Collections;
using UnityEngine;
using ZBase.UnityScreenNavigator.Core.Modals;

namespace _Game.Features.Gameplay
{
    public class RankingBattleManager : BattleManager
    {
        public GiantOctopus giantOctopus;
        public Timer timer;
        public override void Initialize()
        {
            BattleInputManager.gameObject.SetActive(false);
            EntityManager.SpawnShip(SaveSystem.GameSave.ShipSetupSaveData.CurrentShip.ItemId, shipStartPos.position);
            LevelStartSequence.shipSpeed = EntityManager.Ship.ShipSpeed;
            GridAttackHandler.ship = EntityManager.Ship;
            GridPicker.ShipGrid = EntityManager.Ship.ShipSetup;
            RankingBattleViewModel batleView = FindAnyObjectByType<RankingBattleViewModel>();
            if (batleView != null)
                batleView.Init(this);
            StartCoroutine(LevelEntryCoroutine());
        }

        public override IEnumerator LevelEntryCoroutine()
        {
            yield return LevelStartSequence.Play();
            EnemyManager.StartLevel();
            BattleInputManager.gameObject.SetActive(true);
            giantOctopus.gameObject.SetActive(true);
            EntityManager.Ship.ShipSetup.CrewController.ActivateCrews();
            yield return new WaitForSeconds(4);
           
            timer.timeCap = 300;
            timer.StartTimer();
            winCondition.StartChecking();
        }

        public override void HandleWinData()
        {

        }

        public override async void ShowWinUIAsync()
        {
            var options = new ModalOptions("BattleVictory1Screen", true, loadAsync: false);
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
        }
    }
}
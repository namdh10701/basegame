using _Base.Scripts.Audio;
using _Game.Features.Battle;
using _Game.Features.Ranking;
using _Game.Scripts;
using _Game.Scripts.Battle;
using _Game.Scripts.SaveLoad;
using _Game.Scripts.UI;
using Map;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using ZBase.UnityScreenNavigator.Core.Modals;

namespace _Game.Features.Gameplay
{
    public class RankingBattleManager : BattleManager
    {
        public GiantOctopus giantOctopus;
        public Timer timer;
        RankingBattleViewModel batleView;
        public float DmgDeal
        {

            get
            {
                float dmgDealt;
                dmgDealt = giantOctopus.enemyStats.HealthPoint.MaxValue - giantOctopus.enemyStats.HealthPoint.Value;
                return dmgDealt;
            }
        }
        public override void Initialize()
        {
            BattleInputManager.gameObject.SetActive(false);
            EntityManager.SpawnShip(SaveSystem.GameSave.ShipSetupSaveData.CurrentShip.ItemId, shipStartPos.position);
            LevelStartSequence.shipSpeed = EntityManager.Ship.ShipSpeed;
            GridAttackHandler.ship = EntityManager.Ship;
            GridPicker.ShipGrid = EntityManager.Ship.ShipSetup;
            BattleInputManager.reloadCannonController = EntityManager.Ship.reloadCannonController;
            BattleInputManager.CrewJobData = EntityManager.Ship.CrewJobData;
            batleView = FindAnyObjectByType<RankingBattleViewModel>();
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
            yield return new WaitForSeconds(10);
            batleView.ShowTimer();
            winCondition.StartChecking();
        }

        public override void HandleWinData()
        {

        }
        public override void HandleLoseData()
        {
        }
        protected override void StopGame()
        {
            base.StopGame();
            if (giantOctopus.State != OctopusState.Dead)
            {
                giantOctopus.State = OctopusState.None;
            }
        }
        public override async void ShowWinUIAsync()
        {
            if (giantOctopus.State == OctopusState.Dead)
            {
                await Task.Delay((int)(6 * 1000));
            }
            RankingVictoryModal.Params p= new RankingVictoryModal.Params();
            p.Score = (int)DmgDeal;
            await RankingVictoryModal.Show(p);
        }

        public override async void ShowLoseUIAsync()
        {
            RankingVictoryModal.Params p = new RankingVictoryModal.Params();
            p.Score = (int)DmgDeal;
            await RankingVictoryModal.Show(p);
        }

    }
}
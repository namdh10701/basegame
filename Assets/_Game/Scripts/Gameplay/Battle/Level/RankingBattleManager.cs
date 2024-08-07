using _Game.Features.Battle;
using _Game.Features.Ranking;
using _Game.Scripts.SaveLoad;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Game.Features.Inventory;
using Online;
using Online.Model;
using UnityEngine;

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
            IsEnded = false;
            EnemyManager.Init();
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
            
            // Level: 'Level',
            // Exp: 'Exp',
            // Rank: 'Rank',
            // RankingTicketId: 'RankingTicketId',
            // LimitPackages: 'LimitPackages',
            // Gachas: 'Gachas'
            
            var resp = await PlayfabManager.Instance.Ranking.FinishRankBattleAsync((int)DmgDeal);
            // resp.Data["Level"];
            // resp.Items.Select(v => v.ItemId);

            var rewards = new List<RankReward>();
            
            if (resp.Data.TryGetValue("Exp", out var exp))
            {
                rewards.Add(new RankReward()
                {
                    ItemType = ItemType.MISC,
                    ItemId = MiscItemId.exp,
                    Amount = int.Parse(exp.ToString()),
                });
            }
            
            if (resp.VirtualCurrency.TryGetValue("Gold", out var gold))
            {
                rewards.Add(new RankReward()
                {
                    ItemType = ItemType.MISC,
                    ItemId = MiscItemId.gold,
                    Amount = gold,
                });
            }
            
            if (resp.VirtualCurrency.TryGetValue("Key", out var key))
            {
                rewards.Add(new RankReward()
                {
                    ItemType = ItemType.MISC,
                    ItemId = MiscItemId.key,
                    Amount = key,
                });
            }
            
            foreach (var item in resp.Items)
            {
                rewards.Add(new RankReward()
                {
                    ItemType = ItemType.MISC,
                    ItemId = "res_" + item.ItemId, 
                    Amount = 1,
                });
            }
            
            var p = new RankingVictoryModal.Params
            {
                Score = (int)DmgDeal,
                Rewards = rewards,
            };
            await RankingVictoryModal.Show(p);
        }

        public override async void ShowLoseUIAsync()
        {
            var resp = await PlayfabManager.Instance.Ranking.FinishRankBattleAsync((int)DmgDeal);
            var rewards = new List<RankReward>();
            
            if (resp.Data.TryGetValue("Exp", out var exp))
            {
                rewards.Add(new RankReward()
                {
                    ItemType = ItemType.MISC,
                    ItemId = MiscItemId.exp,
                    Amount = int.Parse(exp.ToString()),
                });
            }
            
            if (resp.VirtualCurrency.TryGetValue("Gold", out var gold))
            {
                rewards.Add(new RankReward()
                {
                    ItemType = ItemType.MISC,
                    ItemId = MiscItemId.gold,
                    Amount = gold,
                });
            }
            
            if (resp.VirtualCurrency.TryGetValue("Key", out var key))
            {
                rewards.Add(new RankReward()
                {
                    ItemType = ItemType.MISC,
                    ItemId = MiscItemId.key,
                    Amount = key,
                });
            }
            
            foreach (var item in resp.Items)
            {
                rewards.Add(new RankReward()
                {
                    ItemType = ItemType.MISC,
                    ItemId = "res_" + item.ItemId, 
                    Amount = 1,
                });
            }
            
            var p = new RankingVictoryModal.Params
            {
                Score = (int)DmgDeal,
                Rewards = rewards,
            };
            await RankingVictoryModal.Show(p);
        }

        public override void PlayAgain()
        {
            EntityManager.OnPlayAgain();
        }
    }
}
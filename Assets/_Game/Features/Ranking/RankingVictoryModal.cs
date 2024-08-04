using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Base.Scripts.Utils;
using _Game.Features.Dialogs;
using _Game.Features.Home;
using _Game.Scripts.UI.Utils;
using Cysharp.Threading.Tasks;
using Online.Model;
using Unity.VisualScripting;
using UnityWeld.Binding;

namespace _Game.Features.Ranking
{
    [Binding]
    public partial class RankingVictoryModal : AsyncModal<object, RankingVictoryModal.Params>
    {
        #region Binding Prop: Score

        /// <summary>
        /// Score
        /// </summary>
        [Binding]
        public int Score
        {
            get => _score;
            set
            {
                if (Equals(_score, value))
                {
                    return;
                }

                _score = value;
                OnPropertyChanged(nameof(Score));
            }
        }

        private int _score;

        #endregion

        protected override async UniTask InternalInitialize(Params prm)
        {
            Score = prm.Score;
            
            Rewards.Clear();
            Rewards.AddRange(prm.Rewards.Select(v => new RankingScreen.RankReward()
            {
                BackedData = v
            }).ToList());
        }
        
        [Binding]
        public ObservableList<RankingScreen.RankReward> Rewards { get; set; } = new();

        [Binding]
        public void BattleAgain()
        {
            IOC.Resolve<RankingScreen>().NavToBattle();
        }
        
        [Binding]
        public async void NavBack()
        {
            DoClose();
            await Nav.ShowScreenAsync<RankingScreen>();
        }
        
        [Binding]
        public async void NavToHome()
        {
            DoClose();
            await Nav.ShowScreenAsync<MainScreen>();
        }
            
        public static async Task Show(Params prm)
        {
            await Show<RankingRewardClaimModal>(prm);
        }

        public class Params
        {
            public int Score;
            public List<RankReward> Rewards = new();
        }
    }
}
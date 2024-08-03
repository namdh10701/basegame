using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Game.Features.Dialogs;
using _Game.Features.Inventory;
using Cysharp.Threading.Tasks;
using Online.Model;
using Unity.VisualScripting;
using UnityWeld.Binding;

namespace _Game.Features.Ranking
{
    [Binding]
    public partial class RankingRewardClaimModal : AsyncModal<object, List<RankReward>>
    {
        [Binding]
        public ObservableList<RankingScreen.RankReward> Records { get; set; } = new();

        protected override async UniTask InternalInitialize(List<RankReward> rewards)
        {
            Records.Clear();
            
            Records.AddRange(rewards.Select(v => new RankingScreen.RankReward()
            {
                BackedData = v
            }).ToList());
        }
            
        public static async Task Show(List<RankReward> rewards)
        {
            await Show<RankingRewardClaimModal>(rewards);
        }
    }
}
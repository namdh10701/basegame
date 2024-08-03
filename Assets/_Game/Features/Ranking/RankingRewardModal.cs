using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Game.Features.Dialogs;
using _Game.Features.Inventory;
using Cysharp.Threading.Tasks;
using Online;
using Online.Enum;
using Online.Model;
using Unity.VisualScripting;
using UnityWeld.Binding;

namespace _Game.Features.Ranking
{
    [Binding]
    public partial class RankingRewardModal : AsyncModal<object, ERank>
    {
        [Binding]
        public ObservableList<ClaimRewardBundle> Records { get; set; } = new();

        protected override async UniTask InternalInitialize(ERank userRank)
        {
            Records.Clear();
            // Records.AddRange(PlayfabManager.Instance.Ranking.RewardBundleInfo.Bundles.Select(
            //     v => new ClaimRewardBundle
            //     {
            //         BackedData = v
            //     }).ToList()
            // );
        }
            
        public static async Task Show(ERank userRank)
        {
            await Show<RankingRewardModal>(userRank);
        }
    }
}
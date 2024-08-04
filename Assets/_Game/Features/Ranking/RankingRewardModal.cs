using System;
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
            
            foreach (var rank in Enum.GetValues(typeof(ERank)).Cast<ERank>().Skip(1).Reverse())
            {
                var rec = new ClaimRewardBundle
                {
                    BackedData = new ()
                    {
                        Rank = rank,
                        IsCurrentRank = rank == PlayfabManager.Instance.Rank,
                        Rewards = new()
                        {
                            new ()
                            {
                                ItemType = ItemType.MISC,
                                ItemId = MiscItemId.gold,
                                Amount = 500
                            },
                            new ()
                            {
                                ItemType = ItemType.MISC,
                                ItemId = MiscItemId.gem,
                                Amount = 5
                            }
                        }
                    }
                };
                Records.Add(rec);
            }
        }
            
        public static async Task Show(ERank userRank)
        {
            await Show<RankingRewardModal>(userRank);
        }
    }
}
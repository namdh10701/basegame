using System.Collections.Generic;
using System.Threading.Tasks;
using _Game.Features.Dialogs;
using _Game.Features.Inventory;
using Cysharp.Threading.Tasks;
using Online.Model;
using UnityWeld.Binding;

namespace _Game.Features.Ranking
{
    [Binding]
    public partial class RankingRewardModal : AsyncModal<object, UserRank>
    {
        [Binding]
        public ObservableList<ClaimRewardBundle> Records { get; set; } = new();

        protected override async UniTask InternalInitialize(UserRank userRank)
        {
            Records.Clear();
            
            var backedData = new Online.Model.ClaimRewardBundle
            {
                Rank = UserRank.Hunter,
                IsClaimed = false,
                Rewards = new List<RankReward>
                {
                    new()
                    {
                        ItemType = ItemType.CANNON,
                        ItemId = "0012",
                        Amount = 3
                    },
                    new()
                    {
                        ItemType = ItemType.MISC,
                        ItemId = MiscItemId.blueprint_cannon,
                        Amount = 1
                    }
                }
            };
            
            Records.Add(new ClaimRewardBundle(userRank == backedData.Rank)
            {
                BackedData = backedData
            });
            
            var backedData1 = new Online.Model.ClaimRewardBundle
            {
                Rank = UserRank.Captain,
                IsClaimed = false,
                Rewards = new List<RankReward>
                {
                    new()
                    {
                        ItemType = ItemType.CANNON,
                        ItemId = "0012",
                        Amount = 3
                    },
                    new()
                    {
                        ItemType = ItemType.MISC,
                        ItemId = MiscItemId.blueprint_cannon,
                        Amount = 1
                    }
                }
            };
            
            Records.Add(new ClaimRewardBundle(userRank == backedData1.Rank)
            {
                BackedData = backedData1
            });
        }
            
        public static async Task Show(UserRank userRank)
        {
            await Show<RankingRewardModal>(userRank);
        }
    }
}
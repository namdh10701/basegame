using System.Linq;
using System.Threading.Tasks;
using _Game.Scripts.DB;
using _Game.Scripts.UI;
using Online;
using Online.Enum;
using Online.Model;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.Ranking
{
    public partial class RankingRewardModal
    {
        [Binding]
        public class ClaimRewardBundle: SubViewModel
        {
            #region BackedData

            private Online.Model.ClaimRewardBundle _backedData;
            public Online.Model.ClaimRewardBundle BackedData
            {
                get => _backedData;
                set
                {
                    if (Equals(_backedData, value))
                    {
                        return;
                    }
                    _backedData = value;
                    
                    RewardItems.Clear();
                    
                    foreach (var backedDataRecord in _backedData.Rewards)
                    {
                        RewardItems.Add(new RankingScreen.RankReward()
                        {
                            BackedData = backedDataRecord
                        });
                    }
                    
                    OnPropertyChanged(nameof(IsClaimed));
                }
            }

            #endregion
            
            [Binding]
            public ObservableList<RankingScreen.RankReward> RewardItems { get; set; } = new();
            
            [Binding]
            public ObservableList<RankingScreen.RankReward2> ClaimedRewardItems 
                => new(RewardItems.Select(v => new RankingScreen.RankReward2()
                {
                    BackedData = v.BackedData
                }));

            #region Binding Prop: RankName

            /// <summary>
            /// RankName
            /// </summary>
            [Binding]
            public string RankName => UserRank.ToString();

            #endregion
            
            #region Binding Prop: RankBadge

            /// <summary>
            /// RankBadge
            /// </summary>
            [Binding]
            public Sprite RankBadge => Database.GetRankingTierBadge(UserRank);

            #endregion

            #region Binding Prop: UserRank

            /// <summary>
            /// UserRank
            /// </summary>
            [Binding]
            public ERank UserRank => BackedData.Rank;

            #endregion

            #region Binding Prop: IsClaimed

            /// <summary>
            /// IsClaimed
            /// </summary>
            [Binding]
            public bool IsClaimed
            {
                get => BackedData.IsClaimed;
                set
                {
                    if (Equals(BackedData.IsClaimed, value))
                    {
                        return;
                    }

                    BackedData.IsClaimed = value;
                    OnPropertyChanged(nameof(IsClaimed));
                    OnPropertyChanged(nameof(IsCurrentRankUnClaimed));
                    OnPropertyChanged(nameof(IsCurrentRankClaimed));
                }
            }

            #endregion

            #region Binding Prop: IsCurrentRank

            /// <summary>
            /// IsCurrentRank
            /// </summary>
            [Binding]
            public bool IsCurrentRank => BackedData.IsCurrentRank;

            #endregion
            [Binding] public bool IsCurrentRankUnClaimed => IsCurrentRank && !IsClaimed;
            [Binding] public bool IsCurrentRankClaimed => IsCurrentRank && IsClaimed;
            

            [Binding]
            public async void DoClaim()
            {
                await RankingRewardClaimModal.Show(BackedData.Rewards);
                IsClaimed = await PlayfabManager.Instance.Ranking.ClaimRewardBundle();
            }
        }
    }
}

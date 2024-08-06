using System;
using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.Utils;
using _Base.Scripts.Utils.Extensions;
using _Game.Features.Inventory;
using _Game.Scripts.DB;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.UI;
using Online;
using Online.Enum;
using Online.Model;
using Unity.VisualScripting;
using UnityEngine;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Views;

namespace _Game.Features.Ranking
{
    public partial class RankingScreen
    {
        [Binding]
        public class RankReward2 : RankReward
        {
        }

        [Binding]
        public class RankReward: SubViewModel
        {
            private Online.Model.RankReward _backedData;
            
            public Online.Model.RankReward BackedData
            {
                get => _backedData;
                set
                {
                    if (Equals(_backedData, value))
                    {
                        return;
                    }
                    _backedData = value;
                    
                    OnPropertyChanged(nameof(ItemId));
                    OnPropertyChanged(nameof(ItemType));
                    OnPropertyChanged(nameof(Amount));
                    OnPropertyChanged(nameof(Thumbnail));
                }
            }
            
            [Binding]
            public string ItemId => BackedData.ItemId;

            [Binding]
            public ItemType ItemType => BackedData.ItemType;
            
            [Binding]
            public string Amount => $"x{BackedData.Amount}";

            [Binding]
            public Sprite Thumbnail => Database.GetItemSprite(ItemType, ItemId);
        }
		
        [Binding]
        public class RankRecord: SubViewModel
        {
            #region BackedData

            private Online.Model.PlayerRankInfo _backedData;
            
            public Online.Model.PlayerRankInfo BackedData
            {
                get => _backedData;
                set
                {
                    if (Equals(_backedData, value))
                    {
                        return;
                    }
                    _backedData = value;
                    
                    Rewards.Clear();
                    
                    // foreach (var backedDataRecord in _backedData.Rewards)
                    // {
                    //     Rewards.Add(new RankReward
                    //     {
                    //         BackedData = backedDataRecord
                    //     });
                    // }
                    
                    OnPropertyChanged(nameof(No));
                    OnPropertyChanged(nameof(Username));
                    OnPropertyChanged(nameof(Score));
                    OnPropertyChanged(nameof(Rewards));
                }
            }

            #endregion

            #region Binding Prop: IsActive

            /// <summary>
            /// IsActive
            /// </summary>
            [Binding]
            public bool IsActive
            {
                get => _isActive;
                set
                {
                    if (Equals(_isActive, value))
                    {
                        return;
                    }

                    _isActive = value;
                    OnPropertyChanged(nameof(IsActive));
                }
            }

            private bool _isActive;

            #endregion

            public int Num;

            public RankRecord(int num)
            {
                Num = num;
            }

            [Binding] public bool IsTop1 => Num == 1;
            [Binding] public bool IsTop2 => Num == 2;
            [Binding] public bool IsTop3 => Num == 3;
            [Binding] public bool IsUpRank => Num is >= 1 and <= 15;
            [Binding] public bool IsDownRank => Num is > 15 and <= 50;
            [Binding] public bool IsMidRank => !IsUpRank && !IsDownRank;
            
            [Binding] public Sprite GiftBoxSprite => CachedResources.Load<Sprite>(
                    IsTop1 ? "ranking_gift_top_1" : 
                    IsTop2 ? "ranking_gift_top_2" : 
                    IsTop3 ? "ranking_gift_top_3" : 
                    IsUpRank ? "ranking_gift_1" :
                    IsMidRank ? "ranking_gift_2" : 
                    "ranking_gift_3");
            
            [Binding]
            public string No => $"{Num.ToString().PadLeft(2, '0')}";
            
            [Binding]
            public string Username => BackedData.DisplayName;

            [Binding]
            public string Score => $"{BackedData.Score:n0}";
            
            [Binding]
            public ObservableList<RankReward> Rewards { get; set; } = new();
        }

        [Binding]
        public class UserRankInfo: SubViewModel
        {

            #region BackedData

            private Online.Model.RankInfo _backedData;
            public Online.Model.RankInfo BackedData
            {
                get => _backedData;
                set
                {
                    if (Equals(_backedData, value))
                    {
                        return;
                    }
                    _backedData = value;
                    
                    Records.Clear();

                    for (var index = 0; index < _backedData.Players.Count; index++)
                    {
                        var backedDataRecord = _backedData.Players[index];
                        Records.Add(new RankRecord(index + 1)
                        {
                            IsActive = PlayfabManager.Instance.Profile.PlayfabID == backedDataRecord.Id,
                            BackedData = backedDataRecord,
                        });
                    }

                    SeasonExpireAt = DateTimeOffset
                        .FromUnixTimeSeconds((long)PlayfabManager.Instance.SeasonInfo.EndTimestamp).UtcDateTime;
                    
                    OnPropertyChanged(nameof(SeasonNo));
                    OnPropertyChanged(nameof(SeasonName));
                    OnPropertyChanged(nameof(Rank));
                    OnPropertyChanged(nameof(RankBadge));
                    OnPropertyChanged(nameof(RecordsUpRank));
                    OnPropertyChanged(nameof(RecordsMidRank));
                    OnPropertyChanged(nameof(RecordsDownRank));
                    OnPropertyChanged(nameof(SeasonExpireAt));
                }
            }

            #endregion
            
            private ERank _eRank;
            public ERank UserRank
            {
                get => _eRank;
                set
                {
                    _eRank = value;
                }
            }

            #region Binding Prop: SeasonExpireAt

            /// <summary>
            /// SeasonExpireAt
            /// </summary>
            [Binding]
            public DateTime SeasonExpireAt
            {
                get => _seasonExpireAt;
                set
                {
                    if (Equals(_seasonExpireAt, value))
                    {
                        return;
                    }

                    _seasonExpireAt = value;
                    OnPropertyChanged(nameof(SeasonExpireAt));
                    OnPropertyChanged(nameof(SeasonRemainingTime));
                }
            }

            private DateTime _seasonExpireAt;

            #endregion

            [Binding] 
            public bool IsSeasonExpired => SeasonExpireAt <= DateTime.UtcNow;

            [Binding] 
            public string SeasonRemainingTime => SeasonExpireAt.GetRemainingTime();
            
            [Binding]
            public string SeasonNo => $"Rank Season {PlayfabManager.Instance.SeasonInfo?.SeasonNo}";
            
            [Binding]
            public string SeasonName => PlayfabManager.Instance.SeasonInfo?.SeasonName;
            
            [Binding]
            public string Rank => PlayfabManager.Instance.Rank.ToString();
            
            [Binding]
            public Sprite RankBadge => Database.GetRankingTierBadge(PlayfabManager.Instance.Rank);
            
            [Binding]
            public Sprite NextRankBadge
            {
                get
                {
                    var rank = UserRank.GetNext();
                    return rank == null ? null : Database.GetRankingTierBadge(rank.Value);
                }
            }

            [Binding]
            public Sprite PrevRankBadge
            {
                get
                {
                    var rank = UserRank.GetPrevious();
                    return rank == null ? null : Database.GetRankingTierBadge(rank.Value);
                }
            }

            private List<RankRecord> Records { get; set; } = new();

            [Binding] 
            public List<RankRecord> RecordsUpRank => Records.Take(15).ToList();
            
            [Binding]
            public List<RankRecord> RecordsMidRank => Records.Skip(15).Take(20).ToList();
           
            [Binding]
            public List<RankRecord> RecordsDownRank => Records.Skip(35).Take(15).ToList();

            public void RefreshSeasonRemainingTime()
            {
                OnPropertyChanged(nameof(SeasonRemainingTime));
                OnPropertyChanged(nameof(IsSeasonExpired));
            }
        }
    }
}

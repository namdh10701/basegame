using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Base.Scripts.Utils;
using _Game.Features.Battle;
using _Game.Features.BattleLoading;
using _Game.Features.Dialogs;
using _Game.Features.FightNodeInfoPopup;
using _Game.Features.Inventory;
using _Game.Features.Quest;
using Cysharp.Threading.Tasks;
using Online;
using Online.Enum;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Screens;
using ZBase.UnityScreenNavigator.Core.Views;
using ItemType = _Game.Features.Inventory.ItemType;

namespace _Game.Features.Ranking
{
    [Binding]
    public partial class RankingScreen : ScreenWithViewModel, IPointerDownHandler
    {
        /// <summary>
        /// IsFocusRewardsVisible
        /// </summary>
        [Binding]
        public bool IsFocusRewardsVisible => _focusedRankRecord != null;

        #region Binding Prop: IsInfoBubbleVisible

        /// <summary>
        /// IsInfoBubbleVisible
        /// </summary>
        [Binding]
        public bool IsInfoBubbleVisible
        {
            get => _isInfoBubbleVisible;
            set
            {
                if (Equals(_isInfoBubbleVisible, value))
                {
                    return;
                }

                _isInfoBubbleVisible = value;
                OnPropertyChanged(nameof(IsInfoBubbleVisible));
            }
        }

        private bool _isInfoBubbleVisible;

        #endregion

        #region Binding Prop: FocusedRankRewardBubblePos

        /// <summary>
        /// FocusedRankRewardBubblePos
        /// </summary>
        [Binding]
        public Vector3 FocusedRankRewardBubblePos
        {
            get => _focusedRankRewardBubblePos;
            set
            {
                if (Equals(_focusedRankRewardBubblePos, value))
                {
                    return;
                }

                _focusedRankRewardBubblePos = value;
                OnPropertyChanged(nameof(FocusedRankRewardBubblePos));
            }
        }

        private Vector3 _focusedRankRewardBubblePos;

        #endregion

        #region Binding Prop: FocusedRankRecord

        /// <summary>
        /// FocusedRankRecord
        /// </summary>
        [Binding]
        public RankRecord FocusedRankRecord
        {
            get => _focusedRankRecord;
            set
            {
                if (Equals(_focusedRankRecord, value))
                {
                    return;
                }

                _focusedRankRecord = value;
                OnPropertyChanged(nameof(IsFocusRewardsVisible));
                OnPropertyChanged(nameof(FocusedRankRecord));
            }
        }

        private RankRecord _focusedRankRecord;

        #endregion

        #region Binding Prop: RankInfo

        /// <summary>
        /// RankInfo
        /// </summary>
        [Binding]
        public UserRankInfo RankInfo
        {
            get => _rankInfo;
            set
            {
                if (Equals(_rankInfo, value))
                {
                    return;
                }

                _rankInfo = value;
                OnPropertyChanged(nameof(RankInfo));
            }
        }

        private UserRankInfo _rankInfo = new UserRankInfo();

        #endregion

        #region Binding Prop: OwnedTicketCount

        /// <summary>
        /// OwnedTicketCount
        /// </summary>
        [Binding]
        public int OwnedTicketCount
        {
            get => _ownedTicketCount;
            set
            {
                if (Equals(_ownedTicketCount, value))
                {
                    return;
                }

                _ownedTicketCount = value;
                OnPropertyChanged(nameof(OwnedTicketCount));
                OnPropertyChanged(nameof(TotalTicketCount));
            }
        }

        private int _ownedTicketCount;

        #endregion

        #region Binding Prop: FreeTicketCount

        /// <summary>
        /// FreeTicketCount
        /// </summary>
        [Binding]
        public int FreeTicketCount
        {
            get => _freeTicketCount;
            set
            {
                if (Equals(_freeTicketCount, value))
                {
                    return;
                }

                _freeTicketCount = value;
                OnPropertyChanged(nameof(FreeTicketCount));
                OnPropertyChanged(nameof(TotalTicketCount));
            }
        }

        private int _freeTicketCount;

        #endregion

        #region Binding Prop: TotalTicketCount

        /// <summary>
        /// TotalTicketCount
        /// </summary>
        [Binding]
        public int TotalTicketCount => FreeTicketCount + OwnedTicketCount;

        #endregion

        protected override void Awake()
        {
            base.Awake();
            IOC.Register(this);
        }

        private string _stageId;
        public override async UniTask Initialize(Memory<object> args)
        {
            _stageId = args.ToArray().FirstOrDefault() as string;

            var rankInfo = PlayfabManager.Instance.Ranking.UserRankInfo;
            RankInfo = new UserRankInfo
            {
                BackedData = rankInfo
            };

            OwnedTicketCount = 10;
            FreeTicketCount = 2;
            
            // Start timer
            InvokeRepeating(nameof(UpdateTimer), 0f, 1f);
        }

        public override UniTask Cleanup(Memory<object> args)
        {
            CancelInvoke(nameof(UpdateTimer));
            return base.Cleanup(args);
        }

        private void UpdateTimer()
        {
            RankInfo.RefreshSeasonRemainingTime();
        }

        [Binding]
        public async void NavBack()
        {
            await ScreenContainer.Find(ContainerKey.Screens).PopAsync(true);
        }
        
        [Binding]
        public async void NavToBattle()
        {
            var confirmed = await RankingBattleConfirmModal.Show(new()
            {
                Resources = new List<OwnedConsumableResource>()
                {
                    new ()
                    {
                        ItemType = ItemType.MISC,
                        ItemId = MiscItemId.energy,
                        TotalAmount = 1000,
                        NeedAmount = 99,
                    },
                    
                    new ()
                    {
                        ItemType = ItemType.MISC,
                        ItemId = MiscItemId.ranking_ticket,
                        TotalAmount = 10,
                        NeedAmount = 1,
                    }
                }
            });

            if (!confirmed)
            {
                return;
            }

            var resp = await PlayfabManager.Instance.Ranking.CreateRankTicketAsync();
            if (!resp.Result)
            {
                if (resp.Error == EErrorCode.NotEnoughTicket)
                {
                    await AlertModal.Show("Not Enough Resource!");
                }

                return;
            }

            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
            var screenContainer = ScreenContainer.Find(ContainerKey.Screens);
            
            await screenContainer.PushAsync(new 
                ScreenOptions(nameof(BattleLoadingScreen), stack: false));
            
            await UniTask.Delay(3000);
            
            await screenContainer.PushAsync(
                new ScreenOptions(nameof(BattleScreen), stack: false));
        }

        [Binding]
        public async void ShowRewardPopup()
        {
            await RankingRewardModal.Show(RankInfo.UserRank);
        }

        [Binding]
        public async void ShowInfoBubble()
        {
            IsInfoBubbleVisible = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            FocusedRankRecord = null;
            IsInfoBubbleVisible = false;
        }
    }
}

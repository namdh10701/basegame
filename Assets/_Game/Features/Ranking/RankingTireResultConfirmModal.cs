using System.Threading.Tasks;
using _Game.Features.Dialogs;
using _Game.Scripts.DB;
using Cysharp.Threading.Tasks;
using Online.Enum;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.Ranking
{
    [Binding]
    public partial class RankingTireResultConfirmModal : AsyncModal<Void, RankingTireResultConfirmModal.Params>
    {
        #region Binding Prop: DisplayMode

        /// <summary>
        /// Mode
        /// </summary>
        [Binding]
        public Mode DisplayMode
        {
            get => _displayMode;
            set
            {
                if (Equals(_displayMode, value))
                {
                    return;
                }

                _displayMode = value;
                OnPropertyChanged(nameof(DisplayMode));
                OnPropertyChanged(nameof(IsPromoteMode));
                OnPropertyChanged(nameof(IsDemoteMode));
                OnPropertyChanged(nameof(IsRemainMode));
            }
        }

        private Mode _displayMode;

        #endregion

        #region Binding Prop: Rank

        /// <summary>
        /// Rank
        /// </summary>
        [Binding]
        public ERank Rank
        {
            get => _rank;
            set
            {
                if (Equals(_rank, value))
                {
                    return;
                }

                _rank = value;
                OnPropertyChanged(nameof(Rank));
                OnPropertyChanged(nameof(RankThumbnail));
                OnPropertyChanged(nameof(RankName));
            }
        }

        private ERank _rank;

        #endregion

        [Binding] 
        public Sprite RankThumbnail => Database.GetRankingTierBadge(Rank);
        
        [Binding] 
        public string RankName => Rank.ToString();
        
        [Binding]
        public bool IsPromoteMode => DisplayMode == Mode.Promote;
        
        [Binding]
        public bool IsDemoteMode => DisplayMode == Mode.Demote;
        
        [Binding]
        public bool IsRemainMode => DisplayMode == Mode.Remain;

        protected override async UniTask InternalInitialize(Params @params)
        {
            Rank = @params.rank;
            DisplayMode = @params.displayMode;
        }

        [Binding]
        public void DoConfirm()
        {
            DoClose();
        }
            
        public static async Task Show(ERank rank, Mode displayMode)
        {
            await Show<RankingTireResultConfirmModal>(new Params(rank, displayMode));
        }

        public class Params
        {
            public ERank rank;
            public Mode displayMode;

            public Params(ERank rank, Mode displayMode)
            {
                this.rank = rank;
                this.displayMode = displayMode;
            }
        }

        public enum Mode
        {
            Promote,
            Demote,
            Remain
        }
    }
}
using _Game.Features.Ranking;
using _Game.Scripts.UI;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.RankingReward
{
    [Binding]
    public class RankingRewardItem : SubViewModel
    {
        public RankingRewardModal RankingRewardModal { get; set; }
        
        [Binding]
        public string Id { get; set; }
        
        #region Binding Prop: Name

        /// <summary>
        /// Name
        /// </summary>
        [Binding]
        public string Name
        {
            get => m_name;
            set
            {
                if (Equals(m_name, value))
                {
                    return;
                }

                m_name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string m_name;

        #endregion

        #region Binding Prop: Progress

        /// <summary>
        /// Progress
        /// </summary>
        [Binding]
        public float Progress
        {
            get => _progress;
            set
            {
                if (Equals(_progress, value))
                {
                    return;
                }

                _progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }

        private float _progress;

        #endregion
        
        #region Binding Prop: ProgressMax

        /// <summary>
        /// ProgressMax
        /// </summary>
        [Binding]
        public float ProgressMax
        {
            get => _progressMax;
            set
            {
                if (Equals(_progressMax, value))
                {
                    return;
                }

                _progressMax = value;
                OnPropertyChanged(nameof(ProgressMax));
            }
        }

        private float _progressMax;

        #endregion

        #region Binding Prop: IsProgressDone

        /// <summary>
        /// IsProgressDone
        /// </summary>
        [Binding]
        public bool IsProgressDone => (int)Progress == (int)ProgressMax;

        #endregion

        #region Binding Prop: IsClaimed

        /// <summary>
        /// IsClaimed
        /// </summary>
        [Binding]
        public bool IsClaimed
        {
            get => _isClaimed;
            set
            {
                if (Equals(_isClaimed, value))
                {
                    return;
                }

                _isClaimed = value;
                OnPropertyChanged(nameof(IsClaimed));
            }
        }

        private bool _isClaimed;

        #endregion

        #region Binding Prop: ClaimButtonText

        /// <summary>
        /// ClaimButtonText
        /// </summary>
        [Binding]
        public string ClaimButtonText => IsClaimed ? "Claimed" : "Claim";
        #endregion

        #region Binding Prop: ProgressText

        /// <summary>
        /// ProgressText
        /// </summary>
        [Binding]
        public string ProgressText => $"{Progress}/{ProgressMax}";

        #endregion
        
        [Binding]
        public Sprite Thumbnail => Resources.Load<Sprite>($"Images/Cannon/cannon_{Id}");

        [Binding]
        public void Claim()
        {
            IsClaimed = true;
        }
        
        [Binding]
        public void Go()
        {
            
        }
    }
}
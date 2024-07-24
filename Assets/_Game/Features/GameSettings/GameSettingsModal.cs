using System;
using _Base.Scripts.Audio;
using _Game.Scripts.SaveLoad;
using Cysharp.Threading.Tasks;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;

namespace _Game.Features.GamePause
{
    [Binding]
    public class GameSettingsModal : ModalWithViewModel
    {
        #region Binding Prop: ActiveTabIndex

        /// <summary>
        /// ActiveTabIndex
        /// </summary>
        [Binding]
        public int ActiveTabIndex
        {
            get => _activeTabIndex;
            set
            {
                if (Equals(_activeTabIndex, value))
                {
                    return;
                }

                _activeTabIndex = value;
                OnPropertyChanged(nameof(ActiveTabIndex));
            }
        }

        private int _activeTabIndex;

        #endregion


        #region Binding Prop: MuteBGM

        /// <summary>
        /// MuteBGM
        /// </summary>
        [Binding]
        public bool MuteBGM
        {
            get => _muteBGM;
            set
            {
                if (Equals(_muteBGM, value))
                {
                    return;
                }

                _muteBGM = value;
                OnPropertyChanged(nameof(MuteBGM));
            }
        }

        private bool _muteBGM;

        #endregion

        #region Binding Prop: MuteSFX

        /// <summary>
        /// MuteSFX
        /// </summary>
        [Binding]
        public bool MuteSFX
        {
            get => _muteSFX;
            set
            {
                if (Equals(_muteSFX, value))
                {
                    return;
                }

                _muteSFX = value;
                OnPropertyChanged(nameof(MuteSFX));
            }
        }

        private bool _muteSFX;

        #endregion

        #region Binding Prop: Language

        /// <summary>
        /// Language
        /// </summary>
        [Binding]
        public string Language
        {
            get => _language;
            set
            {
                if (Equals(_language, value))
                {
                    return;
                }

                _language = value;
                OnPropertyChanged(nameof(Language));
            }
        }

        private string _language;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        [Binding]
        public ObservableList<Language> Languages { get; } = new();
        
        public override async UniTask Initialize(Memory<object> args)
        {
            Languages.Clear();
            Languages.Add(new Language("China", "China"));
            Languages.Add(new Language("English", "English"));
            Languages.Add(new Language("France", "France"));
            Languages.Add(new Language("Germany", "Germany"));
            Languages.Add(new Language("Japan", "Japan"));
            Languages.Add(new Language("Korea", "Korea"));
            Languages.Add(new Language("Spain", "Spain"));
            Languages.Add(new Language("Taiwan", "Taiwan"));
            Languages.Add(new Language("Thai", "Thai"));
            Languages.Add(new Language("Vietnam", "Vietnam"));
        }

        void ToggleBgm(bool isOn)
        {
            AudioManager.Instance.IsBgmOn = isOn;
        }

        void ToggleGameSound(bool isOn)
        {
            AudioManager.Instance.IsSfxOn = isOn;
        }

        protected override void Awake()
        {
            base.Awake();
            MuteBGM = SaveSystem.GameSave.Settings.MuteBGM;
            MuteSFX = SaveSystem.GameSave.Settings.MuteSFX;
        }

        [Binding]
        public void SaveSettings()
        {
            SaveSystem.GameSave.Settings.MuteBGM = MuteBGM;
            SaveSystem.GameSave.Settings.MuteSFX = MuteSFX;
            SaveSystem.GameSave.Settings.Language = Language;

            AudioManager.Instance.IsBgmOn = !MuteBGM;
            AudioManager.Instance.IsSfxOn = !MuteSFX;
            
            SaveSystem.SaveGame();

            Close();
        }

        [Binding]
        public async void Close()
        {
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
        }

        [Binding]
        public void OnClickSignInWithFacebook()
        {
            //TODO: DNguyen
        }
    }
}
using System;
using System.Threading.Tasks;
using _Base.Scripts.Audio;
using _Game.Features.Dialogs;
using _Game.Scripts.SaveLoad;
using Cysharp.Threading.Tasks;
using Online;
using UnityEngine;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using Void = _Game.Features.Dialogs.Void;

namespace _Game.Features.GameSettings
{
    [Binding]
    public class GameSettingsModal : AsyncModal
    {
        public static async Task Show()
        {
            await Show<GameSettingsModal>();
        }
        
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

        #region Binding Prop: IsGuest

        /// <summary>
        /// IsLoggedIn
        /// </summary>
        [Binding]
        public bool IsGuest => PlayfabManager.Instance.Profile.IsGuest;

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

        #region Binding Prop: PlayerId

        /// <summary>
        /// PlayerId
        /// </summary>
        [Binding]
        public string PlayerId => PlayfabManager.Instance.Profile.PlayfabID;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        [Binding]
        public ObservableList<Language> Languages { get; } = new();

        protected override UniTask InternalInitialize(Void args)
        {
            MuteBGM = PlayerPrefs.GetInt("Settings.MuteBGM", 0) == 1;
            MuteSFX = PlayerPrefs.GetInt("Settings.MuteSFX", 0) == 1;
            
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
            
            return UniTask.CompletedTask;
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
            // MuteBGM = SaveSystem.GameSave.Settings.MuteBGM;
            // MuteSFX = SaveSystem.GameSave.Settings.MuteSFX;
            
        }

        [Binding]
        public async void SaveSettings()
        {
            PlayerPrefs.SetInt("Settings.MuteBGM", MuteBGM ? 1 : 0);
            PlayerPrefs.SetInt("Settings.MuteSFX", MuteSFX ? 1 : 0);
            PlayerPrefs.SetString("Settings.Language", Language);
            PlayerPrefs.Save();
            // SaveSystem.GameSave.Settings.MuteBGM = MuteBGM;
            // SaveSystem.GameSave.Settings.MuteSFX = MuteSFX;
            // SaveSystem.GameSave.Settings.Language = Language;

            AudioManager.Instance.IsBgmOn = !MuteBGM;
            AudioManager.Instance.IsSfxOn = !MuteSFX;
            
            SaveSystem.SaveGame();

            await DoClose();
        }
        
        [Binding]
        public void DoCopyPlayerId()
        {
            UniClipboard.SetText(PlayfabManager.Instance.Profile.PlayfabID);
        }

        [Binding]
        public async void OpenLinkAccountPopup()
        {
            await LinkAccountModal.Show();
        }

        [Binding]
        public async void DoUnlinkAccount()
        {
            await PlayfabManager.Instance.UnlinkFacebook();
        }

        [Binding]
        public async void DoDeleteAccount()
        {
            var confirm = await ConfirmModal.Show("Please note that deleting your account will permanently remove all of your associated data from our servers, and this cannot be undone. Do you want to delete your account? ");
            if (!confirm)
            {
                return;
            }
            
            //TODO: DNGUYEN - delete account
        }
    }
}
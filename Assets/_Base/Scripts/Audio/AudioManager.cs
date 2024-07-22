using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using _Base.Scripts.Patterns.Pool;
using _Base.Scripts.Utils;
using UnityEngine;

namespace _Base.Scripts.Audio
{
    public class AudioConfig
    {
        public enum AudioType
        {
            SFX,
            BGM
        };

        public AudioType Type;
        public AudioClip Clip;
    }

    public class AudioManager : SingletonMonoBehaviour<AudioManager>
    {
#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern void _playVibrate();
#endif
        [Header("Sound Configs")]
        [SerializeField] protected AudioSource _sampleSoundSource;

        [Header("Common - SFXs")]
        Dictionary<SoundID, AudioClip> btnSfxDic = new Dictionary<SoundID, AudioClip>();

        [SerializeField] protected AudioClip btn_general;
        [SerializeField] protected AudioClip btn_cancel;
        [SerializeField] protected AudioClip btn_popup_open;
        [SerializeField] protected AudioClip btn_popup_close;
        [SerializeField] protected AudioClip btn_navigate_home;
        [SerializeField] protected AudioClip btn_navigate_shop;
        [SerializeField] protected AudioClip btn_navigate_myship;
        [SerializeField] protected AudioClip btn_navigate_inventory;
        [SerializeField] protected AudioClip btn_navigate_habor;
        [SerializeField] protected AudioClip btn_turbo;
        [SerializeField] protected AudioClip btn_confirm;


        [SerializeField] protected AudioClip[] _monsterTakeHits;
        [SerializeField] protected AudioClip _shipMoveForwad;
        [SerializeField] protected AudioClip _shipidle;

        [Header("BGMs")]
        [SerializeField] protected AudioClip _bgmHome;
        [SerializeField] protected AudioClip _bgmGameplay;

        // const string KEY_SFX = "SFX";
        // const string KEY_BGM = "BGM";
        const string KEY_VIBRATE = "VIBRATE";

        bool _isSfxOn;
        bool _isBgmOn;
        bool _isVibrate;

        private float _sfxVolume = 1.0f;
        private float _bgmVolume = 1.0f;

        ObjectPool<Transform> _poolSounds;
        List<AudioSource> _activeSources = new List<AudioSource>();

        List<AudioConfig> _soundConfigs = new List<AudioConfig>();

        public event Action<bool> OnEnableMusic;


        protected override void Awake()
        {
            _poolSounds = new ObjectPool<Transform>(_sampleSoundSource.transform);
            btnSfxDic = new Dictionary<SoundID, AudioClip> {
                { SoundID.btn_general, btn_general },
                { SoundID.btn_confirm, btn_confirm},
                { SoundID.btn_popup_open, btn_popup_open},
                { SoundID.btn_popup_close, btn_popup_close},
                { SoundID.btn_cancel, btn_cancel},
                { SoundID.btn_navigate_home, btn_navigate_home},
                { SoundID.btn_navigate_shop, btn_navigate_shop},
                { SoundID.btn_navigate_myship, btn_navigate_myship},
                { SoundID.btn_navigate_inventory, btn_navigate_inventory},
                { SoundID.btn_navigate_habor, btn_navigate_habor},
                { SoundID.btn_turbo, btn_turbo},
            };
        }

        // public void LoadSoundSettings()
        // {
        //     if (!PlayerPrefs.HasKey(KEY_SFX))
        //     {
        //         IsSfxOn = true;
        //     }
        //
        //     if (PlayerPrefs.HasKey(KEY_BGM))
        //     {
        //         IsBgmOn = true;
        //     }
        //
        //     if (PlayerPrefs.HasKey(KEY_VIBRATE))
        //     {
        //         IsVibrateOn = true;
        //     }
        //
        //
        //     IsSfxOn = PlayerPrefs.GetInt(KEY_SFX, 1) > 0 ? true : false;
        //     IsBgmOn = PlayerPrefs.GetInt(KEY_BGM, 1) > 0 ? true : false;
        //     IsVibrateOn = PlayerPrefs.GetInt(KEY_VIBRATE, 1) > 0 ? true : false;
        // }

        public void ToggleSfx()
        {
            IsSfxOn = !_isSfxOn;
        }

        public void ToggleBGM()
        {
            IsBgmOn = !_isBgmOn;
        }

        public void ToggleVibrate()
        {
            IsVibrateOn = !_isVibrate;
        }

        public bool IsSfxOn
        {
            get
            {
                return _isSfxOn;
            }

            set
            {
                if (value != _isSfxOn)
                {
                    _isSfxOn = value;
                    // PlayerPrefs.SetInt(KEY_SFX, _isSfxOn ? 1 : 0);
                    // PlayerPrefs.Save();
                }

#if USE_ANDROID_NATIVE_AUDIO && !UNITY_EDITOR
            AndroidNativeAudio.Off = !_isSfxOn;
#else
                SetVolumeSFXs(_isSfxOn ? 1f : 0f);
#endif
            }
        }

        public bool IsBgmOn
        {
            get
            {
                return _isBgmOn;
            }

            set
            {
                if (value != _isBgmOn)
                {
                    _isBgmOn = value;
                    // PlayerPrefs.SetInt(KEY_BGM, _isBgmOn ? 1 : 0);
                    // PlayerPrefs.Save();
                }

#if USE_ANDROID_NATIVE_AUDIO && !UNITY_EDITOR
            AndroidNativeAudio.Off = !_isBgmOn;
#else
                SetVolumeBGMs(_isBgmOn ? 1f : 0f);
#endif
                OnEnableMusic?.Invoke(_isBgmOn);

                // if (!value)
                //     StopBGMs();

                //GoogleMobileAds.Api.MobileAds.SetApplicationVolume(_isSfxOn ? 1f : 0f);
            }
        }

        public bool IsVibrateOn
        {
            get
            {
                return _isVibrate;
            }

            set
            {
                if (value != _isVibrate)
                {
                    _isVibrate = value;
                    PlayerPrefs.SetInt(KEY_VIBRATE, _isVibrate ? 1 : 0);
                    PlayerPrefs.Save();
                }
                //Taptic.tapticOn = _isVibrate;
            }
        }

        void SetVolumeBGMs(float volume)
        {
            _bgmVolume = volume;
            foreach (var source in _activeSources)
                if (source.loop)
                    source.volume = _bgmVolume;
        }

        void SetVolumeSFXs(float volume)
        {
            _sfxVolume = volume;
            foreach (var source in _activeSources)
                if (!source.loop)
                    source.volume = _sfxVolume;
        }

        void PlaySound(AudioConfig soundConfig)
        {
            if (soundConfig.Type == AudioConfig.AudioType.BGM && isPlayingSameBGM(soundConfig))
                return;

            var audioSource = _poolSounds.Get();
            audioSource.parent = _sampleSoundSource.transform.parent;
            var audioCom = audioSource.GetComponent<AudioSource>();
            StartCoroutine(PlaySound(audioCom, soundConfig));
        }

        IEnumerator PlaySound(AudioSource source, AudioConfig config)
        {
            _soundConfigs.Add(config);
            source.clip = config.Clip;
            source.loop = config.Type == AudioConfig.AudioType.BGM;
            source.volume = (config.Type == AudioConfig.AudioType.SFX) ? _sfxVolume : _bgmVolume;
            if (config.Type == AudioConfig.AudioType.BGM)
            {
                yield return FadeBGMsVolumes(0);
                StopBGMs();
            }
            _activeSources.Add(source);
            source.Play();
            if (config.Type == AudioConfig.AudioType.BGM)
            {
                yield return FadeBGMsVolumes(_bgmVolume);
            }
            yield return new WaitForSeconds(0.1f);
            while (source.isPlaying)
                yield return null;
            source.Stop();
            _soundConfigs.Remove(config);
            _activeSources.Remove(source);
            _poolSounds.Store(source.transform);
        }


        IEnumerator FadeBGMsVolumes(float targetVolume)
        {
            float startVolume = _bgmVolume;
            float timer = 0f;
            float fadeDuration = 0.25f;
            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                foreach (var item in _activeSources)
                {
                    if (item.loop && item.isPlaying)
                    {
                        item.volume = Mathf.Lerp(startVolume, targetVolume, timer / fadeDuration);
                    }
                }
                yield return null;
            }
        }

        private bool isPlayingSameBGM(AudioConfig newConfig)
        {
            foreach (var source in _activeSources)
            {
                if (source.loop && source.isPlaying)
                {
                    return source.clip == newConfig.Clip;
                }
            }
            return false;
        }

        public void StopBGMs()
        {
            foreach (var item in _activeSources)
            {
                if (item.loop && item.isPlaying)
                {
                    item.Stop();
                }
            }
        }

        public void PauseBGMs()
        {
            foreach (var item in _activeSources)
            {
                if (item.loop)
                {
                    item.volume = 0.0f;
                }
            }
        }

        public void ResumeBGMs()
        {
            foreach (var item in _activeSources)
            {
                if (item.loop)
                {
                    item.volume = 1.0f;
                }
            }
        }

        public void StopSFXs()
        {
            foreach (var item in _activeSources)
            {
                if (!item.loop && item.isPlaying)
                {
                    item.Stop();
                }
            }
        }

        public void PlayVibrate()
        {
            if (!IsVibrateOn) return;

#if UNITY_IOS && !UNITY_EDITOR
        _playVibrate();
#elif UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
        
        AndroidJavaClass vibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect");
        if (vibrationEffectClass != null)
        {
            long milliseconds = 40;
            int amplitude = 60;
            var parameters = new object[] { milliseconds, amplitude };
            AndroidJavaObject vibrationEffect = vibrationEffectClass.CallStatic<AndroidJavaObject>("createOneShot", parameters);
            vibrator.Call("vibrate", vibrationEffect);
        }
#else
            /* Debug.Log("Play Vibrate");
             Handheld.Vibrate();*/
#endif
        }

        void TestVibration()
        {
            //Handheld.Vibrate();
        }

        public void PlaySfxTapButton(SoundID soundId)
        {
            PlaySound(new AudioConfig()
            {
                Clip = btnSfxDic[soundId],
                Type = AudioConfig.AudioType.SFX
            });
        }

        /// <summary>
        /// GAMEPLAY
        /// </summary>



        /// <summary>
        /// BGM
        /// </summary>
        public void PlayBgmHome()
        {
            // if (!IsBgmOn) return;
            PlaySound(new AudioConfig()
            {
                Clip = _bgmHome,
                Type = AudioConfig.AudioType.BGM
            });
        }
        public void PlayBgmGameplay()
        {
            IsBgmOn = true;
            if (!IsBgmOn) return;
            PlaySound(new AudioConfig()
            {
                Clip = _bgmGameplay,
                Type = AudioConfig.AudioType.BGM
            });
        }

        public void PlayShipMove()
        {
            PlaySound(new AudioConfig()
            {
                Clip = _shipMoveForwad,
                Type = AudioConfig.AudioType.SFX
            });
        }

        public void PlayShipIdle()
        {
            PlaySound(new AudioConfig()
            {
                Clip = _shipidle,
                Type = AudioConfig.AudioType.SFX
            });
        }

        internal void PlayPopupClose()
        {
            PlaySound(new AudioConfig()
            {
                Clip = btn_popup_close,
                Type = AudioConfig.AudioType.SFX
            });
        }

        internal void PlayPopupOpen()
        {
            PlaySound(new AudioConfig()
            {
                Clip = btn_popup_open,
                Type = AudioConfig.AudioType.SFX
            });
        }
    }
}
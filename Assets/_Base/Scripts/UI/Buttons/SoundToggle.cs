using _Base.Scripts.Audio;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Base.Scripts.UI.Buttons
{
    public class SoundToggle : UIBehaviour
    {
        private Toggle button;
        [SerializeField] private SoundID _soundId = SoundID.btn_general;

        protected override void Awake()
        {
            base.Awake();
            button = GetComponent<Toggle>();
        }
        protected override void Start()
        {
            base.Start();
            button.onValueChanged.AddListener(PlaySound);
        }

        protected override void OnDestroy()
        {
            button.onValueChanged.RemoveListener(PlaySound);
        }

        private void PlaySound(bool isOn)
        {
            if (isOn)
                AudioManager.Instance.PlaySfxTapButton(_soundId);
        }
    }
}
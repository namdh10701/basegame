using _Base.Scripts.Audio;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Base.Scripts.UI.Buttons
{
    public class SoundButton : UIBehaviour
    {
        private Button button;
        [SerializeField] private SoundID _soundId = SoundID.btn_general;

        protected override void Awake()
        {
            base.Awake();
            button = GetComponent<Button>();
        }
        protected override void Start()
        {
            base.Start();
            button.onClick.AddListener(PlaySound);
        }

        protected override void OnDestroy()
        {
            button.onClick.RemoveListener(PlaySound);
        }

        private void PlaySound()
        {
            AudioManager.Instance.PlaySfxTapButton(_soundId);
        }
    }
}
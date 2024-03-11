using UnityEngine;
using UnityEngine.EventSystems;
using Game.Audio;
using UnityEngine.UI;
public class SoundButton : UIBehaviour
{
    private Button button;
    [SerializeField] private SoundID _soundId = SoundID.Button_Click;

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
        AudioManager.Instance.PlaySfxTapButton();
    }
}
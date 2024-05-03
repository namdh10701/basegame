using _Base.Scripts.Database;
using _Base.Scripts.UI.Managers;
using _Game.Scripts.InventorySystem;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI
{
    public class GearFrame : MonoBehaviour
    {
        [SerializeField] private GearType allowGearType;
        [SerializeField] private Image GearImage;
        [SerializeField] private Button btn;
        private Gear gear;

        public GearType AllowGearType => allowGearType;
        public void SetData(Gear gear)
        {
            this.gear = gear;
            Sprite image = ResourceLoader.LoadGearImage(gear);
            GearImage.sprite = image;
        }

        private void OnEnable()
        {
            btn.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            btn.onClick.RemoveListener(OnClick);
        }

        void OnClick()
        {
            GearInfoPopup gearInfoPopup = PopupManager.Instance.GetPopup<GearInfoPopup>();
            gearInfoPopup.SetData(gear);
            PopupManager.Instance.ShowPopup(gearInfoPopup);
        }
    }
}
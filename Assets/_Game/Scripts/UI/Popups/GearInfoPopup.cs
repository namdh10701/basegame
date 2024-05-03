using _Base.Scripts.UI;
using _Base.Scripts.UI.Managers;
using _Game.Scripts.InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace _Game.Scripts.UI
{
    public class GearInfoPopup : Popup
    {
        [SerializeField] Image image;
        [SerializeField] TextMeshProUGUI rarity;
        [SerializeField] TextMeshProUGUI gearName;
        [SerializeField] Button equipBtn;

        Gear gear;
        public void SetData(Gear gear)
        {
            this.gear = gear;
            Sprite gearImage = ResourceLoader.LoadGearImage(gear);
            image.sprite = gearImage;
            rarity.text = gear.Rarity.ToString();
            gearName.text = gear.Name;
        }
        private void OnEnable()
        {
            equipBtn.onClick.AddListener(OnEquipClick);
        }

        private void OnDisable()
        {
            equipBtn.onClick.RemoveListener(OnEquipClick);
        }

        void OnEquipClick()
        {
            InventoryView.OnEquipGear.Invoke(gear);
            PopupManager.Instance.HideImmediately(this);
        }
    }
}
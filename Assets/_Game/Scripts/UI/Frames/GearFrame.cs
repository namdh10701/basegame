using _Base.Scripts.Database;
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
        private Gear gearDefinition;

        public GearType AllowGearType => allowGearType;
        public void SetData(Gear gearDefinition)
        {
            this.gearDefinition = gearDefinition;
            Sprite image = ResourceLoader.LoadGearImage(gearDefinition.Id.Id, gearDefinition.GearType);
            GearImage.sprite = image;
        }
    }
}
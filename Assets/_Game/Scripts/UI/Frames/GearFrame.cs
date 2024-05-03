using _Base.Scripts.Database;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI
{
    public class GearFrame : MonoBehaviour
    {
        [SerializeField] private GearType allowGearType;
        [SerializeField] private Image GearImage;
        private GearDefinition gearDefinition;

        public GearType AllowGearType => allowGearType;
        public void SetData(GearDefinition gearDefinition)
        {
            this.gearDefinition = gearDefinition;
            Sprite image = ResourceLoader.LoadGearImage(gearDefinition.Id);
            GearImage.sprite = image;
        }
    }
}
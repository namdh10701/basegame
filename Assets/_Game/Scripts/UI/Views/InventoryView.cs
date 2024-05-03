using _Base.Scripts.UI;
using _Game.Scripts.SaveLoad;
using System.Collections.Generic;
using UnityEngine;
namespace _Game.Scripts.UI
{
    public class InventoryView : View
    {
        [SerializeField] private GearFrame hat;
        [SerializeField] private GearFrame necklace;
        [SerializeField] private GearFrame sword;
        [SerializeField] private CharacterFrame characterFrame;
        [SerializeField] private GearFrame gearFramePrefab;
        [SerializeField] private GameObject noGearMsg;
        [SerializeField] private GameObject scrollRect;
        public override void Initialize()
        {
            base.Initialize();
            InitGearSlots();
            InitCharacterFrame();
            InitGearCollection();
        }

        void InitGearSlots()
        {
            /*GearCombination equipingGears = SaveSystem.GameSave.EquipingGears;
            foreach (GearDefinition gearDefinition in equipingGears.GearDefinitions)
            {
                GearKey gearKey = gearDefinition.Id;

                switch (gearKey.GearType)
                {
                    case GearType.Hat:
                        hat.SetData(gearDefinition);
                        break;
                    case GearType.Sword:
                        sword.SetData(gearDefinition);
                        break;
                    case GearType.Necklace:
                        necklace.SetData(gearDefinition);
                        break;
                }
            }*/
        }

        void InitCharacterFrame()
        {
            CharacterDefinition characterDefinition = SaveSystem.GameSave.SelectedCharacter;
            characterFrame.SetData(characterDefinition);
        }

        void InitGearCollection()
        {
            /*List<GearDefinition> ownedGears = SaveSystem.GameSave.OwnedGears;
            if (ownedGears.Count == 0)
            {
                noGearMsg.SetActive(true);
                scrollRect.gameObject.SetActive(false);
            }
            else
            {
                noGearMsg.SetActive(false);
                scrollRect.gameObject.SetActive(true);
                Transform root = scrollRect.transform.GetChild(0);
                foreach (GearDefinition gearDefinition in ownedGears)
                {

                }
            }*/
        }
    }
}
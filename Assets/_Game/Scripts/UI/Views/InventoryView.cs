using _Base.Scripts.UI;
using _Base.Scripts.UI.Managers;
using _Game.Scripts.InventorySystem;
using _Game.Scripts.SaveLoad;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace _Game.Scripts.UI
{
    public class InventoryView : View
    {
        [SerializeField] private GearFrame hat;
        [SerializeField] private GearFrame necklace;
        [SerializeField] private GearFrame sword;
        [SerializeField] private CrewFrame characterFrame;
        [SerializeField] private GearFrame gearFramePrefab;
        [SerializeField] private GameObject noGearMsg;
        [SerializeField] private GameObject scrollRect;
        [SerializeField] private Transform inventoryRoot;
        [SerializeField] private Button backBtn;

        public static Action<Gear> OnEquipGear;
        public override void Initialize()
        {
            base.Initialize();
            //InitGearSlots();
            //InitCharacterFrame();
            //InitGearCollection();
        }
        private void Start()
        {
            InitGearCollection();
            InitGearSlots();
            OnEquipGear += EquipGear;
        }

        private void OnDestroy()
        {
            OnEquipGear -= EquipGear;
        }

        private void OnEnable()
        {
            backBtn.onClick.AddListener(OnBackClick);
        }

        private void OnDisable()
        {
            backBtn.onClick.RemoveListener(OnBackClick);
        }

        void OnBackClick()
        {
            ViewManager.Instance.Show<HomeView>();
        }


        void EquipGear(Gear newGear)
        {
            List<GearData> equipingGears = SaveSystem.GameSave.InventorySaveData.EquippingGears;
            for (int i = 0; i < equipingGears.Count; i++)
            {
                if (equipingGears[i].GearType == newGear.GearType)
                {
                    equipingGears[i] = new GearData(newGear.Id, newGear.GearType, newGear.Rarity);
                    SaveSystem.SaveGame();
                    InitGearSlots();
                    return;
                }
            }
            equipingGears.Add(new GearData(newGear.Id, newGear.GearType, newGear.Rarity));
            SaveSystem.SaveGame();
            InitGearSlots();

        }

        void InitGearSlots()
        {
            List<GearData> equipingGears = SaveSystem.GameSave.InventorySaveData.EquippingGears;
            foreach (GearData gearData in equipingGears)
            {
                Gear gear = new Gear(gearData);
                switch (gear.GearType)
                {
                    case GearType.Hat:
                        hat.SetData(gear);
                        break;
                    case GearType.Sword:
                        sword.SetData(gear);
                        break;
                    case GearType.Necklace:
                        necklace.SetData(gear);
                        break;
                }
            }
        }

        void InitCharacterFrame()
        {
           /* CharacterDefinition characterDefinition = SaveSystem.GameSave.SelectedCharacter;
            characterFrame.SetData(characterDefinition);*/
        }

        void InitGearCollection()
        {
            List<GearData> ownedGears = SaveSystem.GameSave.GetOwnedGears();
            if (ownedGears.Count == 0)
            {
                noGearMsg.SetActive(true);
                scrollRect.gameObject.SetActive(false);
            }
            else
            {
                noGearMsg.SetActive(false);
                scrollRect.gameObject.SetActive(true);
                foreach (GearData gearData in ownedGears)
                {
                    Gear gear = new Gear(gearData);
                    GearFrame gearFrame = Instantiate(gearFramePrefab, inventoryRoot);
                    gearFrame.SetData(gear);
                }
            }
        }
    }
}
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
        [SerializeField] private CharacterFrame characterFrame;
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
            SaveList<GearSlot> equipingGears = SaveSystem.GameSave.EquipingGears;
            for (int i = 0; i < equipingGears.Items.Count; i++)
            {
                if (equipingGears.Items[i].GearType == newGear.GearType)
                {
                    equipingGears.Items[i].Gear = newGear;
                    SaveSystem.SaveGame();
                    InitGearSlots();
                    return;
                }
            }
            equipingGears.Items.Add(new GearSlot(newGear.GearType, newGear));
            SaveSystem.SaveGame();
            InitGearSlots();

        }

        void InitGearSlots()
        {
            SaveList<GearSlot> equipingGears = SaveSystem.GameSave.EquipingGears;
            foreach (GearSlot gearSlot in equipingGears.Items)
            {
                switch (gearSlot.GearType)
                {
                    case GearType.Hat:
                        hat.SetData(gearSlot.Gear);
                        break;
                    case GearType.Sword:
                        sword.SetData(gearSlot.Gear);
                        break;
                    case GearType.Necklace:
                        necklace.SetData(gearSlot.Gear);
                        break;
                }
            }
        }

        void InitCharacterFrame()
        {
            CharacterDefinition characterDefinition = SaveSystem.GameSave.SelectedCharacter;
            characterFrame.SetData(characterDefinition);
        }

        void InitGearCollection()
        {
            List<Gear> ownedGears = SaveSystem.GameSave.GetOwnedGears();
            if (ownedGears.Count == 0)
            {
                noGearMsg.SetActive(true);
                scrollRect.gameObject.SetActive(false);
            }
            else
            {
                noGearMsg.SetActive(false);
                scrollRect.gameObject.SetActive(true);
                foreach (Gear gear in ownedGears)
                {
                    GearFrame gearFrame = Instantiate(gearFramePrefab, inventoryRoot);
                    gearFrame.SetData(gear);
                }
            }
        }
    }
}
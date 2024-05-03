using System.Collections.Generic;
using System.IO;
using _Base.Scripts.SaveSystem;
using _Game.Scripts.InventorySystem;
using UnityEngine;
namespace _Game.Scripts.SaveLoad
{
    public class SaveData : IBinarySaveData
    {
        public static SaveData DefaultSave = GetDefaultSave();

        public int SaveId;
        public SaveList<GearSlot> EquipingGears;
        public InventoryCollection<Gear> OwnedInventoryItems;
        public CharacterDefinition SelectedCharacter;

        public SaveData(int saveId)
        {
            SaveId = saveId;
        }
        public SaveData(int saveId, SaveList<GearSlot> gearCombination, InventoryCollection<Gear> ownedInventoryItems)
        {
            SaveId = saveId;
            EquipingGears = gearCombination;
            OwnedInventoryItems = ownedInventoryItems;
        }
        public SaveData()
        {
            EquipingGears = new SaveList<GearSlot>();
            OwnedInventoryItems = new InventoryCollection<Gear>();
        }

        public void Read(BinaryReader br)
        {
            SaveId = br.ReadInt32();
            EquipingGears.Read(br);
            OwnedInventoryItems.Read(br);
        }

        public void Write(BinaryWriter bw)
        {
            bw.Write(SaveId);
            EquipingGears.Write(bw);
            OwnedInventoryItems.Write(bw);
        }

        public static SaveData GetDefaultSave()
        {
            SaveData defaultSave = new SaveData(1, new SaveList<GearSlot>(), new InventoryCollection<Gear>());
            InventoryCollection<Gear> inventoryCollection = new InventoryCollection<Gear>();
            inventoryCollection.Items.Add(new Gear(new InventoryId(1, InventoryType.Gear), "Sword 1", GearType.Sword, Rarity.Uncommon, null));
            inventoryCollection.Items.Add(new Gear(new InventoryId(2, InventoryType.Gear), "Sword 2", GearType.Sword, Rarity.Uncommon, null));
            inventoryCollection.Items.Add(new Gear(new InventoryId(1, InventoryType.Gear), "Hat 1", GearType.Hat, Rarity.Uncommon, null));
            inventoryCollection.Items.Add(new Gear(new InventoryId(2, InventoryType.Gear), "Hat 2", GearType.Hat, Rarity.Uncommon, null));
            inventoryCollection.Items.Add(new Gear(new InventoryId(1, InventoryType.Gear), "Necklace 1", GearType.Necklace, Rarity.Epic, null));
            inventoryCollection.Items.Add(new Gear(new InventoryId(2, InventoryType.Gear), "Necklace 2", GearType.Necklace, Rarity.Legend, null));
            defaultSave.OwnedInventoryItems = inventoryCollection;

          
            return defaultSave;
        }

        public List<Gear> GetOwnedGears()
        {
            List<Gear> ownedGears = new List<Gear>();
            foreach (InventoryItem inventoryItem in OwnedInventoryItems.Items)
            {
                if (inventoryItem.Id.InventoryType == InventoryType.Gear)
                {
                    ownedGears.Add((Gear)inventoryItem);
                }
            }
            return ownedGears;
        }
    }
}


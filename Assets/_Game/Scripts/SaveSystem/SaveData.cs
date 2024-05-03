using System.Collections.Generic;
using System.IO;
using _Base.Scripts.SaveSystem;

namespace _Game.Scripts.SaveLoad
{
    public class SaveData : IBinarySaveData
    {
        public static SaveData DefaultSave = GetDefaultSave();

        public int SaveId;
        public GearCombination EquipingGears;
        public List<GearDefinition> OwnedGears;
        public CharacterDefinition SelectedCharacter;

        public SaveData(int saveId)
        {
            SaveId = saveId;
        }
        public SaveData(int saveId, GearCombination gearCombination, List<GearDefinition> ownedGears)
        {
            SaveId = saveId;
            EquipingGears = gearCombination;
            OwnedGears = ownedGears;
        }
        public SaveData()
        {

        }

        public void Read(BinaryReader br)
        {

        }

        public void Write(BinaryWriter bw)
        {
        }

        public static SaveData GetDefaultSave()
        {
            SaveData defaultSave = new SaveData(1);
            List<GearDefinition> ownedGears = new List<GearDefinition>();
            return defaultSave;
        }
    }
}


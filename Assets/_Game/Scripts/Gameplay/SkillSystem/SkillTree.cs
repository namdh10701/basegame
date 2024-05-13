using _Game.Scripts.SaveLoad;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.SkillSystem
{
    public class SkillTree : MonoBehaviour
    {
        public int TreeId;
        public List<SkillNode> Roots;
        public List<SkillNode> AllNodes;
        public Action OnSkillPointChanged;
        private void Start()
        {
            SkillSaveData skillSaveData = new SkillSaveData(1);
            skillSaveData.SkillDatas.Add(new SkillData(1, 1));
            skillSaveData.SkillDatas.Add(new SkillData(2, 1));
            foreach (SkillNode skillNode in AllNodes)
            {
                skillNode.Init(skillSaveData);
            }
            TestSaveLoad();
        }

        void TestSaveLoad()
        {
            SaveSystem.LoadSave();
            foreach (IInventoryData gd in SaveSystem.GameSave.InventorySaveData.OwnedInventories)
            {
                if (gd is GearData gearData)
                {
                    Debug.Log(gearData.Id + " " + gearData.Type.ToString() + " " + gearData.Rarity);
                }
            }

            foreach (SkillData sd in SaveSystem.GameSave.SkillSaveData.SkillDatas)
            {
                Debug.Log(sd.Id + "| DEFAULT |");
            }
        }

        public void AddSkillPoint(int id, int amount)
        {
            SkillSaveData skillSaveData = SaveSystem.GameSave.SkillSaveData;

            foreach (SkillData sd in skillSaveData.SkillDatas)
            {
                if (sd.Id == id)
                {
                    sd.Level += amount;
                }
            }
            SaveSystem.SaveGame();

            foreach(SkillNode sn in AllNodes)
            {
                sn.RefreshUI();
            }
        }

        void UpdateState()
        {

        }
    }
}

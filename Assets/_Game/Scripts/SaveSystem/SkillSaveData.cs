using _Game.Scripts.SkillSystem;
using System.Collections.Generic;

namespace _Game.Scripts.SaveLoad
{
    [System.Serializable]
    public class SkillSaveData
    {
        public int SkillTreeId;
        public List<SkillData> SkillDatas;
        public SkillSaveData(int treeId)
        {
            SkillTreeId = treeId;
            SkillDatas = new List<SkillData>();
        }
        public int GetLevel(int Id)
        {
            foreach (SkillData skillData in SkillDatas)
            {
                if (skillData.Id == Id)
                {
                    return skillData.Level;
                }
            }
            return 0;
        }

        public SkillData GetSkillData(int Id)
        {
            foreach (SkillData skillData in SkillDatas)
            {
                if (skillData.Id == Id)
                {
                    return skillData;
                }
            }
            return null;
        }

    }
}

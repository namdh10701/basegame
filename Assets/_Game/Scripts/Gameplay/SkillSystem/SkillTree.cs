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

        private void Start()
        {
            SkillSaveData skillSaveData = new SkillSaveData();
            skillSaveData.SkillDatas.Add(new SkillData(1, 1));
            skillSaveData.SkillDatas.Add(new SkillData(2, 1));
            foreach (SkillNode skillNode in AllNodes)
            {
                skillNode.Init(skillSaveData);
            }
        }

        void UpdateState()
        {
          
        }
    }
}

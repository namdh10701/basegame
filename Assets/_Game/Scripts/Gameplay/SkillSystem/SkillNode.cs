using _Game.Scripts.SaveLoad;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.SkillSystem
{
    public class SkillNode : MonoBehaviour
    {
        public SkillDefinition SkillDefinition;
        public List<Prerequisite> Prerequisites;
        public Action<SkillNode> OnClickAction;
        public SkillNodeUI SkillNodeUI;


        public void Init(SkillSaveData skillSaveData)
        {
            if (Prerequisites != null)
            {
                foreach (Prerequisite prerequisite in Prerequisites)
                {
                    int level = skillSaveData.GetLevel(prerequisite.SkillDef.Id);
                    prerequisite.line.color = level > 0 ? Color.white : Color.black;
                }
                SkillNodeUI.Init(SkillDefinition, skillSaveData.GetLevel(SkillDefinition.Id));
            }
        }
    }
}

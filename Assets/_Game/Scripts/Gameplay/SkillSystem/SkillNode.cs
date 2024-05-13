using _Game.Scripts.SaveLoad;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.SkillSystem
{
    public class SkillNode : MonoBehaviour
    {
        public SkillData skillData;
        public SkillDefinition SkillDefinition;

        public List<Prerequisite> Prerequisites;

        public Image Image;
        public TextMeshProUGUI lvText;
        public Button BuySkillBtn;

        public void Init(SkillSaveData skillSaveData)
        {
            Sprite sprite = ResourceLoader.LoadSkillImage(SkillDefinition.Id);
            Image.sprite = sprite;
            skillData = skillSaveData.GetSkillData(SkillDefinition.Id);
            RefreshUI();
        }

        public void RefreshUI()
        {
            if (Prerequisites != null)
            {
                foreach (Prerequisite prerequisite in Prerequisites)
                {
                    int level = SaveSystem.GameSave.SkillSaveData.GetLevel(prerequisite.SkillDef.Id);
                    prerequisite.line.color = level > 0 ? Color.white : Color.black;
                }
            }
            lvText.text = $"Lv {skillData.Level}";
        }
    }
}

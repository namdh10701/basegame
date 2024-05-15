using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.SkillSystem
{
    [System.Serializable]
    public struct Prerequisite
    {
        public SkillDefinition SkillDef;
        public int Level;
        public Image line;
    }
}
using UnityEngine;

namespace _Game.Scripts.SkillSystem
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Skill Definition")]
    public class SkillDefinition : ScriptableObject
    {
        public int Id;
        public string Name;
        public string Description;
        public int MaxLevel;
    }
}
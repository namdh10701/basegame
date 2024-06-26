using _Base.Scripts.UI;
using _Game.Scripts.SkillSystem;
using UnityEngine;

namespace _Game.Scripts.UI
{
    public class SkillTreeView : View
    {
        [SerializeField] Transform skillTreeRoot;

        SkillTree SkillTree;
        public override void Initialize()
        {
            base.Initialize();
            InitSkillTree();
        }

        void InitSkillTree()
        {
            SkillTree = GetSkillTree();
            Instantiate(SkillTree, skillTreeRoot);
        }

        void OnSkillPointChanged()
        {

        }

        SkillTree GetSkillTree()
        {
            SkillTree skillTree = Resources.Load<SkillTree>("Database/SkillTree/PreDefined/SkillTree");
            return skillTree;
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.SkillSystem
{
    public class SkillTree : MonoBehaviour
    {
        public List<SkillNode> Roots;
        public List<SkillNode> AllNodes;

        private void Start()
        {
            UpdateState();
        }

        void UpdateState()
        {
            SkillSaveData skillSaveData = new SkillSaveData();
            skillSaveData.SkillDatas.Add(new SkillData(1, 1));
            skillSaveData.SkillDatas.Add(new SkillData(2, 1));

            foreach (SkillNode skillNode in AllNodes)
            {
                skillNode.Init(skillSaveData);
            }
        }
        /*public void TraverseDFS()
        {
            HashSet<SkillNode> visited = new HashSet<SkillNode>();
            foreach (SkillNode root in Roots)
            {
                DFS(root, visited);
            }
        }

        private void DFS(SkillNode node, HashSet<SkillNode> visited)
        {
            if (node == null || visited.Contains(node))
                return;

            visited.Add(node);

            foreach (SkillNode child in node.Outgoing)
            {
                DFS(child, visited);
            }
        }

        public void TraverseBFS(Action<SkillNode> onEachSkillNode)
        {
            HashSet<SkillNode> visited = new HashSet<SkillNode>();
            Queue<SkillNode> queue = new Queue<SkillNode>();

            foreach (SkillNode root in Roots)
            {
                queue.Enqueue(root);
                visited.Add(root);
            }

            while (queue.Count > 0)
            {
                SkillNode current = queue.Dequeue();
                onEachSkillNode.Invoke(current);
                foreach (SkillNode child in current.Outgoing)
                {
                    if (!visited.Contains(child))
                    {
                        queue.Enqueue(child);
                        visited.Add(child);
                    }
                }
            }
        }

        public void UpdateState()
        {

        }*/
    }

    public class SkillSaveData
    {
        public List<SkillData> SkillDatas;
        public SkillSaveData()
        {
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
    }

    public class SkillData
    {
        public int Id;
        public int Level;

        public SkillData(int id, int level)
        {
            this.Id = id;
            this.Level = level;
        }
    }
}

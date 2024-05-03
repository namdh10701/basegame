using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.SkillSystem
{
    public class SkillTree : MonoBehaviour
    {
        public List<SkillNode> Roots;
        public List<SkillNode> AllNodes;
        public void TraverseDFS()
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

        }
    }
}

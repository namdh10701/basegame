using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Gameplay.TalentTree
{
    [ExecuteAlways]
    public class TalentNode: MonoBehaviour
    {

        public string itemId;
        [FormerlySerializedAs("nodeType")] public NodeType nodeNodeType;

        public GameObject lineTop;
        public GameObject lineRight;
        public GameObject lineBottom;
        public GameObject lineLeft;

        private void Awake()
        {
            if (nodeNodeType == NodeType.Normal)
            {
                lineLeft.SetActive(false);
            }
            else if (nodeNodeType == NodeType.Premium)
            {
                lineRight.SetActive(false);
            }
        }
    }
}
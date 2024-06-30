using UnityEngine;

namespace Map
{
    public enum NodeType
    {
        MinorEnemy,
        MiniBoss,
        Treasure,
        Boss,
        Armory,
        Mystery
    }
}

namespace Map
{
    [CreateAssetMenu]
    public class NodeBlueprint : ScriptableObject
    {
        public Sprite sprite;
        public NodeType nodeType;
    }
}
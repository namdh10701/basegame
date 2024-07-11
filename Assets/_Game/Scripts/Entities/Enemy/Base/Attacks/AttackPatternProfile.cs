using _Game.Scripts;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Attack Pattern Profile")]
    public class AttackPatternProfile : ScriptableObject
    {
        public CellPattern CellPattern;
        public CellPickType CellPickType;
        public int Size;
        public int Size2;
    }
}
using _Base.Scripts.RPG.Effects;
using _Game.Scripts;
using System.Collections.Generic;
namespace _Game.Features.Gameplay
{
    public class EnemyAttackData
    {
        public List<Cell> TargetCells;
        public Cell CenterCell;
        public List<Effect> Effects;
    }
}
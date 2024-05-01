
using _Game.Scripts.Battle;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class RangedEnemy : Enemy
    {
        protected override void Start()
        {
            base.Start();
            GameObject moveArea = GameObject.Find("MoveArea");
            if (moveArea != null)
            {
                Area area = moveArea.GetComponent<Area>();
                _blackboard.GetVariable<AreaVariable>("MoveArea").Value = area;

            }
        }
    }
}

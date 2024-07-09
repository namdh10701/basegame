using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Game.Scripts;
using _Game.Scripts.Entities;
using _Game.Scripts.PathFinding;
using System.Collections.Generic;
using UnityEngine;


namespace _Game.Features.Gameplay
{
    [System.Serializable]
    public class Cell : MonoBehaviour, IStatsBearer, IEffectTaker, IWorkLocation
    {
        public CellRenderer CellRenderer;
        public int X;
        public int Y;
        public Grid Grid;
        public IGridItem GridItem;
        public EffectHandler effectHandler;
        public Transform Transform => transform;

        public EffectHandler EffectHandler => effectHandler;
        public CellStats stats;
        public CellStatsTemplate template;
        public Stats Stats => stats;

        public List<Node> WorkingSlots { get => workingSlots; set => workingSlots = value; }
        public List<Node> workingSlots;
        public EffectTakerCollider EffectCollider;
        public NodeGraph nodeGraph;


        protected void Awake()
        {
            effectHandler.EffectTaker = this;
            EffectCollider.Taker = this;
            InitWorkingSlot();
        }

        public void InitWorkingSlot()
        {
            workingSlots = new List<Node>();
            foreach (var node in nodeGraph.nodes)
            {
                if (node.cell != null && node.cell == this)
                {
                    workingSlots.Add(node);
                    foreach (var neightbor in node.neighbors)
                    {
                        workingSlots.Add(neightbor);
                    }
                }
            }
        }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }

        public void OnFixed()
        {
            CellRenderer.OnFixed();
        }



        public void OnBroken()
        {
            GlobalEvent<Cell, int>.Send("FixCell", this, 3);
            Debug.Log(X + " " + Y);
            CellRenderer.OnBroken();
        }
        public void OnClick()
        {
            if (stats.HealthPoint.Value <= stats.HealthPoint.MinValue)
            {
                GlobalEvent<Cell, int>.Send("FixCell", this, int.MaxValue);
            }
        }

        void IStatsBearer.ApplyStats()
        {

        }
    }
}



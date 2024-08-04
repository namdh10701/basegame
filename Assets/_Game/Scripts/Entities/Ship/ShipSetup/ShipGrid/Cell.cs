using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Game.Scripts;
using _Game.Scripts.Entities;
using _Game.Scripts.PathFinding;
using System;
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
        public CellEffectHandler effectHandler;
        public Transform Transform => transform;

        public EffectHandler EffectHandler => effectHandler;
        public CellStats stats;
        public CellStatsTemplate template;
        public Stats Stats => stats;

        public List<Node> WorkingSlots { get => workingSlots; set => workingSlots = value; }

        public Stat StatusResist => null;

        public List<Node> workingSlots;
        public EffectTakerCollider EffectCollider;
        public NodeGraph nodeGraph;
        public Action OnStateChanged;

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
            isBroken = false;
            OnStateChanged?.Invoke();
        }

        public bool isBroken;


        public void OnBroken()
        {
            GlobalEvent<Cell, int>.Send("FixCell", this, CrewJobData.DefaultPiority[typeof(FixCellTask)]);
            CellRenderer.OnBroken();
           
            isBroken = true;
            OnStateChanged?.Invoke();
        }

        void IStatsBearer.ApplyStats()
        {

        }

    }
}



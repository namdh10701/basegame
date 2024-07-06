using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.GD;
using _Game.Scripts.PathFinding;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class Ammo : MonoBehaviour, IStatsBearer, IEffectTaker, IGridItem, IWorkLocation, INodeOccupier
    {
        public string id;
        public CannonProjectile Projectile;

        [SerializeField] private GridItemDef def;

        [field: SerializeField]
        public List<Cell> OccupyCells { get; set; }
        public GridItemDef Def { get => def; }
        public Transform Behaviour { get => null; }
        public string GridId { get; set; }
        public List<Node> workingSlots = new List<Node>();
        public List<Node> WorkingSlots { get => workingSlots; set => workingSlots = value; }
        public List<Node> occupyingNodes = new List<Node>();
        public List<Node> OccupyingNodes { get => occupyingNodes; set => occupyingNodes = value; }
        public bool IsBroken { get => isBroken; set => isBroken = value; }
        public bool IsAbleToTakeHit { get => stats.HealthPoint.Value > stats.HealthPoint.MinValue; }
        public EffectHandler effectHandler;
        public EffectHandler EffectHandler => effectHandler;

        public Stats Stats => stats;

        public Transform Transform => transform;
        public AmmoStats stats;

        public SpriteRenderer sprite;
        public Color broken;
        public Color norm;
        bool isBroken;



        public void SetId(string id)
        {
            this.id = id;
            Projectile.Id = id;
        }


        public void OnClick()
        {

        }

        public void Deactivate()
        {
            sprite.color = broken;
            IsBroken = true;
        }

        public void OnFixed()
        {
            stats.HealthPoint.StatValue.BaseValue = stats.HealthPoint.MaxStatValue.Value / 100 * 30;
            sprite.color = norm;
            IsBroken = false;
        }

        public void ApplyStats() { }
    }
}
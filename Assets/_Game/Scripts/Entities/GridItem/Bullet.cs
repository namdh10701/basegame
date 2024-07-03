using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.GD;
using _Game.Scripts.PathFinding;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class Bullet : Entity, IGridItem, IWorkLocation, INodeOccupier
    {
        public string id;
        public Projectile Projectile;

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

        public EffectHandler effectHandler;
        public EffectHandler EffectHandler => effectHandler;

        public override Stats Stats => stats;

        public ProjectileStats stats;
        public ProjectileStatsTemplate projectileStatsTemplate;


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
            sprite.color = norm;
            IsBroken = false;
        }

        protected override void LoadStats()
        {
            if (GDConfigLoader.Instance != null)
            {
                if (GDConfigLoader.Instance.Ammos.TryGetValue(id, out AmmoConfig value))
                {
                    value.ApplyGDConfig(Stats);
                }
                else
                {
                    projectileStatsTemplate.ApplyConfig(stats);
                }

            }
        }

        protected override void LoadModifiers()
        {
            
        }

        protected override void ApplyStats()
        {
            
        }
    }
}
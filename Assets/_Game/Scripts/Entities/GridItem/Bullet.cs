using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.GD;
using _Game.Scripts.PathFinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class Bullet : MonoBehaviour, IGridItem, IWorkLocation, INodeOccupier
    {
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


        public ProjectileStats Stats;
        public ProjectileStatsTemplate projectileStatsTemplate;


        public SpriteRenderer sprite;
        public Color broken;
        public Color norm;
        bool isBroken;

        private void Awake()
        {
            if (GDConfigLoader.Instance != null)
            {
                if (GDConfigLoader.Instance.Ammos.TryGetValue(Projectile.Id, out AmmoConfig value))
                {
                    value.ApplyGDConfig(Stats);
                }
                else
                {
                    projectileStatsTemplate.ApplyConfig(Stats);
                }
            
            }
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
    }
}
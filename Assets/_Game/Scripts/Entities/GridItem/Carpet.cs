using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Game.Scripts;
using _Game.Scripts.Entities;
using _Game.Scripts.GD;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.PathFinding;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class Carpet : Entity, IEffectTaker, IGridItem, IWorkLocation
    {
        public string id;

        [field: SerializeField]
        public List<Cell> OccupyCells { get; set; }
        public string GridId { get; set; }

        public List<Node> workingSlots = new List<Node>();
        public List<Node> WorkingSlots { get => workingSlots; set => workingSlots = value; }

        public EffectHandler effectHandler;
        public EffectHandler EffectHandler => effectHandler;


        public Transform Transform => transform;
        public Stat StatusResist => null;
        public override Scripts.Stats Stats => stats;


        [SerializeField] private GridItemStateManager gridItemStateManager;
        public GridItemStateManager GridItemStateManager => gridItemStateManager;

        public CarpetStats stats;

       /* 
        private CannonStatsConfigLoader _configLoader;

        public CannonStatsConfigLoader ConfigLoader
        {
            get
            {
                if (_configLoader == null)
                {
                    _configLoader = new ItemStatsConfigLoader();
                }

                return _configLoader;
            }
        }*/

        public void Initialize()
        {
            /*var conf = GameData.ItemTable.FindById(id);
            ConfigLoader.LoadConfig(stats, conf);*/
        }
        public override void ApplyStats()
        {
            // throw new System.NotImplementedException();

            // broken theo từng cell
        }

        public void OnBroken()
        {

        }

        public void OnFixed()
        {

        }

        public void Active()
        {
        }
    }
}
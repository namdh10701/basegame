using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Game.Scripts;
using _Game.Scripts.Entities;
using _Game.Scripts.GD;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.PathFinding;
using Fusion;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class Carpet : MonoBehaviour, IStatsBearer, ICellSplitItem, IWorkLocation
    {
        public string id;
        public CarpetType CarpetType;
        public int linkType;

        //public CarpetComponent carpetComponentPrefab;
        [field: SerializeField]
        public List<Cell> OccupyCells { get; set; }
        public string GridId { get; set; }
        public Transform Transform => transform;
        public Stat StatusResist => null;

        public CarpetEffectHandler EffectHandler;


        [SerializeField] private GridItemStateManager gridItemStateManager;
        public GridItemStateManager GridItemStateManager => gridItemStateManager;

        public List<ICellSplitItemComponent> components;
        //List<ICellSplitItemComponent> ICellSplitItem.Components => components;
        public Scripts.Stats Stats => stats;
        public bool IsWalkAble => true;
        List<Node> IWorkLocation.WorkingSlots
        {
            get
            {
                List<Node> nodes = new List<Node>();
                foreach (ICellSplitItemComponent component in components)
                {
                    if (component is IWorkLocation workLocation)
                    {
                        foreach (var node in workLocation.WorkingSlots)
                        {
                            if (!nodes.Contains(node))
                            {
                                nodes.Add(node);
                            }
                        }
                    }
                }
                return nodes;
            }
            set
            { return; }
        }


        public Action<Carpet, bool> OnActive;


        public GridItemView gridItemView;
        public CarpetStats stats;
        public List<Cannon> BuffedCannons = new List<Cannon>();
        public List<Ammo> BuffedAmmos = new List<Ammo>();
        public List<Carpet> LinkedCarpet = new List<Carpet>();
        public void Initialize()
        {
            /*var conf = GameData.ItemTable.FindById(id);
            ConfigLoader.LoadConfig(stats, conf);*/
            gridItemStateManager.gridItem = this;
            gridItemView.Init(this);
            components = new List<ICellSplitItemComponent>();
            foreach (Cell cell in OccupyCells)
            {
                cell.OnStateChanged += CheckActive;
                /*CarpetComponent carpetComponent = Instantiate(carpetComponentPrefab, transform);
                carpetComponent.carpet = this;
                carpetComponent.OccupyCells = new List<Cell> { cell };
                carpetComponent.name = cell.ToString();
                components.Add(carpetComponent);
                carpetComponent.GridItemStateManager.OnStateEntered += OnComponentStateChanged;*/
            }
            if (CarpetType == CarpetType.Defense)
            {
                List<CarpetType> carpetTypes = new List<CarpetType>();
                switch (linkType)
                {
                    case 1:
                        carpetTypes.Add(CarpetType.Survival);
                        break;
                    case 2:
                        carpetTypes.Add(CarpetType.Survival);
                        break;
                    case 3:
                        carpetTypes.Add(CarpetType.Survival);
                        break;
                    case 4:
                        carpetTypes.Add(CarpetType.Survival);
                        carpetTypes.Add(CarpetType.Defense);
                        break;
                }
                carpetLinkCondition = new CarpetLinkCondition(carpetTypes);
            }
            Buff();
        }

        bool isbuff;
        public CarpetLinkCondition carpetLinkCondition;
        void Buff()
        {
            if (isbuff)
                return;
            List<Cell> neighborCells = GridHelper.GetNeighborCells(OccupyCells);
            foreach (Cell cell in neighborCells)
            {
                if (cell.GridItem != null)
                {
                    if (cell.GridItem is Cannon cannon)
                    {
                        if (!BuffedCannons.Contains(cannon))
                        {
                            BuffedCannons.Add(cannon);
                            AddCannonStatModifiers(cannon);
                        }
                    }

                    if (cell.GridItem is Ammo ammo)
                    {
                        BuffedAmmos.Add(ammo);
                        AddAmmoStatModifiers(ammo);
                    }

                    if (CarpetType == CarpetType.Defense)
                    {
                        if (cell.GridItem is Carpet carpet)
                        {
                            if (carpetLinkCondition.IsValid(carpet))
                            {
                                if (!LinkedCarpet.Contains(carpet))
                                {
                                    LinkedCarpet.Add(carpet);
                                    stats.Cooldown.AddModifier(new StatModifier(-1, StatModType.Flat, this));
                                }
                            }
                        }
                    }
                }
            }
            isbuff = true;
        }
        void RemoveBuff()
        {
            isbuff = false;
            foreach (Cannon cannon in BuffedCannons)
            {
                RemoveCannonStatModifiers(cannon);
            }
            foreach (Ammo ammo in BuffedAmmos)
            {
                RemoveAmmoStatModifiers(ammo);
            }
        }

        void AddCannonStatModifiers(Cannon cannon)
        {
            CannonStats stats = cannon.Stats as CannonStats;
            stats.InstanceKillChance.AddModifier(new StatModifier(this.stats.InstanceKillChance.Value, StatModType.Flat, this));
            stats.Ammo.MaxStatValue.AddModifier(new StatModifier(this.stats.AmmoBonus.Value, StatModType.Flat, this));
            stats.AttackSpeed.AddModifier(new StatModifier(this.stats.IncreaseAtkSpeed.Value, StatModType.PercentAdd, this));
            stats.AttackDamage.AddModifier(new StatModifier(this.stats.IncreaseDmg.Value, StatModType.PercentAdd, this));
            stats.FeverTime.AddModifier(new StatModifier(this.stats.IncreaseFeverTime.Value, StatModType.Flat, this));
        }

        void AddAmmoStatModifiers(Ammo ammo)
        {
            AmmoStats stats = ammo.Stats as AmmoStats;
            stats.EnergyCost.AddModifier(new StatModifier(this.stats.LowerAmmoMana.Value, StatModType.Flat, this));
        }

        void RemoveCannonStatModifiers(Cannon cannon)
        {
            CannonStats stats = cannon.Stats as CannonStats;
            stats.InstanceKillChance.RemoveAllModifiersFromSource(this);
            stats.Ammo.MaxStatValue.RemoveAllModifiersFromSource(this);
            stats.AttackSpeed.RemoveAllModifiersFromSource(this);
            stats.AttackDamage.RemoveAllModifiersFromSource(this);
            stats.FeverTime.RemoveAllModifiersFromSource(this);
        }

        void RemoveAmmoStatModifiers(Ammo ammo)
        {
            AmmoStats stats = ammo.Stats as AmmoStats;
            stats.EnergyCost.RemoveAllModifiersFromSource(this);
        }

        void CheckActive()
        {
            foreach (Cell cell in OccupyCells)
            {
                if (cell.isBroken)
                {
                    gridItemStateManager.GridItemState = GridItemState.Broken;
                }
            }

            gridItemStateManager.GridItemState = GridItemState.Active;
        }

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

        public void OnBroken()
        {
            GlobalEvent<Carpet, int>.Send("FixCarpet", this, CrewJobData.DefaultPiority[typeof(FixCarpetTask)]);
            OnActive?.Invoke(this, false);
            RemoveBuff();
        }

        public void Active()
        {
            OnActive?.Invoke(this, true);
            Buff();
        }




        public void ApplyStats()
        {

        }

    }
}
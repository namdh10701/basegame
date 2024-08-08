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
using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace _Game.Features.Gameplay
{
    public class Carpet : MonoBehaviour, IStatsBearer, ICellSplitItem, IWorkLocation, IBuffItem, IBuffable
    {
        public string id;
        public CarpetType CarpetType;
        public int[] effectId;
        public List<IGridItem> adjItems = new List<IGridItem>();
        public List<IGridItem> AdjItems { get => adjItems; set => adjItems = value; }
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
        public CarpetShieldManager carpetShield;
        public void Initialize()
        {
            gridItemStateManager.gridItem = this;
            gridItemView.Init(this);
            components = new List<ICellSplitItemComponent>();
            foreach (Cell cell in OccupyCells)
            {
                cell.OnStateChanged += CheckActive;
            }

            if (CarpetType == CarpetType.Defense)
            {
                foreach (int id in effectId)
                {
                    CarpetShieldData shieldData = gameObject.AddComponent<CarpetShieldData>();
                    shieldData.effectId = id;
                    if (id == 1)
                    {
                        shieldData.shield = 1;
                    }
                    if (id == 2)
                    {
                        shieldData.shield = 100;
                    }
                    if (id == 3)
                    {
                        shieldData.shield = 100;
                    }
                    if (id == 4)
                    {
                        shieldData.shield = 100;
                    }
                    shieldDatas.Add(shieldData);
                }
            }
        }

        public void LateInitialize()
        {
            float countOfSurvivalCarpet = 0;
            float countOfDefenseCarpet = 0;
            foreach (IGridItem gridItem in AdjItems)
            {
                if (gridItem is Carpet carpet)
                {
                    if (carpet.CarpetType == CarpetType.Survival)
                    {
                        countOfSurvivalCarpet++;
                    }
                    if (carpet.CarpetType == CarpetType.Defense)
                    {
                        countOfDefenseCarpet++;
                    }
                }
            }
            foreach (CarpetShieldData shieldData in shieldDatas)
            {
                if (shieldData.effectId == 1)
                {
                    shieldData.cooldown -= countOfSurvivalCarpet;
                }
                if (shieldData.effectId == 2)
                {
                    shieldData.cooldown -= countOfSurvivalCarpet;
                }
                if (shieldData.effectId == 3)
                {
                    shieldData.cooldown -= countOfSurvivalCarpet;
                }
                if (shieldData.effectId == 4)
                {
                    shieldData.shield += 50 * (countOfDefenseCarpet + countOfDefenseCarpet);
                    shieldData.cooldown -= countOfSurvivalCarpet;
                    shieldData.cooldown -= countOfDefenseCarpet;
                }
                shieldData.cooldown = Mathf.Min(shieldData.cooldown, 5);
            }
        }

        public List<CarpetShieldData> shieldDatas = new List<CarpetShieldData>();

        public CarpetLinkCondition carpetLinkCondition;

        void AddCannonStatModifiers(Cannon cannon)
        {
            CannonStats stats = cannon.Stats as CannonStats;
            foreach (int id in effectId)
            {
                switch (id)
                {
                    case 1:
                        stats.AttackDamage.AddModifier(new StatModifier(this.stats.IncreaseDmg.Value, StatModType.PercentAdd, this));
                        break;
                    case 2:
                        stats.AttackSpeed.AddModifier(new StatModifier(this.stats.IncreaseAtkSpeed.Value, StatModType.PercentAdd, this));
                        break;
                    case 3:
                        stats.InstanceKillChance.AddModifier(new StatModifier(1, StatModType.Flat, this));
                        break;
                    case 4:
                        stats.Ammo.MaxStatValue.AddModifier(new StatModifier(this.stats.AmmoBonus.Value, StatModType.Flat, this));
                        break;
                    case 5:
                        stats.FeverTime.AddModifier(new StatModifier(this.stats.IncreaseFeverTime.Value, StatModType.Flat, this));
                        break;
                }
            }
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


        void CheckActive()
        {
            foreach (Cell cell in OccupyCells)
            {
                if (cell.isBroken)
                {
                    Debug.Log("BROKEN");
                    gridItemStateManager.GridItemState = GridItemState.Broken;
                    return;
                }
            }

            Debug.Log(" NOT BROKEN");
            gridItemStateManager.GridItemState = GridItemState.Active;
        }

        public void OnBroken()
        {
            //GlobalEvent<Carpet, int>.Send("FixCarpet", this, CrewJobData.DefaultPiority[typeof(FixCarpetTask)]);
            OnActive?.Invoke(this, false);
        }

        public void Active()
        {
            OnActive?.Invoke(this, true);
        }

        public void ApplyStats()
        {

        }

        List<IBuffable> buffItems = new List<IBuffable>();
        public List<IBuffable> BuffedItems => buffItems;

        public BuffRange buffRange;
        public BuffRange BuffRange => buffRange;

        public bool IsBuffable(IBuffable gridItem)
        {
            if (gridItem is Ship)
            {
                return true;
            }
            if (gridItem is Cannon)
            {
                return true;
            }
            return false;
        }

        public void Buff(IBuffable gridItem)
        {
            if (CarpetType == CarpetType.Defense)
            {
                if (carpetShield != null)
                {
                    carpetShield.Buff(gridItem, effectId);
                }
            }
            if (gridItem is Cannon cannon)
            {
                AddCannonStatModifiers(cannon);
            }
            if (gridItem is Ship ship)
            {
                AddShipStatModifiers(ship);
            }
        }

        void AddShipStatModifiers(Ship ship)
        {
            ShipBuffStats shipBuffStats = ship.ShipBuffStats;
            CarpetStats carpetStats = stats;
            switch (CarpetType)
            {

                case CarpetType.Survival:
                    ship.stats.HealthRegenerationRate.AddModifier(new StatModifier(carpetStats.ShipHpRegen.Value, StatModType.Flat, this));
                    shipBuffStats.LifeSteal.AddModifier(new StatModifier(carpetStats.LifeSteal.Value, StatModType.Flat, this));
                    shipBuffStats.AmmoEnergyCostReduce.AddModifier(new StatModifier(carpetStats.ShipHpRegen.Value, StatModType.Flat, this));
                    shipBuffStats.CrewRepairSpeedBoost.AddModifier(new StatModifier(carpetStats.CrewRepairSpeedBoost.Value, StatModType.Flat, this));
                    break;

                case CarpetType.Defense:

                    break;
                case CarpetType.Attack:

                    break;
                case CarpetType.Resource:

                    shipBuffStats.Luck.AddModifier(new StatModifier(carpetStats.Luck.Value, StatModType.Flat, this));
                    shipBuffStats.GoldIncome.AddModifier(new StatModifier(carpetStats.GoldEarning.Value, StatModType.Flat, this));
                    break;
            }
        }


        void RemoveShipStatModifiers(Ship ship)
        {
            ShipBuffStats shipBuffStats = ship.ShipBuffStats;
            CarpetStats carpetStats = stats;
            switch (CarpetType)
            {
                case CarpetType.Survival:
                    ship.stats.HealthRegenerationRate.RemoveAllModifiersFromSource(this);
                    shipBuffStats.LifeSteal.RemoveAllModifiersFromSource(this);
                    shipBuffStats.AmmoEnergyCostReduce.RemoveAllModifiersFromSource(this);
                    shipBuffStats.CrewRepairSpeedBoost.RemoveAllModifiersFromSource(this);
                    break;
                case CarpetType.Defense:
                    break;
                case CarpetType.Attack:

                    break;
                case CarpetType.Resource:

                    shipBuffStats.Luck.RemoveAllModifiersFromSource(this);
                    shipBuffStats.GoldIncome.RemoveAllModifiersFromSource(this);
                    break;
            }
        }



        public void RemoveBuff(IBuffable gridItem)
        {
            if (CarpetType == CarpetType.Defense)
            {
                if (carpetShield != null)
                {
                    carpetShield.RemoveBuff(gridItem);
                }
            }
            if (gridItem is Ship ship)
            {
                RemoveShipStatModifiers(ship);
            }

            if (gridItem is Cannon cannon)
            {
                RemoveCannonStatModifiers(cannon);
            }
        }

        internal CarpetShieldData GetShieldData(int effectId)
        {
            foreach (CarpetShieldData shieldData in shieldDatas)
            {
                if (shieldData.effectId == effectId)
                {
                    return shieldData;
                }
            }
            return null;
        }
    }
}
using _Base.Scripts.RPG.Stats;
using _Game.Scripts;
using _Game.Scripts.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Game.Features.Gameplay
{
    public class ShipStatsController : MonoBehaviour
    {
        public Ship ship;
        public ShipSetup shipSetup;
        public ShipBuffStats shipBuffStats;
        public List<StatModifier> AllStatModifier = new List<StatModifier>();
        List<Carpet> carpets = new List<Carpet>();
        List<Crew> crews = new List<Crew>();
        public void RegisterCrew(Crew crew)
        {
            if (crews.Contains(crew))
                return;
            crews.Add(crew);
            shipBuffStats.Luck.AddModifier(new StatModifier(crew.stats.Luck.Value, StatModType.Flat, crew));
            shipBuffStats.FeverTimeProb.AddModifier(new StatModifier(crew.stats.FeverTimeProb.Value, StatModType.Flat, crew));
            shipBuffStats.GoldIncome.AddModifier(new StatModifier(crew.stats.GoldIncome.Value, StatModType.Flat, crew));
            shipBuffStats.ZeroManaCost.AddModifier(new StatModifier(crew.stats.ZeroManaCost.Value, StatModType.Flat, crew));
            shipBuffStats.BonusAmmo.AddModifier(new StatModifier(crew.stats.BonusAmmo.Value, StatModType.Flat, crew));
        }

        public void UnregisterCrew(Crew crew)
        {
            if (!crews.Contains(crew))
                return;
            crews.Remove(crew);
            shipBuffStats.Luck.RemoveAllModifiersFromSource(crew);
            shipBuffStats.FeverTimeProb.RemoveAllModifiersFromSource(crew);
            shipBuffStats.GoldIncome.RemoveAllModifiersFromSource(crew);
            shipBuffStats.ZeroManaCost.RemoveAllModifiersFromSource(crew);
            shipBuffStats.BonusAmmo.RemoveAllModifiersFromSource(crew);
        }
        public void RegisterCarpet(Carpet carpet)
        {
            if (carpets.Contains(carpet))
                return;
            carpet.OnActive += OnActiveCarpet;
            carpets.Add(carpet);
        }

        private void OnActiveCarpet(Carpet carpet, bool isActive)
        {
            ShipStats shipStats = ship.Stats as ShipStats;
            if (isActive)
            {
                CarpetStats carpetStats = carpet.Stats as CarpetStats;
                shipSetup.CalculateBuff(carpet);
            }
            else
            {
                shipSetup.CalculateRemoveBuff(carpet);
            }
        }

        public void UnregisterCarpet(Carpet carpet)
        {
            if (!carpets.Contains(carpet))
            {
                return;
            }
            carpet.OnActive -= OnActiveCarpet;
            carpets.Remove(carpet);
            CarpetStats carpetStats = carpet.Stats as CarpetStats;
            switch (carpet.CarpetType)
            {
                case CarpetType.Survival:
                    ship.stats.HealthRegenerationRate.RemoveAllModifiersFromSource(carpet);
                    shipBuffStats.LifeSteal.RemoveAllModifiersFromSource(carpet);
                    shipBuffStats.AmmoEnergyCostReduce.RemoveAllModifiersFromSource(carpet);
                    shipBuffStats.CrewRepairSpeedBoost.RemoveAllModifiersFromSource(carpet);
                    break;

                case CarpetType.Defense:


                    break;
                case CarpetType.Attack:

                    break;
                case CarpetType.Resource:
                    shipBuffStats.Luck.RemoveAllModifiersFromSource(carpet);
                    shipBuffStats.GoldIncome.RemoveAllModifiersFromSource(carpet);
                    break;
            }
        }
    }
}

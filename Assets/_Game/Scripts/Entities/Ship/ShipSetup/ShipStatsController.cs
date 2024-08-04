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
        public List<StatModifier> AllStatModifier = new List<StatModifier>();
        List<Carpet> carpets = new List<Carpet>();
        List<Crew> crews = new List<Crew>();
        public void RegisterCrew(Crew crew)
        {
            if (crews.Contains(crew))
                return;
            crews.Add(crew);
            ship.stats.Luck.AddModifier(new StatModifier(crew.stats.Luck.Value, StatModType.Flat, crew));
            ship.stats.FeverTimeProb.AddModifier(new StatModifier(crew.stats.FeverTimeProb.Value, StatModType.Flat, crew));
            ship.stats.GoldIncome.AddModifier(new StatModifier(crew.stats.GoldIncome.Value, StatModType.Flat, crew));
            ship.stats.ZeroManaCost.AddModifier(new StatModifier(crew.stats.ZeroManaCost.Value, StatModType.Flat, crew));
            ship.stats.BonusAmmo.AddModifier(new StatModifier(crew.stats.BonusAmmo.Value, StatModType.Flat, crew));
        }

        public void UnregisterCrew(Crew crew)
        {
            if (!crews.Contains(crew))
                return;
            crews.Remove(crew);
            ship.stats.Luck.RemoveAllModifiersFromSource(crew);
            ship.stats.FeverTimeProb.RemoveAllModifiersFromSource(crew);
            ship.stats.GoldIncome.RemoveAllModifiersFromSource(crew);
            ship.stats.ZeroManaCost.RemoveAllModifiersFromSource(crew);
            ship.stats.BonusAmmo.RemoveAllModifiersFromSource(crew);
        }
        public void RegisterCarpet(Carpet carpet)
        {
            if (carpets.Contains(carpet))
                return;
            carpet.OnActive += OnActiveCarpet;
            carpets.Add(carpet);
            CarpetStats carpetStats = carpet.Stats as CarpetStats;
            ShipStats shipStats = ship.Stats as ShipStats;

            shipStats.Luck.AddModifier(new StatModifier(carpetStats.Luck.Value, StatModType.Flat, carpet));
            shipStats.GoldIncome.AddModifier(new StatModifier(carpetStats.GoldEarning.Value, StatModType.Flat, carpet));
            shipStats.HealthRegenerationRate.AddModifier(new StatModifier(carpetStats.ShipHpRegen.Value, StatModType.Flat, carpet));
        }

        private void OnActiveCarpet(Carpet carpet, bool isActive)
        {
            ShipStats shipStats = ship.Stats as ShipStats;
            if (isActive)
            {
                CarpetStats carpetStats = carpet.Stats as CarpetStats;
                shipStats.HealthRegenerationRate.AddModifier(new StatModifier(carpetStats.ShipHpRegen.Value, StatModType.Flat, carpet));
            }
            else
            {
                shipStats.HealthRegenerationRate.RemoveAllModifiersFromSource(carpet);
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
            ShipStats shipStats = ship.Stats as ShipStats;
            shipStats.Luck.RemoveAllModifiersFromSource(carpet);
            shipStats.GoldIncome.RemoveAllModifiersFromSource(carpet);
            shipStats.HealthRegenerationRate.RemoveAllModifiersFromSource(carpet);
        }
    }
}

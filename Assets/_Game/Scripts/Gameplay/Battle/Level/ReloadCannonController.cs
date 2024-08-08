using _Base.Scripts.EventSystem;
using _Game.Scripts;
using _Game.Scripts.Entities;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class ReloadCannonController : MonoBehaviour
    {
        public Ship ship;

        public List<Cannon> outOfAmmoCannons;
        bool initialized;
        public void Init(Ship ship)
        {

            this.ship = ship;
            foreach (Cannon cannon in ship.ShipSetup.Cannons)
            {
                cannon.CannonAmmo.OutOfAmmoStateChaged += AutoReload;
            }
            initialized = true;
        }

        void AutoReload(Cannon cannon, bool isOutOfAmmo)
        {
            if (isOutOfAmmo)
            {
                ShipStats shipStats = ship.Stats as ShipStats;
                if (shipStats.ManaPoint.Value >= cannon.UsingAmmo.stats.EnergyCost.Value)
                {
                    ReloadCannon(ship, cannon, cannon.UsingAmmo);
                }
                else
                {
                    if (!outOfAmmoCannons.Contains(cannon))
                    {
                        outOfAmmoCannons.Add(cannon);
                    }
                }
            }
            else
            {
                if (outOfAmmoCannons.Contains(cannon))
                {
                    outOfAmmoCannons.Remove(cannon);
                }
            }
        }

        private void Update()
        {
            if (initialized)
            {
                ShipStats shipStats = ship.Stats as ShipStats;

                foreach (Cannon cannon in outOfAmmoCannons.ToArray())
                {
                    if (shipStats.ManaPoint.Value >= cannon.UsingAmmo.stats.EnergyCost.Value)
                    {
                        ReloadCannon(ship, cannon, cannon.UsingAmmo);
                        outOfAmmoCannons.Remove(cannon);
                    }
                }
            }
        }



        public void ReloadCannon(Ship ship, Cannon selectingCannon, Ammo ammo)
        {
            if (ammo == null)
                return;
            ShipStats shipStats = ship.Stats as ShipStats;
            if (shipStats.ManaPoint.Value < ammo.stats.EnergyCost.Value)
                return;
            bool isDoubleAmmo = false;
            bool isRefundMana = false;
            float ammoDoubledChance = ship.ShipBuffStats.BonusAmmo.Value;
            float refundManaCostChance = ship.ShipBuffStats.ZeroManaCost.Value;

            isDoubleAmmo = Random.Range(0, 1f) < ammoDoubledChance;
            isRefundMana = Random.Range(0, 1f) < refundManaCostChance;

            if (isRefundMana)
            {
                GlobalEvent<int, Vector3>.Send("MANA_REFUNDED", (int)ammo.stats.EnergyCost.Value, selectingCannon.transform.position);
            }
            else
            {

                float manaToBeConsumed = ammo.stats.EnergyCost.Value;
                shipStats.ManaPoint.StatValue.BaseValue -= manaToBeConsumed;
                GlobalEvent<int, Vector3>.Send("MANA_CONSUMED", (int)manaToBeConsumed, selectingCannon.transform.position);
            }

            selectingCannon.Reload(ammo, isDoubleAmmo);
        }
    }
}
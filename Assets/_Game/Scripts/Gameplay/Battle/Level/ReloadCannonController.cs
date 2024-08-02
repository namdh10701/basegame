using _Base.Scripts.EventSystem;
using _Game.Scripts;
using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class ReloadCannonController : MonoBehaviour
    {
        public BattleInputManager battleInputManager;
        public void ReloadCannon(Ship ship, Cannon selectingCannon, Ammo ammo)
        {
            ShipStats shipStats = ship.Stats as ShipStats;
            if (shipStats.ManaPoint.Value < ammo.stats.EnergyCost.Value)
                return;
            bool isDoubleAmmo = false;
            bool isRefundMana = false;
            float ammoDoubledChance = shipStats.BonusAmmo.Value;
            float refundManaCostChance = shipStats.BonusAmmo.Value;

            isDoubleAmmo = Random.Range(0, 1f) < ammoDoubledChance;
            isRefundMana = Random.Range(0, 1f) < refundManaCostChance;

            if (isRefundMana)
            {
                GlobalEvent<int, Vector3>.Send("MANA_REFUNDED", (int)ammo.stats.EnergyCost.Value, battleInputManager.worldPointerPos);
            }
            else
            {
                shipStats.ManaPoint.StatValue.BaseValue -= ammo.stats.EnergyCost.Value;
                GlobalEvent<int, Vector3>.Send("MANA_CONSUMED", (int)ammo.stats.EnergyCost.Value, battleInputManager.worldPointerPos);
            }

            selectingCannon.Reload(ammo, isDoubleAmmo);
        }
    }
}
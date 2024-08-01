using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Base.Scripts.RPG.Stats;
using _Game.Scripts;
using _Game.Scripts.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class CannonAmmo : MonoBehaviour
    {
        Cannon cannon;
        [SerializeField] Ammo usingAmmo;
        RangedStat ammoStat;

        public Ammo UsingAmmo => usingAmmo;

        public Action<bool> OutOfAmmoStateChaged;
        bool isOutOfAmmo;
        public AttackTargetBehaviour attackTargetBehaviour;

        public void Init(Cannon cannon)
        {
            this.cannon = cannon;
            CannonStats cannonStats = cannon.Stats as CannonStats;
            ammoStat = cannonStats.Ammo;
            cannon.CannonFeverStateManager.OnStateEntered += OnFeverStateEntered;
            ammoStat.OnValueChanged += Ammo_OnValueChanged;
        }

        private void OnFeverStateEntered(CannonFeverState state)
        {
            switch (state)
            {
                case CannonFeverState.None:
                    usingAmmo.Projectile.isFever = false;
                    break;
                case CannonFeverState.Fever:
                case CannonFeverState.FullFever:
                    if (usingAmmo.AmmoType == AmmoType.Bomb)
                    {
                        ammoStat.StatValue.BaseValue = usingAmmo.stats.MagazineSize.BaseValue + 10;
                    }
                    else
                    {
                        ammoStat.StatValue.BaseValue = usingAmmo.stats.MagazineSize.BaseValue;
                    }
                    usingAmmo.Projectile.isFever = true;
                    break;
            }
        }

        private void Ammo_OnValueChanged(RangedStat stat)
        {
            if (stat.StatValue.Value == stat.MinValue)
            {
                if (!isOutOfAmmo)
                {
                    isOutOfAmmo = true;
                    GlobalEvent<Cannon, Ammo, int>.Send("Reload", cannon, usingAmmo, 15);
                    OutOfAmmoStateChaged?.Invoke(isOutOfAmmo);
                }
            }
            else if (stat.StatValue.Value > 0)
            {
                if (isOutOfAmmo)
                {
                    isOutOfAmmo = false;
                    OutOfAmmoStateChaged?.Invoke(isOutOfAmmo);
                }

            }
        }

        public void Reload(Ammo ammo)
        {
            if (ammo == null)
            {
                ammoStat.MaxStatValue.BaseValue = 1;
                ammoStat.StatValue.BaseValue = 0;
            }
            else
            {
                this.usingAmmo = ammo;
                usingAmmo.Projectile.IsFever = false;
                ammoStat.MaxStatValue.BaseValue = ammo.stats.MagazineSize.Value;
                ammoStat.StatValue.BaseValue = ammo.stats.MagazineSize.Value;
                attackTargetBehaviour.projectilePrefab = ammo.Projectile;
            }

            this.usingAmmo = ammo;
            ammoStat.MaxStatValue.BaseValue = ammo.stats.MagazineSize.Value;
            ammoStat.StatValue.BaseValue = ammo.stats.MagazineSize.Value;
        }
    }
}
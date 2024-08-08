using System.Collections.Generic;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts;
using _Game.Scripts.GD;
using _Game.Scripts.GD.DataManager;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public abstract class CannonProjectile : Projectile, IGDConfigStatsTarget
    {
        [Header("Cannon Projectile")]
        [Space]
        public string id;

        public string Id { get => id; set => id = value; }
        private AmmoStatsConfigLoader _configLoader;

        public AmmoStatsConfigLoader ConfigLoader
        {
            get
            {
                if (_configLoader == null)
                {
                    _configLoader = new AmmoStatsConfigLoader();
                }

                return _configLoader;
            }
        }
        protected override void Awake()
        {
            base.Awake();
            var conf = GameData.AmmoTable.FindById(id);
            ConfigLoader.LoadConfig(_stats, conf);
            ApplyStats();
            foreach (Effect effect in outGoingEffects)
            {
                effect.Giver = this;
            }
        }
        public bool isFever;


        [Header("Visual")]
        public GameObject feverTrail;
        public GameObject normalTrail;


        public GameObject feverFx;
        public GameObject normalFx;

        public bool IsFever
        {
            get => isFever;
            set
            {
                isFever = value;
                aura.gameObject.SetActive(isFever);
                feverFx.SetActive(isFever);
                feverTrail.SetActive(isFever);
                normalFx.SetActive(!isFever);
                normalTrail.SetActive(!isFever);
            }
        }
    }
}
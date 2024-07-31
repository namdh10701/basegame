using System.Collections.Generic;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.GD;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public abstract class CannonProjectile : Projectile, IPhysicsEffectGiver, IGDConfigStatsTarget
    {
        [Header("Cannon Projectile")]
        [Space]
        public string id;
        public GDConfig gDConfig;
        public StatsTemplate statsTemplate;

        public string Id { get => id; set => id = value; }

        public GDConfig GDConfig => gDConfig;

        public StatsTemplate StatsTemplate => statsTemplate;
        protected override void Awake()
        {
            base.Awake();
        }
        private void Start()
        {
            GDConfigStatsApplier gDConfigStatsApplier = GetComponent<GDConfigStatsApplier>();
            gDConfigStatsApplier.LoadStats(this);
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
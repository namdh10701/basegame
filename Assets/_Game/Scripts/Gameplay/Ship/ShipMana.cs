using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Game.Scripts.Entities;
using _Game.Scripts.GameContext;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Ship
{
    public class ShipMana : MonoBehaviour
    {
        [SerializeField] bool IsAutoGenerate;
        [SerializeField] float maxMp;
        [SerializeField] float currentMp;
        [SerializeField] float generateRate;
        PlayerContext playerContext;
        private void Start()
        {
            playerContext = GlobalContext.PlayerContext;
            IsAutoGenerate = true;
            maxMp = playerContext.ManaPoint.MaxValue;
            currentMp = 0;
            generateRate = 5;
        }
        public float GenerateRate
        {
            get { return generateRate; }
            set { generateRate = value; }
        }
        public float MaxMP
        {
            get { return maxMp; }
            set { maxMp = value; }
        }
        public float CurrentMP
        {
            get { return currentMp; }
            set { currentMp = Mathf.Clamp(value, 0, maxMp); }
        }

        public bool ConsumeMana(float amount)
        {
            if (CurrentMP >= amount)
            {
                CurrentMP -= amount;
                return true;
            }
            return false;
        }

        private void Update()
        {
            if (IsAutoGenerate)
            {
                CurrentMP += GenerateRate * Time.deltaTime;
                // playerContext.ManaPoint = (int)CurrentMP;
            }
        }
    }
}

using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class EvasionBuffArea : MonoBehaviour, IEffectGiver
    {
        public EffectGiverCollisionListener EffectCollisionHandler;

        List<Effect> outGoingEffects = new List<Effect>();

        public List<Effect> OutGoingEffects { get => outGoingEffects; set => outGoingEffects = value; }
        public Transform Transform { get { return transform; } }
    }
}
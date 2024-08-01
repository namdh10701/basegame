using _Base.Scripts.EventSystem;
using _Game.Features.Inventory;
using _Game.Scripts;
using _Game.Scripts.Entities;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class FeverOrb : MonoBehaviour
    {
        public Vector2 TargetPosition;
        private void Update()
        {
            transform.position = Vector2.Lerp(transform.position, TargetPosition, Time.deltaTime * 8);
        }
    }
}
using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Input
{
    public class ItemClickDetector : MonoBehaviour
    {
        [SerializeField] Collider2D collider;
        public GameObject Item;
        public void Toggle(bool isOn)
        {
            collider.enabled = isOn;
        }
    }
}

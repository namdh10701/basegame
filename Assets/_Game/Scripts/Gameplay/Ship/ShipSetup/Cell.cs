using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    [System.Serializable]
    public class Cell : MonoBehaviour
    {
        public CellRenderer CellRenderer;
        public int X;
        public int Y;
        public Grid Grid;
        public IGridItem GridItem;
        public override string ToString()
        {
            return $"{X}, {Y}";
        }
    }
}



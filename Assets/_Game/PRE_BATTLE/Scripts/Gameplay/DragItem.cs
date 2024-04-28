using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts;
using _Game.Scripts.Entities;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;

namespace _Game.Scripts
{
    public class DragItem : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public GridItem GridItem;
        public GridItemReference GridItemReference;
        public List<Cell> cells;
        public void EnableRenderer()
        {
            spriteRenderer.enabled = true;
        }

        public void DisableRenderer()
        {
            spriteRenderer.enabled = false;
        }
       
    }
}

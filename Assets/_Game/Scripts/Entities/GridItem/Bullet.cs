using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.PathFinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class Bullet : MonoBehaviour, IGridItem, IWorkLocation, INodeOccupier
    {
        public Projectile Projectile;

        [SerializeField] private GridItemDef def;

        [field: SerializeField]
        public List<Cell> OccupyCells { get; set; }
        public GridItemDef Def { get => def; }
        public Transform Behaviour { get => null; }
        public string GridId { get; set; }
        public List<Node> workingSlots = new List<Node>();
        public List<Node> WorkingSlots { get => workingSlots; set => workingSlots = value; }
        public List<Node> occupyingNodes = new List<Node>(); 
        public List<Node> OccupyingNodes { get => occupyingNodes; set => occupyingNodes = value; }

        public void OnClick()
        {

        }

        public void Deactivate()
        {
            
        }

        public void OnFixed()
        {
           
        }
    }
}
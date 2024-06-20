using _Game.Scripts;
using _Game.Scripts.Entities;
using _Game.Scripts.PathFinding;
using System.Collections.Generic;
using UnityEngine;

public interface IWorkLocation
{
    public List<Node> WorkingSlots { get; set; }

    public void OnClick();
}

public interface INodeOccupier
{
    public List<Node> OccupyingNodes { get; set; }
}
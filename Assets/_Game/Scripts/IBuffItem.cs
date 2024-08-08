using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffRange
{
    Adjacent, Ship
}
public interface IBuffItem
{
    public BuffRange BuffRange { get; }
    public bool IsBuffable(IBuffable gridItem);
    public void Buff(IBuffable gridItem);
    public void RemoveBuff(IBuffable gridItem);
    public List<IBuffable> BuffedItems { get; }
}

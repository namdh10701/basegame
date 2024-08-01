using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGridItemView
{
    public void Init(IGridItem gridItem);
    public void HandleActive();
    public void HandleBroken();
}

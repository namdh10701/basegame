using _Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class CrewAction
{
    public string Name;
    public IEnumerator Execute;
    public IEnumerator Interupt;
    public abstract void ReBuild(Crew crew);
}

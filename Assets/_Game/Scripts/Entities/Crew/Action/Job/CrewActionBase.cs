using _Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class CrewActionBase
{
    public string Name;
    public IEnumerator Execute;
    public IEnumerator Interupt;
    public abstract void ReBuild(Crew crew);
}

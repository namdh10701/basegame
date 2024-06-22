using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class CrewAction
{
    public IEnumerator Execute;
    public IEnumerator Interupt;
}

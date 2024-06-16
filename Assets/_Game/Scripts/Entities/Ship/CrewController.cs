using _Base.Scripts.EventSystem;
using _Game.Scripts;
using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewController : MonoBehaviour
{
    public List<Crew> crews = new List<Crew>();
    private void Awake()
    {
        GlobalEvent<Cannon, Bullet>.Register("Reload", ReloadCannon);
    }

    void ReloadCannon(Cannon cannon, Bullet bullet)
    {
        crews[0].ReloadCannon(cannon, bullet);
    }
}

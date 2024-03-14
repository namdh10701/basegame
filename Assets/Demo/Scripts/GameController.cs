using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] ShipController _ship;
    void Start()
    {
        _ship.Setup();
    }
}

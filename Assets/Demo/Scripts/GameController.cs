using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour, IGameController
{
    [SerializeField] ShipController _ship;

    [SerializeField] PointClickDetector _pointClickDetector;
    void Awake()
    {
        Initializa();
    }

    private void Initializa()
    {
        _ship.Setup();
        _pointClickDetector.OnClickCallback = OnClickPointDetector;
    }

    private void OnClickPointDetector(GameObject go)
    {

    }
}

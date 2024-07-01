using _Game.Scripts.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Gameplay
{
    public class BattleManager : MonoBehaviour
    {
        public string selectShipid = "0001";
        public static string StageId;
        public Transform shipStartPos;
        public EntityManager EntityManager;
        public LevelStartSequence LevelStartSequence;
        public EnemyManager EnemyManager;
        public BattleInputManager BattleInputManager;
        public GridAttackHandler GridAttackHandler;
        public GridPicker GridPicker;
        public void Initnialize()
        {
            BattleInputManager.gameObject.SetActive(false);
            EntityManager.SpawnShip(selectShipid, shipStartPos.position);
            LevelStartSequence.shipSpeed = EntityManager.Ship.ShipSpeed;
            BattleInputManager.shipSetup = EntityManager.Ship.ShipSetup;
            GridAttackHandler.ship = EntityManager.Ship;
            GridPicker.ShipGrid = EntityManager.Ship.ShipSetup;
            StartCoroutine(LevelEntryCoroutine());
        }

        IEnumerator LevelEntryCoroutine()
        {
            yield return LevelStartSequence.Play();
            EnemyManager.StartLevel();
            BattleInputManager.gameObject.SetActive(true);
            EntityManager.Ship.ShipSetup.CrewController.ActivateCrews();
        }


        public void CleanUp()
        {
            //EntityManager.CleanUp();

        }
    }
}
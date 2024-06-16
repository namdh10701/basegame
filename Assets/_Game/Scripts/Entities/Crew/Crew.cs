using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Game.Scripts.Entities;
using _Game.Scripts.Gameplay.Ship;
using _Game.Scripts.PathFinding;
using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Game.Scripts
{
    public class Crew : Entity, IGridItem, IEffectTaker
    {
        public CrewStats stats;
        public float wait = 0.25f;
        public override Stats Stats => stats;
        public CrewStatsTemplate _statTemplate;

        [Header("Crew")]
        Ship Ship;
        PathfindingController pathfinder;
        public CrewAniamtionHandler Animation;

        [Header("EffectTaker")]
        public EffectHandler effectHandler;
        public Transform Transform => transform;

        public EffectHandler EffectHandler => effectHandler;


        [Header("GridItem")]
        public GridItemDef def;
        public Transform behaviour;
        [field: SerializeField]
        public List<Cell> OccupyCells { get; set; }
        public GridItemDef Def { get => def; set => def = value; }
        public Transform Behaviour { get => behaviour; }
        public string GridId { get; set; }


        public SpriteRenderer carryObject;
        private void Start()
        {
            Ship = FindAnyObjectByType<Ship>();
            pathfinder = Ship.PathfindingController;
        }

        protected override void ApplyStats()
        {
        }

        protected override void LoadModifiers()
        {

        }

        protected override void LoadStats()
        {
            _statTemplate.ApplyConfig(stats);
        }

        public void ReloadCannon(Cannon cannon, Bullet bullet)
        {
            StartCoroutine(ReloadCannonCoroutine(cannon, bullet));
        }

        IEnumerator ReloadCannonCoroutine(Cannon cannon, Bullet bullet)
        {
            Animation.PlayMove();

            Cell cellToReachBullet = GridHelper.GetClosetAvailableCellSurroundShape(Ship.ShipSetup.Grids[0].Cells, bullet.OccupyCells, transform.position);
            List<Vector3> path = pathfinder.GetPath(transform.position, cellToReachBullet.transform.position);
            yield return MoveCoroutine(path);
            Animation.PlayIdle();
            yield return new WaitForSeconds(wait);
            Animation.PlayCarry();
            carryObject.gameObject.SetActive(true);
            carryObject.sprite = bullet.Def.Image;
            Cell cellToReachCannon = GridHelper.GetClosetAvailableCellSurroundShape(Ship.ShipSetup.Grids[0].Cells, cannon.OccupyCells, transform.position);

            List<Vector3> path1 = pathfinder.GetPath(transform.position, cellToReachCannon.transform.position);
            yield return MoveCoroutine(path1);
            Animation.PlayIdle();
            yield return new WaitForSeconds(wait);
            cannon.Reloader.Reload(bullet.Projectile);
            carryObject.gameObject.SetActive(false);
        }

        IEnumerator MoveCoroutine(List<Vector3> path)
        {
            foreach (Vector3 waypoint in path)
            {
                Vector3 direction = (waypoint - transform.position).normalized;
                if (direction.x > 0)
                {
                    Animation.Flip(Direction.Right);
                }
                else if (direction.x < 0)
                {
                    Animation.Flip(Direction.Left);
                }
                while (Vector3.Distance(transform.position, waypoint) > 0.1f)
                {
                    body.velocity = direction * stats.MoveSpeed.Value;
                    yield return null;
                }
                body.velocity = Vector3.zero;
            }
        }
    }
}



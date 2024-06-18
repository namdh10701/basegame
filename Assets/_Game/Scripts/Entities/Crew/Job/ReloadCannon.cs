using _Game.Scripts;
using _Game.Scripts.Entities;
using _Game.Scripts.Gameplay.Ship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ReloadCannon : CrewJob
{
    Cannon cannon;
    Bullet bullet;
    public ReloadCannon(Cannon cannon, Bullet bullet)
    {
        this.cannon = cannon;
        this.bullet = bullet;

    }
    public override IEnumerator Execute()
    {
        crew.Animation.PlayMove();
        Cell cellToReachBullet = GridHelper.GetClosetAvailableCellSurroundShape(crew.Ship.ShipSetup.Grids[0].Cells, bullet.OccupyCells, crew.transform.position);
        List<Vector3> path = crew.pathfinder.GetPath(crew.transform.position, cellToReachBullet.transform.position);
        yield return MoveCoroutine(path);
        yield return new WaitForSeconds(.5f);
        crew.Animation.PlayCarry();
        crew.carryObject.gameObject.SetActive(true);
        crew.carryObject.sprite = bullet.Def.Image;
        Cell cellToReachCannon = GridHelper.GetClosetAvailableCellSurroundShape(crew.Ship.ShipSetup.Grids[0].Cells, cannon.OccupyCells, crew.transform.position);

        List<Vector3> path1 = crew.pathfinder.GetPath(crew.transform.position, cellToReachCannon.transform.position);
        yield return MoveCoroutine(path1);
        yield return new WaitForSeconds(.5f);
        cannon.Reloader.Reload(bullet.Projectile);
        crew.carryObject.gameObject.SetActive(false);
    }
    IEnumerator MoveCoroutine(List<Vector3> path)
    {
        foreach (Vector3 waypoint in path)
        {
            Vector3 direction = (waypoint - crew.transform.position).normalized;
            if (direction.x > 0)
            {
                crew.Animation.Flip(Direction.Right);
            }
            else if (direction.x < 0)
            {
                crew.Animation.Flip(Direction.Left);
            }
            while (Vector3.Distance(crew.transform.position, waypoint) > 0.1f)
            {
                crew.body.velocity = direction * crew.stats.MoveSpeed.Value;
                yield return null;
            }
            crew.body.velocity = Vector3.zero;
        }
        crew.Animation.PlayIdle();
    }
    public override IEnumerator Interupt()
    {
        yield break;
    }
}

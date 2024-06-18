using _Game.Scripts;
using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        yield return crew.CrewMovement.MoveByPath(path);
        yield return new WaitForSeconds(.5f);
        crew.Animation.PlayCarry();
        crew.carryObject.gameObject.SetActive(true);
        crew.carryObject.sprite = bullet.Def.Image;
        Cell cellToReachCannon = GridHelper.GetClosetAvailableCellSurroundShape(crew.Ship.ShipSetup.Grids[0].Cells, cannon.OccupyCells, crew.transform.position);

        List<Vector3> path1 = crew.pathfinder.GetPath(crew.transform.position, cellToReachCannon.transform.position);
        yield return crew.CrewMovement.MoveByPathCarry(path1);
        yield return new WaitForSeconds(.5f);
        cannon.Reloader.Reload(bullet.Projectile);
        crew.carryObject.gameObject.SetActive(false);
    }

    public override IEnumerator Interupt()
    {
        Status = JobStatus.Interupting;
        yield return null;
        Status = JobStatus.Free;
    }
}

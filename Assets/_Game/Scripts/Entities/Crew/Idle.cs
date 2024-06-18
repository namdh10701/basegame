using _Game.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : CrewAction
{
    Crew crew;
    public Idle(Crew crew)
    {
        this.crew = crew;
    }
    public override IEnumerator Execute()
    {
        Vector2 raycastOrigin = crew.transform.position;
        Vector2 raycastDirection = Vector2.zero;
        float raycastDistance = 1f;
        LayerMask layerMask = LayerMask.GetMask("Cell");
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, raycastDirection, raycastDistance, layerMask);
        if (hit.collider != null)
        {
            Cell cell = hit.collider.GetComponent<Cell>();
            if (cell != null)
            {
                crew.OccupyCells = new List<Cell> { cell };
            }
        }
        crew.Animation.AddIdle();
        yield return new WaitForSeconds(2);
    }

    public override IEnumerator Interupt()
    {
        yield break;
    }

}

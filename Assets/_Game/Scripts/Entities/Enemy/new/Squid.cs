using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid : Enemy, ITeleportable
{
    public PositionPool positionPool;
    public Vector2 TeleportPosition { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public void SetTargetPoses(List<Vector2> targetPoses)
    {
        positionPool.SetPool(targetPoses);
    }
    public void RefreshTeleportPosition()
    {
        throw new System.NotImplementedException();
    }
    public IEnumerator Teleport()
    {
        SpineAnimationEnemyHandler.PlayAnim(Anim.Hide, false, () => collider.enabled = false);
        yield return new WaitForSecondsRealtime(2f);
        body.MovePosition(TeleportPosition);
        SpineAnimationEnemyHandler.PlayAnim(Anim.Appear, false, () => collider.enabled = true);
    }
}

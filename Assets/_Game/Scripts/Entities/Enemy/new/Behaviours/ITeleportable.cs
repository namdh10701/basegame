using System.Collections;
using UnityEngine;

public interface ITeleportable
{
    public Vector2 TeleportPosition { get; set; }
    public void RefreshTeleportPosition();
    public IEnumerator Teleport();
}
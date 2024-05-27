using System.Collections;
using UnityEngine;

public interface ITeleporter
{
    public Vector2 TeleportPosition { get; set; }
    public void RefreshTeleportPosition();
    public IEnumerator Teleport();
}
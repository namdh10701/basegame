using UnityEngine;

public static class CoordinateConverter
{
    private static Vector2 topLeft = new Vector2(-11, 19.5f);
    private static Vector2 downRight = new Vector2(11, -15.5f);
    private static Vector2 gridSize = new Vector2(35, 22);

    public static Vector2 ToWorldPos(Vector2 coord)
    {
        float worldX = topLeft.x + coord.x;
        float worldY = topLeft.y - coord.y;

        // Return the world position
        return new Vector2(worldX + .5f, worldY);
    }
}
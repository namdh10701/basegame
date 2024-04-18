using UnityEngine;
public static class ArrayExtension
{
    public static T GetRandom<T>(this T[,] array)
    {
        if (array == null || array.Length == 0)
        {
            return default(T);
        }

        int rows = array.GetLength(0);
        int cols = array.GetLength(1);

        int randomRow = Random.Range(0, rows);
        int randomCol = Random.Range(0, cols);

        return array[randomRow, randomCol];
    }
}
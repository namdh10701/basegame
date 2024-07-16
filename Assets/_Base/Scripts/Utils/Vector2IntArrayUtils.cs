using System;
using System.Collections.Generic;
using UnityEngine;

// Required for Vector2Int

namespace _Base.Scripts.Utils
{
    public static class Vector2IntArrayUtils
    {
        public static List<Vector2Int> Shift(List<Vector2Int> list, Vector2Int offset)
        {
            if (list == null || list.Count == 0)
            {
                throw new ArgumentException("List must not be null or empty.");
            }

            List<Vector2Int> shiftedList = new List<Vector2Int>();

            foreach (Vector2Int vector in list)
            {
                Vector2Int shiftedVector = vector + offset;
                shiftedList.Add(shiftedVector);
            }

            return shiftedList;
        }
        public static Vector2Int FindMostCentralPoint(List<Vector2Int> list)
        {
            if (list == null || list.Count == 0)
            {
                throw new ArgumentException("List must not be null or empty.");
            }

            // Calculate the center of the list
            int centerX = 0;
            int centerY = 0;

            foreach (Vector2Int vector in list)
            {
                centerX += vector.x;
                centerY += vector.y;
            }

            centerX /= list.Count;
            centerY /= list.Count;

            Vector2Int centerPoint = new Vector2Int(centerX, centerY);

            // Find the point closest to the calculated center
            Vector2Int mostCentralPoint = list[0];
            float minDistance = Vector2Int.Distance(mostCentralPoint, centerPoint);

            for (int i = 1; i < list.Count; i++)
            {
                Vector2Int currentVector = list[i];
                float distance = Vector2Int.Distance(currentVector, centerPoint);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    mostCentralPoint = currentVector;
                }
            }

            return mostCentralPoint;
        }
        
        public static List<Vector2Int> ShiftToZero(List<Vector2Int> list)
        {
            if (list == null || list.Count == 0)
            {
                throw new ArgumentException("List must not be null or empty.");
            }

            // Find minimum x and y values in the list
            int minX = list[0].x;
            int minY = list[0].y;

            foreach (Vector2Int vector in list)
            {
                if (vector.x < minX)
                    minX = vector.x;
            
                if (vector.y < minY)
                    minY = vector.y;
            }

            // Shift all points to make minimum x and y values zero
            List<Vector2Int> shiftedList = new List<Vector2Int>();

            foreach (Vector2Int vector in list)
            {
                int newX = vector.x - minX;
                int newY = vector.y - minY;
                shiftedList.Add(new Vector2Int(newX, newY));
            }

            return shiftedList;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridPatternLogic
{
    public class Cell : MonoBehaviour
    {
        public int Row;
        public int Col;

        public void Set(int X, int Y)
        {
            this.Row = Y;
            this.Col = X;
            name = X.ToString() + ", " + Y.ToString();
        }

    }
}
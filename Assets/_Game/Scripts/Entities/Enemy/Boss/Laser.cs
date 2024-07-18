using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class Laser : MonoBehaviour
    {
        bool isLeft;
        public int maxAffectCell = 4;
        public GridPicker gridPicker;
        public int level = 1;
        public LaserGuide LaserGuide;
        public Cell startCell;
        void SelectCells()
        {
            startCell = gridPicker.PickRandomCell();
            if (startCell.transform.position.x < gridPicker.ShipGrid.transform.position.x)
            {
                isLeft = false;
            }
            else
            {
                isLeft = true;
            }

            // define max cell number = 4;
            // go from selected cell follow direction 

            // laser use a guide object
            // move guide object from left to right, or right to left, trigger effect if collided with selected cells

            // 3 level -> stage 1: 1 line, stage 2: 2 line: left - right, stage 3: 
            //
        }

        Tween tween;
        public void Play()
        {
            SelectCells();
            LaserGuide.Initialize(startCell, isLeft);
        }
    }
}
using System;
using UnityEngine;

namespace _Game.Scripts.Gameplay.TalentTree
{
    [ExecuteAlways]
    public class TalentNode: MonoBehaviour
    {
        public enum Type
        {
            Normal,
            Level,
            Premium,
        }

        public string itemId;
        public Type nodeType;

        public GameObject lineTop;
        public GameObject lineRight;
        public GameObject lineBottom;
        public GameObject lineLeft;

        private void Awake()
        {
            if (nodeType == Type.Normal)
            {
                lineLeft.SetActive(false);
            }
            else if (nodeType == Type.Premium)
            {
                lineRight.SetActive(false);
            }
        }
    }
}
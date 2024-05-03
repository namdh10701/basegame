using _Base.Scripts.Database;
using _Base.Scripts.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace _Game.Scripts
{
    public class GearIdentifier : Record, IBinarySaveData
    {
        public Sprite Image;
        public GearIdentifier(Sprite image)
        {
            Image = image;
        }

        public void Read(BinaryReader br)
        {

        }

        public void Write(BinaryWriter bw)
        {

        }
    }
}

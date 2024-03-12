using System.IO;
using _Base.Scripts.SaveSystem;

namespace _Game.Scripts.SaveSystem
{
    public class SaveData : IBinarySaveData
    {
        public int SaveId;
        public int Coin;

        public SaveData(int saveId, int coin)
        {
            SaveId = saveId;
            Coin = coin;
        }
        public SaveData()
        {

        }

        public void Read(BinaryReader br)
        {
            SaveId = br.ReadInt32();
            Coin = br.ReadInt32();
        }

        public void Write(BinaryWriter bw)
        {
            bw.Write(SaveId);
            bw.Write(Coin);
        }
    }
}


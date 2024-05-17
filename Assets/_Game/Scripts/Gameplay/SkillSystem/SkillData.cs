namespace _Game.Scripts.SkillSystem
{
    [System.Serializable]
    public class SkillData
    {
        public int Id;
        public int Level;

        public SkillData(int id, int level)
        {
            this.Id = id;
            this.Level = level;
        }
    }
}
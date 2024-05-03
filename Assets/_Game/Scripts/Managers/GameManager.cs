using _Base.Scripts;

namespace _Game.Scripts.Managers
{
    public class GameManager : BaseGameManager
    {
        public Database.Database Database;
        public override void LoadDatabase()
        {
            Database?.Load();
        }
    }
}

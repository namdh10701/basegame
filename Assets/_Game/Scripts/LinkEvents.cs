using _Base.Scripts.EventSystem;

namespace _Game.Scripts
{
    public static class LinkEvents
    {
        public static GameEvent Click_LevelSelect = new();
        public static GameEvent Click_MainMenu = new();
        public static GameEvent Setup_Ship_Completed = new();
        public static GameEvent Play_End = new();

        //MapScene

        public static GameEvent Click_Back = new();
        public static GameEvent Click_Play = new();
        public static GameEvent Click_PreBattle = new();
    }
}

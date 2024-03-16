using _Base.Scripts.EventSystem;

namespace _Game.Scripts
{
    public static class LinkEvents
    {
        public static BaseEvent Click_LevelSelect = new();
        public static BaseEvent Click_MainMenu = new BaseEvent();
        public static BaseEvent Setup_Ship_Completed = new();
        public static BaseEvent Play_End = new();
    }
}

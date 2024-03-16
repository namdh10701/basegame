    using _Base.Scripts.EventSystem;

    namespace _Game.Scripts
    {
        public static class LinkEvents
        {
            public static GameEvent Click_LevelSelect = new();
            public static GameEvent Click_MainMenu = new ();
            public static GameEvent Setup_Ship_Completed = new();
            public static GameEvent Play_End = new ();
        }
    }

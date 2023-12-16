namespace AsepStudios.Mechanic.GameCore
{
    public static class GameArgsHolder
    {
        public static GameArgs GameArgs { get; private set; }
        public static bool IsGameArgsInitialized = false;

        public const int MaxPlayerCount = 8;
        public const int MinPlayerCount = 1;
        public const int MaxPlayerStartPoint = 80;
        public const int MinPlayerStartPoint = 10;


        public static void SetGameArgs(GameArgs gameArgs)
        {
            GameArgs = gameArgs;
            IsGameArgsInitialized = true;
        }
    }
}
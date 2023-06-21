namespace AsepStudios.App
{
    public static class Session
    {
        public static string Username { get; private set; }
        public static int Avatar { get; private set; } //TODO make enum
        public static bool IsInitialized { get; private set; }

        public static void SetSession(string username, int avatar)
        {
            Username = username;
            Avatar = avatar;
            IsInitialized = true;
        }
    }

}


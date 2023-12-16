namespace AsepStudios.TableChump.App
{
    public static class Session
    {
        public static string Username { get; private set; }
        public static int AvatarIndex { get; private set; } //TODO make enum
        public static bool IsInitialized { get; private set; }

        public static void SetSession(string username, int avatar)
        {
            Username = username;
            AvatarIndex = avatar;
            IsInitialized = true;

            PlayerPreferences.Username = username;
            PlayerPreferences.AvatarIndex = avatar;
            PlayerPreferences.IsRegistered = true;
        }
    }

}


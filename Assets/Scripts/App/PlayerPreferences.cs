using UnityEngine;

namespace AsepStudios.App
{
    public static class PlayerPreferences
    {
        private const string IS_REGISTERED = "IsRegistered";
        private const string USERNAME = "Username";
        private const string AVATAR_INDEX = "AvatarIndex";

        public static void ClearPreferences()
        {
            PlayerPrefs.DeleteAll();
        }
        
        public static bool IsRegistered
        {
            get => PlayerPrefs.GetInt(IS_REGISTERED, 0) == 1;
            set => PlayerPrefs.SetInt(IS_REGISTERED, value ? 1 : 0);
        }
        
        public static string Username
        {
            get => PlayerPrefs.GetString(USERNAME, string.Empty);
            set => PlayerPrefs.SetString(USERNAME, value);
        }
        
        public static int AvatarIndex
        {
            get => PlayerPrefs.GetInt(AVATAR_INDEX, 0);
            set => PlayerPrefs.SetInt(AVATAR_INDEX, value);
        }
    }

}

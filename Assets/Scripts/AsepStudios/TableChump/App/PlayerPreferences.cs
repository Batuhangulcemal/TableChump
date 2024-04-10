using UnityEngine;

namespace AsepStudios.TableChump.App
{
    public static class PlayerPreferences
    {
        private const string IsRegisteredKey = "IsRegistered";
        private const string UsernameKey = "Username";
        private const string AvatarIndexKey = "AvatarIndex";

        public static void ClearPreferences()
        {
            PlayerPrefs.DeleteAll();
        }
        
        public static bool IsRegistered
        {
            get => PlayerPrefs.GetInt(IsRegisteredKey, 0) == 1;
            set => PlayerPrefs.SetInt(IsRegisteredKey, value ? 1 : 0);
        }
        
        public static string Username
        {
            get => PlayerPrefs.GetString(UsernameKey, string.Empty);
            set => PlayerPrefs.SetString(UsernameKey, value);
        }
        
        public static int AvatarIndex
        {
            get => PlayerPrefs.GetInt(AvatarIndexKey, 0);
            set => PlayerPrefs.SetInt(AvatarIndexKey, value);
        }
    }

}

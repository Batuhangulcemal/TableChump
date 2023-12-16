using AsepStudios.App;
using UnityEngine;

namespace AsepStudios.Utils
{
    public static class PlayerPreferencesLoader
    {
        public static void LoadPreferences()
        {
            if (PlayerPreferences.IsRegistered)
            {
                Session.SetSession(PlayerPreferences.Username, PlayerPreferences.AvatarIndex);
            }
        }
    }
}

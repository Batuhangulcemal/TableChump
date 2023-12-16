using AsepStudios.TableChump.App;

namespace AsepStudios.TableChump.Utils
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

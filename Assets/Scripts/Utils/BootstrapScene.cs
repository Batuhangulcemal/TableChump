using AsepStudios.SceneManagement;
using AsepStudios.Utils;
using UnityEngine;

public class BootstrapScene : MonoBehaviour
{
    private void Awake()
    {
        PlayerPreferencesLoader.LoadPreferences();
        Loader.Load(UnityScene.MainMenuScene);
    }
}

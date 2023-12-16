using AsepStudios.SceneManagement;
using AsepStudios.Utils;
using UnityEngine;

public class BootstrapScene : MonoBehaviour
{
    [SerializeField] private ResourceProvider resourceProvider;
    private void Awake()
    {
        resourceProvider.Initialize();
        resourceProvider.Initialize();
        PlayerPreferencesLoader.LoadPreferences();
        Loader.Load(UnityScene.MainMenuScene);
    }
}

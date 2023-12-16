using AsepStudios.TableChump.Utils.SceneManagement;
using UnityEngine;

namespace AsepStudios.TableChump.Utils
{
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
}

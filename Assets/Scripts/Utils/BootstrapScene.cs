using BoardsStake.SceneManagement;
using UnityEngine;

public class BootstrapScene : MonoBehaviour
{
    private void Awake()
    {
        Loader.Load(UnityScene.MainMenuScene);
    }
}

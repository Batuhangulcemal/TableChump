using BoardsStake.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoardsStake.SceneManagement
{
    public class Loader : MonoBehaviour
    {
        private static UnityScene targetScene;

        public static void Load(UnityScene targetScene)
        {
            Loader.targetScene = targetScene;
            SceneManager.LoadScene(UnityScene.LoadingScene.ToString());

        }

        public static void LoadWithoutLoadingScene(UnityScene targetScene)
        {
            SceneManager.LoadScene(targetScene.ToString());
        }

        public static void LoaderCallback()
        {
            SceneManager.LoadScene(targetScene.ToString());
        }
    }
}

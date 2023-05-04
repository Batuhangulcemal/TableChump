using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootstrapScene : MonoBehaviour
{
    private void Awake()
    {
        Loader.Load(Loader.Scene.MainMenuScene);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSceneCleanupHandler : MonoBehaviour
{

    private void Awake()
    {
        if(LobbyManager.Instance != null) Destroy(LobbyManager.Instance.gameObject);
    }
}

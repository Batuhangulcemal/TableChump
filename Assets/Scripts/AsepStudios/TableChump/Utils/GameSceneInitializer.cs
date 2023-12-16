using System;
using AsepStudios.Mechanic.PlayerCore.LocalPlayerCore;
using AsepStudios.UI;
using UnityEngine;

public class GameSceneInitializer : MonoBehaviour
{
    private void Start()
    {
        LocalPlayer.Instance.OnPlayerAttached += OnLocalPlayerAttached;
    }

    private void OnLocalPlayerAttached(object sender, EventArgs e)
    {
        ViewManager.Initialize();
    }
}

using System;
using AsepStudios.TableChump.Mechanics.PlayerCore.LocalPlayerCore;
using AsepStudios.TableChump.UI;
using UnityEngine;

namespace AsepStudios.TableChump.Utils
{
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
}

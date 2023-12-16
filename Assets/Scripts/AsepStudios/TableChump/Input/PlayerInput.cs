using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AsepStudios.Input
{
    public class PlayerInput : MonoBehaviour
    {
        public event EventHandler OnEscapePerformed;

        public static PlayerInput Instance { get; private set; }
        
        private PlayerActions actions;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        private void OnEnable()
        {
            actions = new PlayerActions();
            actions.Enable();

            actions.UI.Escape.performed += EscapeAction;
        }

        private void EscapeAction(InputAction.CallbackContext obj)
        {
            OnEscapePerformed?.Invoke(this, EventArgs.Empty);
        }

        private void OnDisable()
        {
            actions.Disable();
        }
    }

}

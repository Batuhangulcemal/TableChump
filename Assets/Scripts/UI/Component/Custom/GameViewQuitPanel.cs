using UnityEngine;
using UnityEngine.UI;

namespace AsepStudios.UI
{
    public class GameViewQuitPanel : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button quitButton;

        private static readonly int Active = Animator.StringToHash("Active");

        private bool active = true;

        public void Initialize()
        {
            ChangeState();
            
            resumeButton.onClick.AddListener(ChangeState);
            quitButton.onClick.AddListener(ConnectionService.Disconnect);
        }

        public void ChangeState()
        {
            active = !active;
            
            animator.SetBool(Active, active);
        }
    }
}
using AsepStudios.Mechanic.GameCore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AsepStudios.UI
{
    public class GameOverView : View
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Button testButton;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            testButton.onClick.AddListener(() =>
            {
                Game.Instance.RestartGame();
            });
        }
    }
}
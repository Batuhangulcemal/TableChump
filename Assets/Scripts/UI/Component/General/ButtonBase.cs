using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AsepStudios.UI
{
    public class ButtonBase : MonoBehaviour
    {
        public Button.ButtonClickedEvent OnClick => Button.onClick;
        public TextMeshProUGUI Text => TryGetText();
        private Button Button => GetButton();
        private Image Image => GetImage();



        public bool Interactable
        {
            get => Button.interactable;
            set => Button.interactable = value;
        }

        public Color ButtonColor
        {
            get => Image.color;
            set => Image.color = value;
        }

        public Sprite ButtonSprite
        {
            get => Image.sprite;
            set => Image.sprite = value;
        }
        
        private Button GetButton()
        {
            return GetComponent<Button>();
        }
        
        private Image GetImage()
        {
            return GetComponent<Image>();
        }
        
        private TextMeshProUGUI TryGetText()
        {
            return GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}
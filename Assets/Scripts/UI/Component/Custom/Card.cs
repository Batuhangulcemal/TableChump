using AsepStudios.Mechanic.PlayerCore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AsepStudios.UI
{
    public class Card : ToggleButton
    {
        [SerializeField] private Image cardImage;
        [SerializeField] private TextMeshProUGUI numberText;
        public int Number { get; private set; }
        
        public Card SetCard(int number)
        {
            Number = number;
            numberText.text = number.ToString();
            return this;
        }

        public void SetCard(int number, Player player)
        {
            SetCard(number);
        }
        
    }
}
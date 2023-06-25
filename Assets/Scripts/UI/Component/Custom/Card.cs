using System;
using AsepStudios.Mechanic.GameCore;
using AsepStudios.Mechanic.PlayerCore;
using AsepStudios.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AsepStudios.UI
{
    public class Card : ButtonBase, IToggleButton
    {
        [SerializeField] private Image cardImage;
        [SerializeField] private Outline outline;
        [SerializeField] private TextMeshProUGUI numberText;
        [SerializeField] private TextMeshProUGUI senderUsernameText;
        public int Number { get; private set; }
        
        private readonly Color highlightColor = ColorService.Orange;
        private readonly Color lowlightColor = ColorService.Cream;
        
        public bool IsHighlighted { get; private set; } = false;
        
        public Card SetCard(int number)
        {
            var point = CardPointCalculator.Calculate(number);
            Number = number;
            numberText.text = number.ToString();
            cardImage.color = GetColorByPoint(point);
            Highlight(IsHighlighted);
            return this;
        }

        public void SetCard(int number, string userName)
        {
            SetCard(number);
            senderUsernameText.text = userName;
        }

        private Color GetColorByPoint(int point)
        {
            if (point == 1)
            {
                return ColorService.Cream;
            }
            if (point == 2)
            {
                return ColorService.Beach;
            }
            if (point == 5)
            {
                return ColorService.Green;
            }
            if (point == 10)
            {
                return ColorService.Orange;
            }
            return ColorService.Red;
        }

        public void Highlight(bool value)
        {
            outline.effectColor = value ? highlightColor : lowlightColor;
        }
    }
}
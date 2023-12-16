using AsepStudios.TableChump.UI.Component.General;
using AsepStudios.TableChump.Utils.Service;
using UnityEngine;
using UnityEngine.UI;

namespace AsepStudios.TableChump.UI.Component.Custom
{
    public class ProfileAvatarButton : ButtonBase, IToggleButton
    {
        [SerializeField] private Outline outline;
            
        private readonly Color highlightColor = ColorService.Orange;
        private readonly Color lowlightColor = ColorService.Cream;
        public bool IsHighlighted { get; private set; } = false;

        private void Awake()
        {
            Highlight(IsHighlighted);
        }

        public void Highlight(bool value)
        {
            IsHighlighted = value;

            outline.effectColor = value ? highlightColor : lowlightColor;
        }
    }
}
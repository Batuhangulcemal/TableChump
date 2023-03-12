using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileView : View
{
    [SerializeField] private Button back;

    public override void Initialize()
    {
        back.onClick.AddListener(() => Back());

        base.Initialize();
    }

    private void Back()
    {
        PlayerController.Instance.IsReadyToPlay = true;
        UIManager.Instance.Show<MainMenuView>();

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : View
{
    [SerializeField] private Button back;
    public override void Initialize()
    {
        back.onClick.AddListener(() => Back());

        base.Initialize();
    }

    private void Back()
    {
        UIManager.Instance.Show<MainMenuView>();

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : View
{
    [SerializeField] private Button play;
    [SerializeField] private Button profile;
    [SerializeField] private Button settings;

    [SerializeField] private TextMeshProUGUI warnText;


    public override void Initialize()
    {
        play.onClick.AddListener(() => Play());
        profile.onClick.AddListener(() => Profile());
        settings.onClick.AddListener(() => Settings());

        base.Initialize();
    }

    private void Play()
    {

        UIManager.Instance.Show<PlayerNetworkSelectionView>();

    }

    private void Profile()
    {
        UIManager.Instance.Show<ProfileView>();
    }

    private void Settings()
    {
        UIManager.Instance.Show<SettingsView>();
    }

    private void Warn(string message)
    {
        warnText.text = message;
    }
}

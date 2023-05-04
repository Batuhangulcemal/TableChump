using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : UI
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    public override void Initialize()
    {
        base.Initialize();

        playButton.onClick.AddListener(() =>
        {
            UIManager.Instance.Show<ConnectionMenuUI>();
        });

        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {  get; private set; }

    [SerializeField] public List<UI> UIList;
    [SerializeField] private UI defaultUI;

    [SerializeField] private bool autoInitialize;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (autoInitialize) Initialize();
    }

    public void Initialize()
    {
        foreach (UI ui in UIList)
        {
            ui.Initialize();
            ui.Hide();
        }
        if (defaultUI != null)
        {
            defaultUI.Show();
        }
    }

    public void Show<TUI>() where TUI : UI
    {
        foreach(UI ui in UIList)
        {
            if(ui is TUI)
            {
                ui.Show();
            }
            else
            {
                ui.Hide();
            }
        }
    }

}

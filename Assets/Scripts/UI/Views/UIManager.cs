using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] public List<View> Views;

    [SerializeField] private bool autoInitialize;

    [SerializeField] private View defaultSlide;

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
        foreach (View slide in Views)
        {
            slide.Initialize();

            slide.Hide();
        }

        if (defaultSlide != null) defaultSlide.Show();
    }

    public void Show<TView>(object args = null) where TView : View
    {

        foreach (View view in Views)
        {
            if (view is TView)
            {
                view.Show(args);
            }
            else
            {
                view.Hide();
            }
        }
        

    }


}

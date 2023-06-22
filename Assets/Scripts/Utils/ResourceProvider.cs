using System;
using ScriptableObjects;
using UnityEngine;

public class ResourceProvider : MonoBehaviour
{
    public static ResourceProvider Instance { get; private set; }

    public static ColorPaletteSo Colors => Instance.colorPaletteSo;
    public static AvatarSo Avatars => Instance.avatars;

    [SerializeField] private ColorPaletteSo colorPaletteSo;
    [SerializeField] private AvatarSo avatars;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
}

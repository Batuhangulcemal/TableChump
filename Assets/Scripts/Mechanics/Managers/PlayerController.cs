using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] public FixedString32Bytes Username;
    [SerializeField] public bool IsReadyToPlay;

    private void Awake()
    {
        Instance = this;
    }
}

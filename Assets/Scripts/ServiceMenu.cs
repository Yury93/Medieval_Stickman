using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceMenu : MonoBehaviour
{
    [SerializeField] private SelectedCharWindow charWindow;
    [SerializeField] private StartWindow startWindow;
    [SerializeField] private SoundSystem soundSystem;
    public SelectedCharWindow CharWindow => charWindow;
    public StartWindow StartWindow => startWindow;


    public static ServiceMenu Instance;
    private void Awake()
    {
        Instance = this;
        soundSystem.Init();
    }
    private void Start()
    {
        charWindow.OnClose += OnCloseCharWindow;
        charWindow.OnOpen += OnOpenCharWindow;
    
    }

    private void OnOpenCharWindow(Window w)
    {
        startWindow.gameObject.SetActive(false);
    }

    private void OnCloseCharWindow(Window w)
    {
        startWindow.gameObject.SetActive(true);
    }
}

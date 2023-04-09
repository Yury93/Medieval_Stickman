using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartWindow : Window
{
    [SerializeField] private Button loadGameButton,selectChar;
    

    private void Start()
    {
        loadGameButton.onClick.AddListener(LoadGame);
        selectChar.onClick.AddListener(OpenCharWindow);
    }

    private void OpenCharWindow()
    {
        ServiceMenu.Instance.CharWindow.Open();
        gameObject.SetActive(false);
    }

    private void LoadGame()
    {
        SceneLoader.LoadGame();
    }
}

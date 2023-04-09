using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Button startButton;
   
    void Awake()
    {
        Time.timeScale = 0.00001f;
        startButton.onClick.AddListener(ClickStartGame);
    }

    private void ClickStartGame()
    {
        Time.timeScale =  1f;
        gameObject.SetActive(false);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Button startButton, adsRewardButton,helpButton;
   
    void Awake()
    {
        Yandex.instance.AdvButton = adsRewardButton;
        Yandex.instance.helpButton = helpButton;
        Time.timeScale = 0.00001f;
        startButton.onClick.AddListener(ClickStartGame);
        //adsRewardButton.onClick.AddListener(ShowAdv);
    }

   

    private void SetActiveHelp()
    {
        helpButton.gameObject.SetActive(true);
        adsRewardButton.gameObject.SetActive(false);
    }

    private void ClickStartGame()
    {
        Time.timeScale =  1f;
        gameObject.SetActive(false);
    }
}

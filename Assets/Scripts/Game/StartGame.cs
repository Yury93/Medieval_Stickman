using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Button startButton, adsRewardButton,helpButton;

    bool isPlayeAudio;
    void Awake()
    {
        Yandex.instance.AdvButton = adsRewardButton;
        Yandex.instance.helpButton = helpButton;
        Yandex.instance.OnShowAdvReward += ShowAdvReward;
        Time.timeScale = 0.00001f;
        startButton.onClick.AddListener(ClickStartGame);
        //adsRewardButton.onClick.AddListener(ShowAdv);
    }

    private void ShowAdvReward(bool show)
    {
       if(show == true)
        {
            isPlayeAudio = SoundSystem.instance.isAudioPlay;
            SoundSystem.instance.SetActiveSound(false);
        }
        else
        {
            if(isPlayeAudio)
            {
                SoundSystem.instance.SetActiveSound(true);
            }
        }

    }

    private void SetActiveHelp()
    {
        helpButton.gameObject.SetActive(true);
        adsRewardButton.gameObject.SetActive(false);
       
    }
    public void CloseAdvReward()
    {

    }
    private void ClickStartGame()
    {
        Time.timeScale =  1f;
        gameObject.SetActive(false);
    }
}

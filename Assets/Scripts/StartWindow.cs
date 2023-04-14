using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartWindow : Window
{
    [SerializeField] private Button loadGameButton,selectChar,english,russian,sound;
    [SerializeField] private TextMeshProUGUI soundsText;
    

    private void Start()
    {
        loadGameButton.onClick.AddListener(LoadGame);
        selectChar.onClick.AddListener(OpenCharWindow);
        english.onClick.AddListener(SetEnglish);
       russian.onClick.AddListener(SetRussian);
        sound.onClick.AddListener(SetActiveSound);
        LanguageSystem.instance.OnChangeLanguage += OnChangeLanguage;
    }

    private void OnChangeLanguage()
    {
        if (SoundSystem.instance.isAudioPlay)
        {
            soundsText.text = LanguageSystem.instance.Translater.GetValueOrDefault("«вук выкл.");
        }
        else
        {
            soundsText.text = LanguageSystem.instance.Translater.GetValueOrDefault("«вук вкл.");
        }
    }

    private void SetActiveSound()
    {
        if (SoundSystem.instance.isAudioPlay)
        {
            SoundSystem.instance.SetActiveSound(false);
            soundsText.text = LanguageSystem.instance.Translater.GetValueOrDefault("«вук вкл.");
        }
        else
        {
            SoundSystem.instance.SetActiveSound(true);
            soundsText.text = LanguageSystem.instance.Translater.GetValueOrDefault("«вук выкл.") ;
        }
    }

    private void SetRussian()
    {
        SoundSystem.instance.CreateSound(SoundSystem.instance.soundLibrary.clickButton);
    }

    private void SetEnglish()
    {
        SoundSystem.instance.CreateSound(SoundSystem.instance.soundLibrary.clickButton);
    }

    private void OpenCharWindow()
    {
        ServiceMenu.Instance.CharWindow.Open();
        gameObject.SetActive(false);
        SoundSystem.instance.CreateSound(SoundSystem.instance.soundLibrary.clickButton);
    }

    private void LoadGame()
    {
        SoundSystem.instance.CreateSound(SoundSystem.instance.soundLibrary.fire);
        SceneLoader.LoadGame();
     
    }
}

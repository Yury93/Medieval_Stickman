using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PushSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pushText;
    [SerializeField] private GameObject popupInfo,gameOverPopup;
    [SerializeField] private Button closeButton1, closeButton2, restartButton, menuButton; 
    
    public void Init()
    {
        closeButton1.onClick.AddListener(ClosePopupInfo);
        closeButton2.onClick.AddListener(ClosePopupInfo);

        restartButton.onClick.AddListener(SceneLoader.LoadGame);
        menuButton.onClick.AddListener(SceneLoader.LoadMenu);

        CoreEnivroment.Instance.upgradeGameSystem.OnUpgradeButton += OnUpgradeButton;
        CoreEnivroment.Instance.activeStickman.OnDeathStickman += OnDeathStickman;
    }

    private void OnDeathStickman()
    {
        gameOverPopup.gameObject.SetActive(true);

    }

  

    private void OnUpgradeButton(UpgradeGameSystem.TypeButtonUpgrade typeUpgraded)
    {
        OpenPopupInfo();
        if(typeUpgraded == UpgradeGameSystem.TypeButtonUpgrade.jerk)
        {
            pushText.text = LanguageSystem.instance.Translater.GetValueOrDefault("Вам доступен рывок");
        }
        if (typeUpgraded == UpgradeGameSystem.TypeButtonUpgrade.magicIdle)
        {
            pushText.text = LanguageSystem.instance.Translater.GetValueOrDefault("Теперь вы можете использовать магию");
        }
        if (typeUpgraded == UpgradeGameSystem.TypeButtonUpgrade.magicWalk)
        {
            pushText.text = LanguageSystem.instance.Translater.GetValueOrDefault("Теперь вы можете использовать магию в движении");
        }
        if (typeUpgraded == UpgradeGameSystem.TypeButtonUpgrade.kickWalk)
        {
            pushText.text = LanguageSystem.instance.Translater.GetValueOrDefault("Теперь вы можете использовать подкат");
        }
        if (typeUpgraded == UpgradeGameSystem.TypeButtonUpgrade.magic2)
        {
            pushText.text = LanguageSystem.instance.Translater.GetValueOrDefault("В нижней части экрана доступен выбор магии");
        }
        if (typeUpgraded == UpgradeGameSystem.TypeButtonUpgrade.magic3)
        {
            pushText.text = LanguageSystem.instance.Translater.GetValueOrDefault("Окрыта новая магия");
        }
        if (typeUpgraded == UpgradeGameSystem.TypeButtonUpgrade.magic4)
        {
            pushText.text = LanguageSystem.instance.Translater.GetValueOrDefault("Окрыта новая магия");
        }
        if (typeUpgraded == UpgradeGameSystem.TypeButtonUpgrade.help)
        {
            pushText.text = LanguageSystem.instance.Translater.GetValueOrDefault("Окрыта новая магия");
        }
    }

    private void OpenPopupInfo()
    {
        popupInfo.SetActive(true);
    }

    private void ClosePopupInfo()
    {
        popupInfo.SetActive(false);
    }
}

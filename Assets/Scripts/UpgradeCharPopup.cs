using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCharPopup : MonoBehaviour
{
    public enum TypeUpgrade { HP,Armor,Mana,Power}
    [Serializable]
    public class Parametrs
    {
        public TextMeshProUGUI hpText, armorText, manaText, powerText;
        public Button minusHp, plusHp, minusArmor, plusArmor, minusMana, plusMana, minusPower, plusPower;
        public int freeScore;
    }

    [SerializeField] private Parametrs parametrs;
    [SerializeField] private Button closeButton, backgroundCloseButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button accept;

    private Stickman stickman;
  public int score,hp,armor,mana,power;
    private bool isUpgrade;
    public void Init()
    {
        stickman = CoreEnivroment.Instance.activeStickman;
        closeButton.onClick.AddListener(Close);
        backgroundCloseButton.onClick.AddListener(Close);

        parametrs.minusHp.onClick.AddListener(()=>OnClickMinus(TypeUpgrade.HP));
        parametrs.plusHp.onClick.AddListener(() => OnClickPlus(TypeUpgrade.HP));

        parametrs.minusArmor.onClick.AddListener(() => OnClickMinus(TypeUpgrade.Armor));
        parametrs.plusArmor.onClick.AddListener(() => OnClickPlus(TypeUpgrade.Armor));


        parametrs.minusMana.onClick.AddListener(() => OnClickMinus(TypeUpgrade.Mana));
        parametrs.plusMana.onClick.AddListener(() => OnClickPlus(TypeUpgrade.Mana));


        parametrs.minusPower.onClick.AddListener(() => OnClickMinus(TypeUpgrade.Power));
        parametrs.plusPower.onClick.AddListener(() => OnClickPlus(TypeUpgrade.Power));
        accept.onClick.AddListener(ConfirmUpgrade);
        gameObject.SetActive(false);
    }

    private void ConfirmUpgrade()
    {
        if(isUpgrade)
        {
            Debug.Log(StickmanSaveUpgrader.GetStickmanParametrs(StickmanSaveUpgrader.SickmanParametr.Armor));
            StickmanSaveUpgrader.UpgradeStickmanParametrs
                (StickmanSaveUpgrader.GetStickmanParametrs(StickmanSaveUpgrader.SickmanParametr.Hitpoints) + hp
                ,StickmanSaveUpgrader.SickmanParametr.Hitpoints);
            StickmanSaveUpgrader.UpgradeStickmanParametrs
             (StickmanSaveUpgrader.GetStickmanParametrs(StickmanSaveUpgrader.SickmanParametr.Armor) + armor
             , StickmanSaveUpgrader.SickmanParametr.Armor);
            Debug.Log(StickmanSaveUpgrader.GetStickmanParametrs(StickmanSaveUpgrader.SickmanParametr.Armor));

            StickmanSaveUpgrader.UpgradeStickmanParametrs
            (StickmanSaveUpgrader.GetStickmanParametrs(StickmanSaveUpgrader.SickmanParametr.Mana) + mana
            , StickmanSaveUpgrader.SickmanParametr.Mana);
            StickmanSaveUpgrader.UpgradeStickmanParametrs
          (StickmanSaveUpgrader.GetStickmanParametrs(StickmanSaveUpgrader.SickmanParametr.Power) + power
          , StickmanSaveUpgrader.SickmanParametr.Power);
            Debug.Log(stickman.Armor + "/" + stickman.MaxArmor);


            stickman.CurrentHp += hp;
            stickman.Armor += armor;
            stickman.Mana += mana;
            stickman.Power += power;
            Debug.Log(stickman.Armor + "/" + stickman.MaxArmor);



            Close();
        }
    }

    private void OnClickPlus(TypeUpgrade type)
    {
        Debug.Log(stickman.Armor + "/" + stickman.MaxArmor);
        if (type == TypeUpgrade.HP)
        {
            if(score>0)
            {
                score -= 1;
                hp += 1;
                parametrs.hpText.text = LanguageSystem.instance.Translater.GetValueOrDefault("Здоровье") + ": " + (stickman.CurrentHp + hp);
                isUpgrade = true;
                return;
            }
        }
        if (type == TypeUpgrade.Armor)
        {
            if (score > 0)
            {
                score -= 1;
                armor += 1;
                Debug.Log(stickman.Armor+ " + " + armor + " /" + stickman.MaxArmor);
                parametrs.armorText.text = LanguageSystem.instance.Translater.GetValueOrDefault("Броня") + ": "+ (stickman.Armor + armor);
                isUpgrade = true;
                return;
            }
        }
        if (type == TypeUpgrade.Mana)
        {
            if (score > 0)
            {
                score -= 1;
                mana += 1;
                parametrs.manaText.text = LanguageSystem.instance.Translater.GetValueOrDefault("Мана") + ": "+ (stickman.Mana + mana);
                isUpgrade = true;
                return;
            }
        }
        if (type == TypeUpgrade.Power)
        {
            if (score > 0)
            {
                score -= 1;
                power += 1;
                parametrs.powerText.text = LanguageSystem.instance.Translater.GetValueOrDefault("Сила") + ": " + (stickman.Power + power);
                isUpgrade = true;
                return;
            }
        }
        if(score > 0)
        {
            isUpgrade = false;
            return;
        }

    }
    private void OnClickMinus(TypeUpgrade type)
    {
        if (type == TypeUpgrade.HP)
        {
            if( hp > 0)
            {
                score += 1;
                hp = 0;
                parametrs.hpText.text = LanguageSystem.instance.Translater.GetValueOrDefault("Здоровье") + ": "  + (stickman.CurrentHp);
                return;
            }
        }
        if (type == TypeUpgrade.Armor)
        {
            if ( armor > 0)
            {
                score += 1;
                armor = 0;
                parametrs.armorText.text = LanguageSystem.instance.Translater.GetValueOrDefault("Броня") + ": " + (stickman.Armor);
                return;
            }
        }
        if (type == TypeUpgrade.Mana)
        {
            if ( mana > 0)
            {
                score += 1;
                mana = 0;
                parametrs.manaText.text = LanguageSystem.instance.Translater.GetValueOrDefault("Мана") + ": " + (stickman.Mana);
                return;
            }
        }
        if (type == TypeUpgrade.Power)
        {
            if ( power > 0)
            {
                score += 1;
                power = 0;
                parametrs.powerText.text = LanguageSystem.instance.Translater.GetValueOrDefault("Сила") + ": " + (stickman.Power);
                return;
            }
        }
        if (score > 0)
        {
            isUpgrade = false;
            return;
        }
    }

    private void Close()
    {
        if (isUpgrade)
        {
            Time.timeScale = 1F;
            gameObject.SetActive(false);

            stickman.SetMaxParameters(stickman.CurrentHp, stickman.Armor, stickman.Mana);
            Debug.Log(stickman.Armor + "/" +stickman.MaxArmor);
            CoreEnivroment.Instance.guiStickman.RefreshStartParametrs(stickman.CurrentHp, stickman.Armor, stickman.Mana);
            CoreEnivroment.Instance.guiStickman.RefreshParametrs(stickman.CurrentHp, stickman.Armor, stickman.Mana);
        }
    }
    public void Open()
    {
        Time.timeScale = 0.00001F;
        gameObject.SetActive(true);

        score = 1;
        hp = 0;
        armor = 0;
        mana = 0;
        power = 0;


        stickman.CurrentHp = stickman.MaxHP;
        stickman.Mana = stickman.MaxMana;
        stickman.Armor = stickman.MaxArmor;


        parametrs.hpText.text = LanguageSystem.instance.Translater.GetValueOrDefault("Здоровье") + ": " + (stickman.CurrentHp);
        parametrs.armorText.text = LanguageSystem.instance.Translater.GetValueOrDefault("Броня") + ": " + (stickman.Armor);
        parametrs.manaText.text = LanguageSystem.instance.Translater.GetValueOrDefault("Мана") + ": " + (stickman.Mana);
        parametrs.powerText.text = LanguageSystem.instance.Translater.GetValueOrDefault("Сила") + ": " + (stickman.Power);

        SoundSystem.instance.CreateSound(SoundSystem.instance.soundLibrary.upgradePlayer, 0.3f);
    }
}

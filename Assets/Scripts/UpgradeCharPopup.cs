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
        stickman = UpgradeGameSystem.instance.stickman;
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
            StickmanSaveUpgrader.UpgradeStickmanParametrs
                (StickmanSaveUpgrader.GetStickmanParametrs(StickmanSaveUpgrader.SickmanParametr.Hitpoints) + hp
                ,StickmanSaveUpgrader.SickmanParametr.Hitpoints);
            StickmanSaveUpgrader.UpgradeStickmanParametrs
             (StickmanSaveUpgrader.GetStickmanParametrs(StickmanSaveUpgrader.SickmanParametr.Armor) + armor
             , StickmanSaveUpgrader.SickmanParametr.Armor);

            StickmanSaveUpgrader.UpgradeStickmanParametrs
            (StickmanSaveUpgrader.GetStickmanParametrs(StickmanSaveUpgrader.SickmanParametr.Mana) + mana
            , StickmanSaveUpgrader.SickmanParametr.Mana);
            StickmanSaveUpgrader.UpgradeStickmanParametrs
          (StickmanSaveUpgrader.GetStickmanParametrs(StickmanSaveUpgrader.SickmanParametr.Power) + power
          , StickmanSaveUpgrader.SickmanParametr.Power);


           stickman. CurrentHp += StickmanSaveUpgrader.GetStickmanParametrs(StickmanSaveUpgrader.SickmanParametr.Hitpoints);
            stickman.Armor += StickmanSaveUpgrader.GetStickmanParametrs(StickmanSaveUpgrader.SickmanParametr.Armor);
            stickman.Mana += StickmanSaveUpgrader.GetStickmanParametrs(StickmanSaveUpgrader.SickmanParametr.Mana);
            stickman.Power += StickmanSaveUpgrader.GetStickmanParametrs(StickmanSaveUpgrader.SickmanParametr.Power);


          
            Close();
        }
    }

    private void OnClickPlus(TypeUpgrade type)
    {
       if(type == TypeUpgrade.HP)
        {
            if(score>0)
            {
                score -= 1;
                hp += 1;
                parametrs.hpText.text = "Здоровье: "+ (stickman.CurrentHp + hp);
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
                parametrs.armorText.text = "Броня: " + (stickman.Armor + armor);
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
                parametrs.manaText.text = "МАНА: " + (stickman.Mana + mana);
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
                parametrs.powerText.text = "СИЛА: "+ (stickman.Power + power);
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
                parametrs.hpText.text = "Здоровье: " + (stickman.CurrentHp);
                return;
            }
        }
        if (type == TypeUpgrade.Armor)
        {
            if ( armor > 0)
            {
                score += 1;
                armor = 0;
                parametrs.armorText.text = "Броня: " + (stickman.Armor);
                return;
            }
        }
        if (type == TypeUpgrade.Mana)
        {
            if ( mana > 0)
            {
                score += 1;
                mana = 0;
                parametrs.manaText.text = "МАНА: " + (stickman.Mana);
                return;
            }
        }
        if (type == TypeUpgrade.Power)
        {
            if ( power > 0)
            {
                score += 1;
                power = 0;
                parametrs.powerText.text = "СИЛА: " + (stickman.Power);
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

            GuiStickman.instance.Init(stickman.CurrentHp, stickman.Armor, stickman.Mana);
            GuiStickman.instance.RefreshParametrs(stickman.CurrentHp, stickman.Armor, stickman.Mana);
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


        parametrs.hpText.text = "Здоровье: " + (stickman.CurrentHp);
        parametrs.armorText.text = "Броня: " + (stickman.Armor);
        parametrs.manaText.text = "МАНА: " + (stickman.Mana);
        parametrs.powerText.text = "СИЛА: " + (stickman.Power);
    }
}

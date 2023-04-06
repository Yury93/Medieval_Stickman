using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuiStickman : MonoBehaviour
{
    [SerializeField] private Image portretStickman, armorImage, hpImage, manaImage;
    [SerializeField] private TextMeshProUGUI armorText, hpText, manaText;
    private float maxHp, maxArmor, maxMana;
  
    public void Init()
    {
        portretStickman.sprite = CoreEnivroment.Instance.activeStickman.Portret;
    }
    public void RefreshStartParametrs(float hp, float armor,float mana)
    {
        maxArmor = armor;
        maxHp = hp;
        maxMana = mana;
        armorText.text = $"Броня: {Convert.ToInt32(armor)}/{Convert.ToInt32(armor)}";
        hpText.text = $"Здоровье: {Convert.ToInt32(hp)}/{Convert.ToInt32(hp)}";
        manaText.text = $"Мана: {Convert.ToInt32(mana)}/{Convert.ToInt32(mana)}";

        if (armor > 0)
        {
            armorImage.fillAmount = armor / maxArmor;
            armorText.text = $"Броня: {Convert.ToInt32(armor)}/{Convert.ToInt32(maxArmor)}";
        }
        else
        {
            armorImage.enabled = false;
            armorText.enabled = false;
        }
        hpImage.fillAmount = hp / maxHp;
        manaImage.fillAmount = mana / maxMana;
    }
    public void RefreshParametrs(float currentHp, float currentArmor, float currentMana)
    {
        var hp = Mathf.Clamp(currentHp, 0, 999999999);
        var armor = Mathf.Clamp(currentArmor, 0, 999999999);
        var mana = Mathf.Clamp(currentMana, 0, 999999999);
        if (armor > 0)
        {
            armorImage.fillAmount = currentArmor / maxArmor;
            armorText.text = $"Броня: {Convert.ToInt32(armor)}/{Convert.ToInt32(maxArmor)}";
            armorImage.enabled = true;
            armorText.enabled = true;
            hpText.enabled = false;
        }
        else
        {
            armorImage.enabled = false;
            armorText.enabled = false;
            hpText.enabled = true;
        }
        hpImage.fillAmount = currentHp / maxHp;
        manaImage.fillAmount = currentMana / maxMana;
       
        //if(currentArmor == 0)
        //{
        //    armorText.enabled = false;
        //}
        hpText.text = $"Здоровье: {Convert.ToInt32(hp)}/{Convert.ToInt32(maxHp)}";
        manaText.text = $"Мана: {Convert.ToInt32(mana)}/{Convert.ToInt32(maxMana)}";
       
    }
}

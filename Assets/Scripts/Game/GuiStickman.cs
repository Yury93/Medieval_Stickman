using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuiStickman : MonoBehaviour
{
    [SerializeField] private Image armorImage, hpImage, manaImage;
    [SerializeField] private TextMeshProUGUI armorText, hpText, manaText;
    private float maxHp, maxArmor, maxMana;
    public static GuiStickman instance;
    private void Awake()
    {
        instance = this;
    }
    public void Init(float hp, float armor,float mana)
    {
        maxArmor = armor;
        maxHp = hp;
        maxMana = mana;
        armorText.text = $"{Convert.ToInt32(armor)}/{Convert.ToInt32(armor)}";
        hpText.text = $"{Convert.ToInt32(hp)}/{Convert.ToInt32(hp)}";
        manaText.text = $"{Convert.ToInt32(mana)}/{Convert.ToInt32(mana)}";

        if (armor > 0)
        {
            armorImage.fillAmount = armor / maxArmor;
            armorText.text = $"{Convert.ToInt32(armor)}/{Convert.ToInt32(maxArmor)}";
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
        var hp = Mathf.Clamp(currentHp, 0, maxHp);
        var armor = Mathf.Clamp(currentArmor, 0, maxArmor);
        var mana = Mathf.Clamp(currentMana, 0, maxMana);
        if (armor > 0)
        {
            armorImage.fillAmount = currentArmor / maxArmor;
            armorText.text = $"{Convert.ToInt32(armor)}/{Convert.ToInt32(maxArmor)}";
        }
        else
        {
            armorImage.enabled = false;
            armorText.enabled = false;
            hpText.enabled = true;
        }
        hpImage.fillAmount = currentHp / maxHp;
        manaImage.fillAmount = currentMana / maxMana;
       
        hpText.text = $"{Convert.ToInt32(hp)}/{Convert.ToInt32(maxHp)}";
        manaText.text = $"{Convert.ToInt32(mana)}/{Convert.ToInt32(maxMana)}";
    }
}

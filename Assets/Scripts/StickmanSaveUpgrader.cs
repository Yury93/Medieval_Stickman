using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StickmanSaveUpgrader 
{
    public enum MagicTypeParametr { Power,ManaCost, Speed, Exp}
    public enum SickmanParametr { Mana,Armor,Hitpoints,Power}

    public const string EXP = "EXP";

    public static void SetExpStickman(int newValue)
    {
        PlayerPrefs.SetInt(EXP, newValue);
    }
    public static int GetExpStickman()
    {
        return PlayerPrefs.GetInt(EXP);
    }


    public const string MANA_PREFS = "MANA";
    public const string ARMOR_PREFS = "ARMOR";
    public const string HP_PREFS = "HITPOINT";
    public const string POWER_PREFS = "POWER";

    public static void UpgradeStickmanParametrs(int newValue, SickmanParametr type)
    {
        if(type == SickmanParametr.Mana)
        {
            PlayerPrefs.SetInt(MANA_PREFS, newValue);
        }
        else if(type == SickmanParametr.Armor)
        {
            PlayerPrefs.SetInt(ARMOR_PREFS, newValue);
        }
        else if(type == SickmanParametr.Hitpoints)
        {
            PlayerPrefs.SetInt(HP_PREFS,newValue);
        }
        else if(type == SickmanParametr.Power)
        {
            PlayerPrefs.SetInt(POWER_PREFS, newValue);
        }
    }
    public static int GetStickmanParametrs( SickmanParametr type)
    {
        if (type == SickmanParametr.Mana)
        {
          return  PlayerPrefs.GetInt(MANA_PREFS);
        }
        else if (type == SickmanParametr.Armor)
        {
           return PlayerPrefs.GetInt(ARMOR_PREFS);
        }
        else if (type == SickmanParametr.Hitpoints)
        {
            return PlayerPrefs.GetInt(HP_PREFS);
        }
        else if (type == SickmanParametr.Power)
        {
            return PlayerPrefs.GetInt(POWER_PREFS);
        }
        return 0;
    }
  

    #region Buttons
    public const string ROLLING_PREFS = "ROLLING";
    public const string MAGIC_PREFS = "MAGIC";
    public const string MAGIC_ROLLING_PREFS = "MAGIC_ROLLING_PREFS";
    public const string JERK_PREFS = "JERK";
    public const string HELP_PREFS = "HELP";
    public static void UpgradeRollingButton()
    {
        PlayerPrefs.SetInt(ROLLING_PREFS, 1);
    }
    public static int GetRollingButton()
    {
        return PlayerPrefs.GetInt(ROLLING_PREFS);
    }
    public static void UpgradeMagicButton()
    {
        PlayerPrefs.SetInt(MAGIC_PREFS, 1);
    }
    public static int GetMagicButton()
    {
        return PlayerPrefs.GetInt(MAGIC_PREFS);
    }
    public static void UpgradeMagicRollingButton()
    {
        PlayerPrefs.SetInt(MAGIC_ROLLING_PREFS, 1);
    }
    public static int GetMagicRollingButton()
    {
        return PlayerPrefs.GetInt(MAGIC_ROLLING_PREFS);
    }
    public static void UpgradeJerkButton()
    {
        PlayerPrefs.SetInt(JERK_PREFS, 1);
    }
    public static int GetJerkButton()
    {
        return PlayerPrefs.GetInt(JERK_PREFS);
    }
    public static void UpgradeHelpButton()
    {
        PlayerPrefs.SetInt(HELP_PREFS, 1);
    }
    public static int GetHelpButton()
    {
        return PlayerPrefs.GetInt(HELP_PREFS);
    }
    #endregion

    #region MagicButtons
    public const string MAGIC_2_PREFS = "MAGIC2";
    public const string MAGIC_3_PREFS = "MAGIC3";
    public const string MAGIC_4_PREFS = "MAGIC4";

    public static void UpgradeMagic2Button()
    {
        PlayerPrefs.SetInt(MAGIC_2_PREFS, 1);
    }
    public static int GetMagic2Button()
    {
        return PlayerPrefs.GetInt(MAGIC_2_PREFS);
    }

    public static void UpgradeMagic3Button()
    {
        PlayerPrefs.SetInt(MAGIC_3_PREFS, 1);
    }
    public static int GetMagic3Button()
    {
        return PlayerPrefs.GetInt(MAGIC_3_PREFS);
    }

    public static void UpgradeMagic4Button()
    {
        PlayerPrefs.SetInt(MAGIC_4_PREFS, 1);
    }
    public static int GetMagic4Button()
    {
        return PlayerPrefs.GetInt(MAGIC_4_PREFS);
    }

    #endregion

    #region MagicParametrs
    public const string MAGIC_1_POWER_PREFS = "MAGIC1POWER";
    public const string MAGIC_1_SPEED_PREFS = "MAGIC1SPEED";
    public const string MAGIC_1_MANACOST_PREFS = "MAGIC1COST";
    public static void UpgradeParametrsMagic1(int newValue, MagicTypeParametr type)
    {
        if(type == MagicTypeParametr.Power)
        {
            PlayerPrefs.SetInt(MAGIC_1_POWER_PREFS, newValue);
        }
        else if(type == MagicTypeParametr.ManaCost)
        {
            PlayerPrefs.SetInt(MAGIC_1_MANACOST_PREFS, newValue);
        }
        else if( type == MagicTypeParametr.Speed)
        {
            PlayerPrefs.SetInt(MAGIC_1_SPEED_PREFS, newValue);
        }
    }
    public static int GetParametrsMagic1(MagicTypeParametr type)
    {
        if (type == MagicTypeParametr.Power)
        {
           return PlayerPrefs.GetInt(MAGIC_1_POWER_PREFS);
        }
        else if (type == MagicTypeParametr.ManaCost)
        {
          return  PlayerPrefs.GetInt(MAGIC_1_MANACOST_PREFS);
        }
        else if (type == MagicTypeParametr.Speed)
        {
           return PlayerPrefs.GetInt(MAGIC_1_SPEED_PREFS);
        }
        return 0;
    }


    public const string MAGIC_2_POWER_PREFS = "MAGIC2POWER";
    public const string MAGIC_2_SPEED_PREFS = "MAGIC2SPEED";
    public const string MAGIC_2_MANACOST_PREFS = "MAGIC2COST";

    public static void UpgradeParametrsMagic2(int newValue, MagicTypeParametr type)
    {
        if (type == MagicTypeParametr.Power)
        {
            PlayerPrefs.SetInt(MAGIC_2_POWER_PREFS, newValue);
        }
        else if (type == MagicTypeParametr.ManaCost)
        {
            PlayerPrefs.SetInt(MAGIC_2_MANACOST_PREFS, newValue);
        }
        else if (type == MagicTypeParametr.Speed)
        {
            PlayerPrefs.SetInt(MAGIC_2_SPEED_PREFS, newValue);
        }
    }
    public static int GetParametrsMagic2(MagicTypeParametr type)
    {
        if (type == MagicTypeParametr.Power)
        {
            return PlayerPrefs.GetInt(MAGIC_2_POWER_PREFS);
        }
        else if (type == MagicTypeParametr.ManaCost)
        {
            return PlayerPrefs.GetInt(MAGIC_2_MANACOST_PREFS);
        }
        else if (type == MagicTypeParametr.Speed)
        {
            return PlayerPrefs.GetInt(MAGIC_2_SPEED_PREFS);
        }
        return 0;
    }

    public const string MAGIC_3_POWER_PREFS = "MAGIC3POWER";
    public const string MAGIC_3_SPEED_PREFS = "MAGIC3SPEED";
    public const string MAGIC_3_MANACOST_PREFS = "MAGIC3COST";

    public static void UpgradeParametrsMagic3(int newValue, MagicTypeParametr type)
    {
        if (type == MagicTypeParametr.Power)
        {
            PlayerPrefs.SetInt(MAGIC_3_POWER_PREFS, newValue);
        }
        else if (type == MagicTypeParametr.ManaCost)
        {
            PlayerPrefs.SetInt(MAGIC_3_MANACOST_PREFS, newValue);
        }
        else if (type == MagicTypeParametr.Speed)
        {
            PlayerPrefs.SetInt(MAGIC_3_SPEED_PREFS, newValue);
        }
    }
    public static int GetParametrsMagic3(MagicTypeParametr type)
    {
        if (type == MagicTypeParametr.Power)
        {
            return PlayerPrefs.GetInt(MAGIC_3_POWER_PREFS);
        }
        else if (type == MagicTypeParametr.ManaCost)
        {
            return PlayerPrefs.GetInt(MAGIC_3_MANACOST_PREFS);
        }
        else if (type == MagicTypeParametr.Speed)
        {
            return PlayerPrefs.GetInt(MAGIC_3_SPEED_PREFS);
        }
        return 0;
    }

    public const string MAGIC_4_POWER_PREFS = "MAGIC4POWER";
    public const string MAGIC_4_SPEED_PREFS = "MAGIC4SPEED";
    public const string MAGIC_4_MANACOST_PREFS = "MAGIC4COST";

    public static void UpgradeParametrsMagic4(int newValue, MagicTypeParametr type)
    {
        if (type == MagicTypeParametr.Power)
        {
            PlayerPrefs.SetInt(MAGIC_4_POWER_PREFS, newValue);
        }
        else if (type == MagicTypeParametr.ManaCost)
        {
            PlayerPrefs.SetInt(MAGIC_4_MANACOST_PREFS, newValue);
        }
        else if (type == MagicTypeParametr.Speed)
        {
            PlayerPrefs.SetInt(MAGIC_4_SPEED_PREFS, newValue);
        }
    }
    public static int GetParametrsMagic4(MagicTypeParametr type)
    {
        if (type == MagicTypeParametr.Power)
        {
            return PlayerPrefs.GetInt(MAGIC_4_POWER_PREFS);
        }
        else if (type == MagicTypeParametr.ManaCost)
        {
            return PlayerPrefs.GetInt(MAGIC_4_MANACOST_PREFS);
        }
        else if (type == MagicTypeParametr.Speed)
        {
            return PlayerPrefs.GetInt(MAGIC_4_SPEED_PREFS);
        }
        return 0;
    }
    #endregion

}

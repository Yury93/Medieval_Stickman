using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeGameSystem : MonoBehaviour
{
    public Stickman stickman;

    public ExpLevels ExpLevels;
    public UpgradeCharPopup UpgradeCharPopup;

    private int lastLevel;

    public static UpgradeGameSystem instance;
    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        ExpLevels.Init();
        ExpLevels.OnPlusExp += UpgradeCharPopupOpen;
        UpgradeCharPopup.Init();
        lastLevel = PlayerPrefs.GetInt("LASTLEVELCHAR");
    }

    private void UpgradeCharPopupOpen(int currentExp)
    {
        if(lastLevel != ExpLevels.CurrentLevel.Level)
        {
            UpgradeCharPopup.Open();
            lastLevel = ExpLevels.CurrentLevel.Level;
            PlayerPrefs.SetInt("LASTLEVELCHAR", lastLevel);
        }
    }
}

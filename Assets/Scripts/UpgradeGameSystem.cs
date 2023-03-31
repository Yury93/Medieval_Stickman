using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeGameSystem : MonoBehaviour
{
    public Stickman stickman;

    public ExpLevels ExpLevels;
    public UpgradeCharPopup UpgradeCharPopup;
    public MagicPanel magicPanel;

    public bool isJerk, isMagicWalk, isKickWalk, isHelp,isMagicIdle, isMagic2,isMagic3,isMagic4;

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


        if(StickmanSaveUpgrader.GetJerkButton() == 1)
        {
            isJerk = true;
        }
        if (StickmanSaveUpgrader.GetMagicRollingButton() == 1)
        {
            isMagicWalk = true;
        }
        if (StickmanSaveUpgrader.GetRollingButton() == 1)
        {
            isKickWalk = true;
        }
        if (StickmanSaveUpgrader.GetMagicButton() == 1)
        {
            isMagicIdle = true;
        }
        if (StickmanSaveUpgrader.GetHelpButton() == 1)
        {
            isHelp = true;
        }
        if (StickmanSaveUpgrader.GetMagic2Button() == 1)
        {
            magicPanel.SetActiveMagic1(true);
            magicPanel.SetActiveMagic2(true);
            isMagic2 = true;
        }
        if (StickmanSaveUpgrader.GetMagic3Button() == 1)
        {
            magicPanel.SetActiveMagic3(true);
            isMagic3 = true;
        }
        if (StickmanSaveUpgrader.GetMagic4Button() == 1)
        {
            magicPanel.SetActiveMagic4(true);
            isMagic4 = true;
        }
    }

    private void UpgradeCharPopupOpen(int currentExp)
    {
        if(lastLevel != ExpLevels.CurrentLevel.Level)
        {
            UpgradeCharPopup.Open();
            lastLevel = ExpLevels.CurrentLevel.Level;
            PlayerPrefs.SetInt("LASTLEVELCHAR", lastLevel);

            UpgradeButtons();
        }
    }

    private void UpgradeButtons()
    {
        if (ExpLevels.CurrentLevel.isButtonOpened == true)
        {
            if(isMagicIdle == false)
            {
                isMagicIdle = true;
                //magicPanel.SetActiveMagic1(true);
                StickmanSaveUpgrader.UpgradeMagicButton();
                return;
            }
            if(isKickWalk == false)
            {
                isKickWalk = true;
                StickmanSaveUpgrader.UpgradeRollingButton();
                return;
            }
            if (isMagicWalk == false)
            {
                isMagicWalk = true;
                StickmanSaveUpgrader.UpgradeMagicRollingButton();
                return;
            }
            if(isJerk == false)
            {
                isJerk = true;
                StickmanSaveUpgrader.UpgradeJerkButton();
                return;
            }
            if (isHelp == false)
            {
                isHelp = true;
                StickmanSaveUpgrader.UpgradeHelpButton();
                return;
            }


            if (isMagic2 == false)
            {
                isMagic2 = true;
                magicPanel.SetActiveMagic1(true);
                magicPanel.SetActiveMagic2(true);
                StickmanSaveUpgrader.UpgradeMagic2Button();
                return;
            }
            if (isMagic3 == false)
            {
                isMagic3 = true;
                magicPanel.SetActiveMagic3(true);
                StickmanSaveUpgrader.UpgradeMagic3Button();
                return;
            }
            if (isMagic4 == false)
            {
                isMagic4 = true;
                magicPanel.SetActiveMagic4(true);
                StickmanSaveUpgrader.UpgradeMagic4Button();
                return;
            }
        }
    }
}

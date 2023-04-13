using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoreEnivroment : MonoBehaviour
{
    public Stickman stickman;
    public Stickman stickManGirl;

    public HeartBonusCreator heartBonusCreator;
    public Stickman activeStickman;
    public Tower tower;
    public CameraMachine cameraMachine;
    public MagicPanel magicPanel;
    public GuiStickman guiStickman;
    public EnemiesService enemiesService;
    public UpgradeGameSystem upgradeGameSystem;
    public PushSystem pushSystem;

    public static CoreEnivroment Instance;
    private void Awake()
    {
        Instance = this;
        if (InterstateObject.instance.GetStickmanId() == 0)
        {
            activeStickman = stickman;

            stickManGirl.gameObject.SetActive(false);
            stickman.gameObject.SetActive(true);
            stickman.Init();
        }
        else
        {
            activeStickman = stickManGirl;
            stickManGirl.gameObject.SetActive(true);
            stickman.gameObject.SetActive(false);

            stickManGirl.Init();
        }
    }
    private void Start()
    {
        heartBonusCreator.Init();
        guiStickman.Init();
        enemiesService.Init();
        upgradeGameSystem.Init();
        tower.Init();
        cameraMachine.Init();
        magicPanel.Init();
        pushSystem.Init();

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete ("СДЕЛАТЬ АКТИВНЫМ И Инциализировать либо одного стикмана, либо другого")]
public class CoreEnivroment : MonoBehaviour
{
    public Stickman stickman;
    public Stickman stickManGirl;

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
    }
    private void Start()
    {
        //stickman.Init();

        stickManGirl.Init();
        guiStickman.Init();
        enemiesService.Init();
        upgradeGameSystem.Init();
        tower.Init();
        cameraMachine.Init();
        magicPanel.Init();
        pushSystem.Init();

    }
}

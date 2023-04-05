using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete ("Инциализировать либо одного стикмана, либо другого")]
public class CoreEnivroment : MonoBehaviour
{
    public Stickman stickman;
    public Stickman stickManGirl;

    public Stickman activeStickman;
    public Tower tower;

    public static CoreEnivroment Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        stickman.Init();
        //stickManGirl.Init();
    }
}

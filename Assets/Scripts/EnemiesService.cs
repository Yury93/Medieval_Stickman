using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesService : MonoBehaviour
{
    [SerializeField] private SpawnSystem spawnSystem;
    public SpawnSystem SpawnSystem => spawnSystem;
    public static EnemiesService instance;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        spawnSystem.Init();
    }

 
}

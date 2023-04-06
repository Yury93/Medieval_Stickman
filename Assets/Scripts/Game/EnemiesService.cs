using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesService : MonoBehaviour
{
    [SerializeField] private SpawnSystem spawnSystem;
    public SpawnSystem SpawnSystem => spawnSystem;
    public void Init()
    {
        spawnSystem.Init();
    }

   
 
}

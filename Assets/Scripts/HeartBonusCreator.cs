using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBonusCreator : MonoBehaviour
{
    [SerializeField] private HeartBonus heartBonusPrefab;

    public void Init()
    {
        CoreEnivroment.Instance.enemiesService.SpawnSystem.EnemySpawners.ForEach(s => s.OnEnemySpawn += OnEnemySpawn);
    }
  

    private void OnEnemySpawn(Enemy enemy)
    {
        enemy.OnEnemyDeath += OnEnemyDeath;
    }

    private void OnEnemyDeath(Enemy enemy)
    {
       var random = GenerateRandomNumber();
         if (random < 40)
        {
            Instantiate(heartBonusPrefab, new Vector3(enemy.transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, this.transform);
        }
        Debug.Log(random);
    }
  

    public int GenerateRandomNumber()
    {
        System.Random random = new System.Random();
        return random.Next(101);
    }
}

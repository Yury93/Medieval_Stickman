using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> enemySpawners;
    [SerializeField] private int currentWave;
    
    private List<Enemy> enemies;
    private int countKillEnemiesInCurrentWave;
    public List<EnemySpawner> EnemySpawners => enemySpawners;
    public List<Enemy> AllEnemies => enemies;
    public List<EnemyWave> enemyWaves;
    
    public void Init()
    {
        enemies = new List<Enemy>();
        enemySpawners.ForEach(s => s.OnEnemySpawn += AddListAllEnemies);
        enemySpawners.ForEach(s => s.StartCoroutine(s.CorSpawn(enemyWaves[currentWave].EnemyCount,enemyWaves[currentWave].EnemyPrefabs)));
       
    }
  

    private void AddListAllEnemies(Enemy enemy)
    {
        enemies.Add(enemy);
        enemy.OnEnemyDeath += TryStartNewWave;
       
    }

    private void TryStartNewWave(Enemy enemyDead)
    {
        enemies.Remove(enemyDead);
        countKillEnemiesInCurrentWave += 1;
        Debug.Log($"enemyWaves {currentWave} == countKillEnemiesInCurrentWave{countKillEnemiesInCurrentWave}");
        if (enemyWaves[currentWave].EnemyCount == countKillEnemiesInCurrentWave)
        {
           
            if (enemyWaves.Count-1  > currentWave)
            {
                Debug.Log($"current wave{currentWave} < countWave{enemyWaves.Count}");
                enemySpawners.ForEach(s => s.StartCoroutine(s.CorSpawn(enemyWaves[currentWave].EnemyCount, enemyWaves[currentWave].EnemyPrefabs)));
                currentWave += 1;
            }
            countKillEnemiesInCurrentWave = 0;
        }
    }

    public void RemoveEnemiesNull()
    {
        enemies.RemoveAll(e => e == null);
    }
}

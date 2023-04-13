using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> enemySpawners;
    [SerializeField] private int currentWave;
    [SerializeField] private TextMeshProUGUI numberWaveText;
    
    private List<Enemy> enemies;
    private int countKillEnemiesInCurrentWave;
    public List<EnemySpawner> EnemySpawners => enemySpawners;
    public List<Enemy> AllEnemies => enemies;
    public List<EnemyWave> enemyWaves;
    public List<Coroutine> coroutines;
    public void Init()
    {
        coroutines = new List<Coroutine>();
        enemies = new List<Enemy>();
        numberWaveText.enabled = false;
        enemySpawners.ForEach(s => s.OnEnemySpawn += AddListAllEnemies);
        enemySpawners.ForEach(s => s.StartCoroutine(s.CorSpawn(enemyWaves[currentWave].EnemyCount,enemyWaves[currentWave].EnemyPrefabs)));
        StartCoroutine(CorShowNumberWave(enemyWaves[currentWave].NumberWave));
        CoreEnivroment.Instance.activeStickman.OnDeathStickman += OnDeathStickman;
    }

    private void OnDeathStickman()
    {
        enemySpawners.ForEach(s => s.gameObject.SetActive(false));
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
       
        if (enemyWaves[currentWave].EnemyCount == countKillEnemiesInCurrentWave)
        {
            currentWave += 1;
            if (enemyWaves.Count  > currentWave)
            {
               
                enemySpawners.ForEach(s => s.StartCoroutine(s.CorSpawn(enemyWaves[currentWave].EnemyCount, enemyWaves[currentWave].EnemyPrefabs)));
               StartCoroutine(CorShowNumberWave(  enemyWaves[currentWave].NumberWave));
                //currentWave += 1;

            }
            countKillEnemiesInCurrentWave = 0;
        }
    }
    IEnumerator CorShowNumberWave(int number)
    {
        numberWaveText.enabled = true;
        string s = LanguageSystem.instance.Translater.GetValueOrDefault("Волна");
        numberWaveText.text = LanguageSystem.instance.Translater.GetValueOrDefault("Волна") +" " + number;
        yield return new WaitForSeconds(3f);
        numberWaveText.enabled = false;
    }

    public void RemoveEnemiesNull()
    {
        enemies.RemoveAll(e => e == null);
    }
}

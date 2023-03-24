using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy prefab;
    [SerializeField] private Transform startMove, finishMove;
    [SerializeField] private Transform darkArm;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float delay;
    [SerializeField] private Transform parentSpawnEnemy;
    private float cashDelay;
    private void Start()
    {
        StartCoroutine(CorSpawn());
    }
    public IEnumerator CorSpawn()
    {
        cashDelay = delay;
      var enemy = SpawnEnemy();
        while(delay >= 0 )
        {

            transform.position = Vector3.MoveTowards(transform.position, finishMove.position, moveSpeed * Time.deltaTime);
            yield return null;
            delay -= Time.deltaTime;
        }
        enemy.transform.SetParent(parentSpawnEnemy);
        enemy.Init();
        while (delay < cashDelay)
        {

            transform.position = Vector3.MoveTowards(transform.position, startMove.position, moveSpeed * Time.deltaTime);
            yield return null;
            delay += Time.deltaTime;
        }
    }

    public Enemy SpawnEnemy()
    {
       var enemy = Instantiate(prefab, darkArm);
        return enemy;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform startMove, finishMove;
    [SerializeField] private Transform darkArm;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float delay;
    [SerializeField] private Transform parentSpawnEnemy;
    [SerializeField] private float secondToNextWave;
    [SerializeField] private float xMax, xMin;
    private float cashDelay;
    public Action<Enemy> OnEnemySpawn;
    public int newPosX;

    private int GetRandomNumberInRange(int minValue, int maxValue)
    {
        return  UnityEngine.Random.Range(minValue, maxValue);
    }
    public int GenerateRandomNumber(int min, int max, int previousNumber = -1)
    {
        int nextNumber = previousNumber;
        Func<int> randomGenerator = () => new System.Random().Next(min, max + 1);

        while (nextNumber == previousNumber)
        {
            nextNumber = randomGenerator();
        }

        return nextNumber;
    }
    public IEnumerator CorSpawn(int enemyCount,List<Enemy> prefabsEnemy)
    {
        if (CoreEnivroment.Instance.activeStickman == null) yield break;
        yield return new WaitForSeconds(secondToNextWave);
        while (enemyCount > 0)
        {
            cashDelay = delay;
            var enemy = SpawnEnemy(prefabsEnemy[GetRandomNumberInRange(0,prefabsEnemy.Count)]);

            
            var newX = GenerateRandomNumber((int)xMin, (int)xMax,-1);
            while(newPosX == newX)
            {
                 newX = GenerateRandomNumber((int)xMin, (int)xMax, -1);
            }
            newPosX = newX;
            transform.localPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);

            OnEnemySpawn?.Invoke(enemy);
            while (delay >= 0)
            {

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, finishMove.position.y), moveSpeed * Time.deltaTime);
                yield return null;
                delay -= Time.deltaTime;
            }
           

            enemy.transform.SetParent(parentSpawnEnemy);
            enemy.Init();
            enemy.Rigidbody.gravityScale = 8;

            while (delay < cashDelay)
            {

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, startMove.position.y), moveSpeed * Time.deltaTime);
                yield return null;
                delay += Time.deltaTime;
            }
            enemyCount -= 1;

            yield return new WaitForSeconds(delay);
           
        }
    }

    private Enemy SpawnEnemy(Enemy prefab)
    {
       var enemy = Instantiate(prefab, darkArm.transform.position,Quaternion.identity,darkArm.transform);
        enemy.Rigidbody.gravityScale = 0;
        return enemy;
    }
}

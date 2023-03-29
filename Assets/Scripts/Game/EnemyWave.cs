using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "EnemyWave")]
public class EnemyWave : ScriptableObject
{
    [SerializeField] private int numberWave;
    [SerializeField] private int enemyCount;
    [SerializeField] private List<Enemy> enemyPrefabs;
    public int NumberWave => numberWave;
    public int EnemyCount => enemyCount;
    public List<Enemy> EnemyPrefabs => enemyPrefabs; 

}

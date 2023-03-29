using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawSpawner : MonoBehaviour
{
    [SerializeField] private Saw sawPrefab;

    private void Start()
    {
        StartCoroutine(CorSawCreate());
    }
    public IEnumerator CorSawCreate()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 50));
            Instantiate(sawPrefab, transform.position, Quaternion.identity);
        }
    }
}

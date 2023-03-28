using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickManTowerEvent : MonoBehaviour
{
    [SerializeField] private GameObject lasers;
    public void Attack()
    {
        StartCoroutine(CorAttack());
    }

    private IEnumerator CorAttack()
    {
        int count = 3;
        float delay = 0.1f;
        while (count > 0)
        {
            count -= 1;
            lasers.gameObject.SetActive(true);
            yield return new WaitForSeconds(delay);
            lasers.gameObject.SetActive(false);
            delay += 0.1f;
        }
        lasers.gameObject.SetActive(false);
    }
}

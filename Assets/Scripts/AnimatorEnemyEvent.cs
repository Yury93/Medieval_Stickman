using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEnemyEvent : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    public void ApplyDamage()
    {
        enemy.ApplyDamage();
    }
}

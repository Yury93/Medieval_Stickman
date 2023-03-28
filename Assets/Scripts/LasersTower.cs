using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LasersTower : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
       var enemy = collision.GetComponent<Enemy>();
        if(enemy)
        {
            enemy.OnDamage(20);
        }
    }
}

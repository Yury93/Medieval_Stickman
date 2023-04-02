using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LasersTower : MonoBehaviour
{
    [SerializeField] private Stickman stickman;
    private void OnTriggerEnter2D(Collider2D collision)
    {
       var enemy = collision.GetComponent<Enemy>();
        if(enemy)
        {
            if (stickman)
            {
                if (stickman.Mana > stickman.Power)
                    enemy.OnDamage(stickman.Mana);
                else
                    enemy.OnDamage(stickman.Power);
            }

        }
    }
}

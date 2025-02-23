using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int Armor; /*{ get; private set; }*/
    public int CurrentHp;/* { get; private set; }*/
    public bool Increadible;/*{ get; private set; }*/
    public TowerStickman towerStickMan;
    public Stickman stickman;
    public GameObject smoke;
    public void Init()
    {
        stickman = CoreEnivroment.Instance.activeStickman;
        towerStickMan.Init(stickman);
    }

    public void OnDamage(int damage)
    {
        if (Increadible == true) return;

        if (Armor <= 0)
        {
            CurrentHp -= damage;
        }
        else
        {
            Armor -= damage;
        }
        if (CurrentHp <= 0)
        {
            OnDeath(this);
        }
    }
    
    private void OnDeath(Tower tower)
    {
        Debug.Log("����� ���������");
        Time.timeScale = 1;
        Destroy(gameObject, 4);
        smoke.gameObject.SetActive(true);
    }
}

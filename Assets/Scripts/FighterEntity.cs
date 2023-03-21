using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterEntity : MonoBehaviour
{
    public PersonState State { get; protected set; }
    public int CurrentHp { get; protected set; }
    public int Armor { get; protected set; }
    public bool Increadible { get; protected set; }
    public int Power { get; private set; }

    public void SetParametrs(int currentHp,int armor,int power,bool increadible)
    {
        CurrentHp = currentHp;
        Armor = armor;
        Increadible = increadible;
        Power = power;
    }
    public void SetState(PersonState state)
    {
        State = state;
    }

    public virtual void OnDamage(int damage)
    {
        if (Increadible == true) return;

        if(Armor <=0 )
        {
            CurrentHp -= damage;
        }
        else
        {
            Armor -= damage;
        }
        if(CurrentHp <= 0)
        {
            OnDeath(this);
        }

    }

    protected virtual void OnDeath(FighterEntity fighterEntity)
    {
        Debug.Log("Смерть " + gameObject.name);
    }

   
}

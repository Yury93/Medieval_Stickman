using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterEntity : MonoBehaviour
{
    public PersonState State; /*{ get; protected set; }*/
    public int CurrentHp { get; protected set; }
    public int Armor { get; protected set; }
    public bool Increadible { get; protected set; }
    public int Power { get; private set; }
    [SerializeField] private List<SpriteRenderer> PersonSpriteRenderes;

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
        StartCoroutine(CorStartDamageReaction());
    }

    public IEnumerator CorStartDamageReaction()
    {
        if (CurrentHp <= 0) yield break;
        Increadible = true;
            var time = 10;
        bool flagActiveSprites = false;
        while (time != 0)
        {
            if (flagActiveSprites) flagActiveSprites = false;
            else flagActiveSprites = true;
            foreach (var sprite in PersonSpriteRenderes)
            {
                sprite.enabled = flagActiveSprites;
            }
            yield return new WaitForSeconds(0.05f);
            time -= 1;
        }
        foreach (var sprite in PersonSpriteRenderes)
        {
            sprite.enabled = true;
        }
        Increadible = false;
    }



    protected virtual void OnDeath(FighterEntity fighterEntity)
    {
        State = PersonState.Death;
        Debug.Log("Смерть " + gameObject.name);
        Destroy(gameObject, 5f);
    }

   
}

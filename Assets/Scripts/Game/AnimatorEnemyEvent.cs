using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEnemyEvent : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private EnemySpell currentSpell;
    [SerializeField] private Transform cratedMagic;
    [SerializeField] private Stickman stickman;
    public bool isMagicEnemy;
    public void OnAttack()
    {
        enemy.ApplyAttack();
    }

    public void CreateMagic()
    {
        if (currentSpell == null) return;
       stickman =  enemy.stickman;
        if (stickman == null) return;

        Debug.Log(cratedMagic + " / " + currentSpell);
        var spell = Instantiate(currentSpell, cratedMagic.position, Quaternion.identity);
        spell.SetMagicPower(enemy.Power);
        if (transform.rotation.y > 0)
            spell.MoveSpell(1);
        else
            spell.MoveSpell(-1);
    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEnemyEvent : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private EnemySpell currentSpell;
    [SerializeField] private Transform cratedMagic;
    [SerializeField] private Stickman stickman;
    public void OnAttack()
    {
        enemy.ApplyAttack();
    }

    public void CreateMagic()
    {
       stickman =  enemy.stickman;
        if (stickman == null) return;

        var spell = Instantiate(currentSpell, cratedMagic.position, Quaternion.identity);
        spell.Init(stickman);
        if (transform.rotation.y > 0)
            spell.MoveSpell(1);
        else
            spell.MoveSpell(-1);
    
    }
}

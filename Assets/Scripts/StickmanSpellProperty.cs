using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SpellProperty", menuName = "SpellStickmanProperty")]
public class StickmanSpellProperty : ScriptableObject
{
    [SerializeField]public int ManaCost;
    [SerializeField] public float Speed;
    [SerializeField] public int Power;
    [SerializeField] public int IsLearnIdSpell;
    public void SetLearnId(int id)
    {
        IsLearnIdSpell = id;
    }
}

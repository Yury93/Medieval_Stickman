using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimatorPlayerEvent : MonoBehaviour
{
    [SerializeField] private Stickman stickman;
    [SerializeField] private Transform cratedMagic;
    [SerializeField] private List<StickmanSpell> spell;
    [SerializeField] private StickmanSpell currentSpell;
    [SerializeField] private float delayPlusMana;
    
    Coroutine corRegenMana;
    public void ApplyDamageKickIdle()
    {
        stickman.ApplyDamage(stickman.Power/3);
    }
    public void ApplyDamageKickWalk()
    {
        stickman.ApplyDamage(stickman.Power/5);
    }
    public void CreateMagic()
    {
        currentSpell = GameLibrary.instance.spellLibrary.stickmanSpells.FirstOrDefault(s => s.Id == stickman.CurrentSpell.IsLearnIdSpell);
        if (stickman.CurrentSpell.ManaCost <= stickman.Mana)
        {
            var spell = Instantiate(currentSpell, cratedMagic.position, Quaternion.identity);
            spell.Init(stickman, stickman.CurrentSpell);
            if (transform.rotation.y > 0)
                spell.MoveSpell(1);
            else
                spell.MoveSpell(-1);
        }
        CoreEnivroment.Instance.guiStickman.RefreshParametrs(stickman .CurrentHp, stickman.Armor, stickman.Mana);

        if (corRegenMana != null) StopCoroutine(corRegenMana);
        corRegenMana = StartCoroutine(CorManaRegen());
        IEnumerator CorManaRegen()
        {
            while(stickman.MaxMana > stickman.Mana)
            {
                yield return new WaitForSeconds(delayPlusMana);
                stickman.Mana += 1;
                CoreEnivroment.Instance.guiStickman.RefreshParametrs(stickman.CurrentHp, stickman.Armor, stickman.Mana);
            }
        }
        
    }
 
   
}

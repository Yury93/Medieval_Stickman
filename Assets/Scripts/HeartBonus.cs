using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBonus : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(gameObject, 5);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var stickman = collision.gameObject.GetComponent<Stickman>();
        if(stickman)
        {
            if(stickman.CurrentHp != stickman.MaxHP || stickman.Armor != stickman.MaxArmor)
            {
                if(stickman.CurrentHp < stickman.MaxHP)
                {
                    SoundSystem.instance.CreateSound(SoundSystem.instance.soundLibrary.hpUp);
                    stickman.CurrentHp = stickman.MaxHP;
                    CoreEnivroment.Instance.guiStickman.RefreshParametrs(stickman.CurrentHp, stickman.Armor, stickman.Mana);
                    Destroy(gameObject);
                    return;
                }
                if (stickman.Armor < stickman.MaxArmor  && stickman.Armor != 0)
                {
                    SoundSystem.instance.CreateSound(SoundSystem.instance.soundLibrary.hpUp);
                    stickman.Armor = stickman.MaxArmor;
                    CoreEnivroment.Instance.guiStickman.RefreshParametrs(stickman.CurrentHp, stickman.Armor, stickman.Mana);
                    Destroy(gameObject);

                    return;
                }
                
            }
        }
    }
}

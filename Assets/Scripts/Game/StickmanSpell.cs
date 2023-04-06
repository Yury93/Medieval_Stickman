using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanSpell : MonoBehaviour
{
    [SerializeField] private int id;
    public int Id => id;
    public StickmanSpellProperty SpellProperty { get; private set; }
    private Stickman stickman;
    private Coroutine corMove;


    public void Init( Stickman stickman,StickmanSpellProperty stickmanSpellProperty)
    {
        SpellProperty = stickmanSpellProperty;
        this.stickman = stickman;
        this.stickman.Mana -= SpellProperty.ManaCost;
    }


    public void MoveSpell(float direction)
    {
      corMove =  StartCoroutine(CorMove(direction));

    }
    IEnumerator CorMove(float direction)
    {
       
        while(true)
        {
            if (transform.localScale.x < 2)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(2, 2, 2), 0.3f * Time.deltaTime);
              
            }
                transform.Translate(Vector3.right * direction * SpellProperty.Speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.OnDamage(SpellProperty.Power);
          
            if (corMove != null) StopCoroutine(corMove);

            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpell : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private int speed;
    [SerializeField] private int magicPower;
    
    private Coroutine corMove;


   public void SetMagicPower(int power)
    {
        magicPower = power;
    }


    public void MoveSpell(float direction)
    {
        corMove = StartCoroutine(CorMove(direction));

    }
    IEnumerator CorMove(float direction)
    {
        
        while (true)
        {
            if (transform.localScale.x < 2)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(2, 2, 2), 0.3f * Time.deltaTime);

            }
            transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var stickman = collision.GetComponent<Stickman>();
        var tower = collision.GetComponent<Tower>();
        if (stickman)
        {
            if (stickman.MoveController.IsEnemyColliderIgnore == false)
            {
                SoundSystem.instance.CreateSound(SoundSystem.instance.soundLibrary.electro, 0.3f);
                stickman.OnDamage(magicPower);

                if (corMove != null) StopCoroutine(corMove);

                Destroy(gameObject);
            }
        }
        else if(CoreEnivroment.Instance.activeStickman == null && tower)
        {
            tower.OnDamage(magicPower);

            if (corMove != null) StopCoroutine(corMove);

            Destroy(gameObject);
        }
    }
}

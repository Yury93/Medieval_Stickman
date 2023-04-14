using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private float timeLive;
    [SerializeField] private SpriteRenderer spriteSaw;
    [SerializeField] private int power;
    private void Start()
    {
        StartCoroutine(CorMove());

    }
    public IEnumerator CorMove()
    {

        while(timeLive > 0)
        {
            spriteSaw.transform.Rotate(0, 0, speed * speed * Time.fixedDeltaTime);
            rigidbody.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
            timeLive -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject, 1f);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var stickman = collision.GetComponent<Stickman>();
        var tower = collision.GetComponent<Tower>();
        if (tower)
        {
            Destroy(gameObject);
        }
        if (stickman)
        {
            stickman.OnDamage(power);
            SoundSystem.instance.CreateSound(SoundSystem.instance.soundLibrary.saw);
            Destroy(gameObject,10);
        }
    }
}

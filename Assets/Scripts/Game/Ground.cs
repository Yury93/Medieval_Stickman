using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
      var e =  collision.gameObject.GetComponent<Enemy>();
        if (e!=null)
        {
            e.Rigidbody.gravityScale = 1;
            e.ISGround = true;
        }
    }
}

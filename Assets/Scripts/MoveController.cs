using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveController
{
    private Rigidbody2D rigidbody;
    
    public PersonState State { get; private set ;  }
    public Animator animator { get; private set; }//child объект, берем для поворота персонажа
    public bool IsGrounded { get; private set; }
    
   
    public MoveController(Animator animator,Rigidbody2D rigidbody)
    {
        this.rigidbody = rigidbody;
        this.animator = animator;
        IsGrounded = true;
    }




    public IEnumerator CorJerk(float direction, float distance, float delay)
    {
        if (direction > 0)
        {
            rigidbody.transform.position = new Vector2(rigidbody.transform.position.x + distance, rigidbody.transform.position.y);
        }
        else
        {
            rigidbody.transform.position = new Vector2(rigidbody.transform.position.x - distance, rigidbody.transform.position.y);
        }
    
        rigidbody.gameObject.SetActive(true);
        
        yield return null;
    }





    public IEnumerator CorAutoWalk(float direction, float speedWalk, float duration)
    {
        var timeDuration = duration;
        while (timeDuration >= 0)
        {
            rigidbody.AddForce(new Vector2(direction * speedWalk, 0), ForceMode2D.Impulse);
            timeDuration -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
            
        }
    }
    public void Move(float direction,float speedWalk)
    {

        rigidbody.AddForce(new Vector2( direction * speedWalk,0), ForceMode2D.Impulse);

        if (IsGrounded)
        {
            if (direction > 0.2 || direction < -0.2)
            {
                State = PersonState.Walk;
            }
            else if (direction == 0)
            {
                State = PersonState.Idle;
            }
        }

        RotateToDirectionPerson(direction);
    }

    public IEnumerator CorJump(float jumpSpeed, float direction)
    {
        if (IsGrounded == false)
        {
            yield break;
        }
        State = PersonState.Jump;
        IsGrounded = false;

        rigidbody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);

        while (!IsGrounded && rigidbody.drag >= 0)
        {
            rigidbody.drag -= 2;
            yield return new WaitForFixedUpdate();
        }

    }
  
    public void SetGrounded(bool isGround)
    {
        this.IsGrounded = isGround;
        if (isGround)
        {
            State = PersonState.Idle;
            rigidbody.drag = 7;
        }

    }
  
    public void RotateToDirectionPerson(float direction)
    {
        if (direction >= 0.2)
        {
            animator.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (direction <= -0.2)
        {
            animator.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }

}

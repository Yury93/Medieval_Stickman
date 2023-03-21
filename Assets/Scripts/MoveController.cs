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
            Vector2 movement = new Vector2(1 * speedWalk * Time.fixedDeltaTime, rigidbody.velocity.y); 
            if (direction > 0)
            {
               movement = new Vector2(1 * speedWalk * Time.fixedDeltaTime, rigidbody.velocity.y);
                
            }
            else if (direction < 0)
            {
                movement = new Vector2(-1 * speedWalk * Time.fixedDeltaTime, rigidbody.velocity.y);
            }
            rigidbody.MovePosition(rigidbody.position + movement);
            timeDuration -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
            
        }
    }
    public void Move(float direction,float speedWalk)
    {
        Vector2 movement = new Vector2(direction * speedWalk * Time.fixedDeltaTime, rigidbody.velocity.y);
        rigidbody.MovePosition(rigidbody.position + movement);
       
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

    public IEnumerator CorJump(float jumpSpeed, float timeJump, LayerMask groundLayer)
    {
        if (IsGrounded == false)
        {
            yield break;
        }
        State = PersonState.Jump;

        IsGrounded = false;
        float timeFalling = timeJump;
        
        Jump(jumpSpeed);

        while (timeJump >= 0)
        {
            timeJump -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        Jump(-jumpSpeed);
        yield return new WaitForSeconds(timeFalling);

        Transform transformPerson = rigidbody.GetComponent<Transform>();
        IsGrounded = CheckIfGrounded( groundLayer);
        State = PersonState.Idle;
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
    }
    private bool CheckIfGrounded(LayerMask groundLayer)
    {
      var  collider = Physics2D.OverlapCircle(rigidbody. transform.position, 5,groundLayer);
        if (collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void Jump(float jumpForce)
    {
        rigidbody.velocity = new Vector2(0, jumpForce);
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

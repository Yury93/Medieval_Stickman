using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveController
{
    private Rigidbody2D rigidbody;
    private FighterEntity fighterEntity;

    public Animator animator { get; private set; }//child объект, берем для поворота персонажа
    public bool IsGrounded { get; private set; }
    public bool IsEnemyColliderIgnore { get; private set; }
   
    public MoveController(Animator animator,FighterEntity fighterEntity,Rigidbody2D rigidbody)
    {
        this.rigidbody = rigidbody;
        this.animator = animator;
        this.fighterEntity = fighterEntity;
        IsGrounded = true;
    }




    public IEnumerator CorJerk(float direction, float distance, float delay)
    {
        var stickman = fighterEntity as Stickman;
        if (stickman)
        {
            stickman.SetActiveBatsEffect();
        }
        yield return new WaitForFixedUpdate();
        IsEnemyColliderIgnore = true;
        float startSpeed = rigidbody.velocity.magnitude;

        rigidbody.AddForce(new Vector2(direction * distance * distance * distance, 0), ForceMode2D.Impulse);


        while (rigidbody.velocity.magnitude > startSpeed)
        {
            yield return null;
        }
        if (stickman)
        {
            stickman.DisactiveBatsEffect();
        }
        IsEnemyColliderIgnore = false;
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
               fighterEntity.SetState( PersonState.Walk);
            }
            else if (direction == 0)
            {
                fighterEntity.SetState(PersonState.Idle);
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
        SoundSystem.instance.CreateSound(SoundSystem.instance.soundLibrary.startJump,0.1f);
        fighterEntity.SetState(PersonState.Jump);
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
            fighterEntity.SetState(PersonState.Idle);
            rigidbody.drag = 7;
            SoundSystem.instance.CreateSound(SoundSystem.instance.soundLibrary.isGround, 0.1f);
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

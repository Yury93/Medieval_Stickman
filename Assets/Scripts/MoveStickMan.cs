using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveStickMan
{
    private AnimatorStickman animatorStickman;
    private Rigidbody2D rigidbody;
    private LayerMask groundLayer;
    public enum StateMoving { idle, walk, jump }
    public StateMoving State { get; private set; }
    public Animator AnimatorTransform { get; private set; }
    public bool IsGrounded { get; private set; }
    private float jumpSpeed;
    public MoveStickMan(Animator animator, AnimatorStickman animatorStickman,Rigidbody2D rigidbody)
    {
        this.animatorStickman = animatorStickman;
        this.rigidbody = rigidbody;
        AnimatorTransform = animator;
        IsGrounded = true;

    }

    public void Move(float direction,float speedWalk)
    {
        Vector2 movement = new Vector2(direction * speedWalk * Time.fixedDeltaTime, rigidbody.velocity.y);
        rigidbody.MovePosition(rigidbody.position + movement);

        if (IsGrounded)
        {
            if (direction > 0.2 || direction < -0.2)
            {
                State = StateMoving.walk;
                animatorStickman.SetWalk(true);
            }
            else if (direction == 0)
            {
                State = StateMoving.idle;
                animatorStickman.SetWalk(false);
            }
        }

        RotationStickMan(direction);
    }

    public IEnumerator CorJump(float jumpSpeed, float timeJump, LayerMask groundLayer)
    {
        if (IsGrounded == false)
        {
            yield break;
        }

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
    private void RotationStickMan(float direction)
    {
        if (direction >= 0.2)
        {
            AnimatorTransform.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (direction <= -0.2)
        {
            AnimatorTransform.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }

}

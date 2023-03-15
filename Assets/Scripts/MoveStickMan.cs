using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveStickMan
{
    public Transform StickmanTransfrorm { get; private set; }
    public Transform AnimatorTransform { get; private set; }
    public float SpeedWalk { get; private set; }
    public float SpeedJump { get; private set; }
    public float TimeJump { get; private set; }
    public bool IsGrounded { get; private set; }
    private AnimatorStickman animatorStickman;
    private float startTimeJump, startPositionY;
    private Joystick joystick;

    public MoveStickMan(
        Joystick joystick,AnimatorStickman animatorStickman
        ,Transform me, Transform animatorTransform,
        float speedWalk, float speedJump, float timeJump)
    {
        this.TimeJump = timeJump;
        startTimeJump = timeJump;
        this.SpeedJump = speedJump;
        this.SpeedWalk = speedWalk;
        this.AnimatorTransform = animatorTransform;
        StickmanTransfrorm = me;
        this.joystick = joystick;
        this.animatorStickman = animatorStickman;
        startPositionY = StickmanTransfrorm.position.y;
        IsGrounded = true;
    }


    public void Move()
    {
        if (IsGrounded)
        {
            if (joystick.Horizontal > 0.2 || joystick.Horizontal < -0.2)
                animatorStickman.SetWalk(true);
            else if (joystick.Horizontal == 0)
                animatorStickman.SetWalk(false);
        }

        StickmanTransfrorm.Translate(joystick.Horizontal * SpeedWalk * Time.deltaTime, 0, 0);
        RotationStickMan();
    }
   public IEnumerator CorJump()
    {
        IsGrounded = false;
        animatorStickman.SetJump(true);
        float clipLength = 0.3f;
        yield return new WaitForSeconds(clipLength);
        animatorStickman.SetJump(false);

        while (TimeJump > 0)
        {
            StickmanTransfrorm.Translate(joystick.Horizontal * SpeedWalk * Time.deltaTime, SpeedJump * Time.deltaTime, 0);
            TimeJump -= Time.deltaTime;

            yield return null;
        }
        yield return new WaitForSeconds(0.04f);
        while (StickmanTransfrorm.position.y >= startPositionY)// если у меня будут платформы, то можно сделать фер оверлап или рейкаст вниз для проверки на земле ли я нахожусь
        {
            StickmanTransfrorm.Translate(joystick.Horizontal * SpeedWalk * Time.deltaTime, -9.8f * Time.deltaTime, 0);
            yield return null;
        }
        animatorStickman.SetIdle(true);
        yield return new WaitForSeconds(0.1f);
        IsGrounded = true;
        TimeJump = startTimeJump;

    }

    private void RotationStickMan()
    {
        if (joystick.Horizontal >= 0.2) AnimatorTransform.transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (joystick.Horizontal <= -0.2) AnimatorTransform.rotation = Quaternion.Euler(Vector3.zero);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stickman : MonoBehaviour
{
    public Joystick joystick;
    public Transform animatorTransform;
    public float speedWalk, speedJump, timeJump;
    public AnimatorStickman animatorMan;
    public MoveStickMan moveMan;
    private void Start()
    {
        moveMan = new MoveStickMan(joystick,animatorMan, transform, animatorTransform, speedWalk, speedJump, timeJump);
    }

    void Update()
    {
        moveMan.Move();
        if (moveMan.IsGrounded == true && joystick.Vertical > 0.5f)
        {
            StartCoroutine(moveMan.CorJump());
        }
    }
}
